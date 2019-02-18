using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class GameHeader : MonoBehaviour {


    public GameObject SceneObjects;


    public static int numPlayers { set; get; }
    public static int CurrentTurn { set; get; }
    public static string Borad { set; get; }
    public static int BoradSize { set; get; }
    public static List<string> WinGeneSet { get; set; }//key=token,vale = bitarray
    public static Dictionary<string, string> TokenTrns;
    public static string[] Tokens { set; get; }
    public static string Win { set; get; }
    public static bool BWin { set; get; }
    public static string CurrentToken { get; set; }
    public static bool OnEditWin { get; set; }
    public static bool NeedToTrns { get; set; }
    public static bool Dirty { get; set; }
    public static string LastState=null;
    [ThreadStatic]
    public static Dictionary<int, List<State>> DicByLayer;
    public const string LAYERS_GLOBAL_PATH = "https://dynamicagent-681fa.firebaseio.com/BoardSize/3/Layers/";
    
    private void Awake()
    {
        //general setup for a game(3X3)
        CurrentToken = "X";
        WinGeneSet = new List<string>();
        Tokens = new string[4];
        Tokens[0] = "X";
        //  WinGeneSet.Add("X", new List<BitArray>());
        Tokens[1] = "O";
        //WinGeneSet.Add("O", new List<BitArray>());
        Tokens[2] = "@";
        // WinGeneSet.Add("@", new List<BitArray>());
        Tokens[3] = "&";
        //WinGeneSet.Add("&", new List<BitArray>());
        Debug.Log("Tokens" + Tokens);
        Debug.Log("WinGeneSet" + WinGeneSet);
        numPlayers = 2;
        CurrentTurn = 0;
        BoradSize = 3;
        //Borad = "0-";
        Borad += new string('_', BoradSize * BoradSize);
        BWin = false;
        Dirty = false;
        OnEditWin = false;
        NeedToTrns = false;
        Debug.Log("numPlayers" + numPlayers);
        Debug.Log("CurrentTurn" + CurrentTurn);
        Debug.Log("BoradSize" + BoradSize);
        Debug.Log("Borad" + Borad);
        Debug.Log("BWin" + BWin);
        //end setup game

        //active the game
        SceneObjects.SetActive(true);
        //
    }
    public static void GetStatesForLayer()
    {
        int v = GameHeader.CurrentTurn;


        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");

        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

            //get states from data base - in each state get the edges
            Debug.Log("loading layer " + v);

            //value = edges
            FirebaseDatabase.DefaultInstance
          .GetReference("BoardSize").Child("" + GameHeader.BoradSize).Child("Layers").Child("" + v).Child("States")
          .ValueChanged += GameHeader_ValueChanged;

    }

    private static void GameHeader_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int v = GameHeader.CurrentTurn;

        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }

        // Do something with the data in args.Snapshot
        DataSnapshot snapshot = e.Snapshot;

        Dictionary<string, object> dic = (Dictionary<string, object>)snapshot.Value;

        List<State> value = new List<State>();
        State currentState;
        //Debug.Log("dic contains " + staticDic.Count + " elements");
        //foreach (KeyValuePair<string, object> kv in staticDic)
        //    Debug.Log(kv.Key + " * **---*** " + "val = " + kv.Value);
        Dictionary<string, object> edgesDic;
        List<Edge> edges = new List<Edge>();
        Edge currentEdge;
        foreach (KeyValuePair<string, System.Object> item in dic)
        {
            edges.Clear();

            edgesDic = (Dictionary<string, System.Object>)item.Value;
            currentState = new State
            {
                Id = item.Key,
                Edges = new List<Edge>()
           
            };

            edgesDic = (Dictionary<string, System.Object>)edgesDic["Edges"];

            //foreach (KeyValuePair<string, System.Object> keyValuePair in edgesDic)
            //    Debug.Log(keyValuePair.Key);
            foreach (KeyValuePair<string, System.Object> keyValue in edgesDic)
            {
                Dictionary<string, System.Object> currentEdgeDB = new Dictionary<string, System.Object>();

                currentEdgeDB = (Dictionary<string, System.Object>)keyValue.Value;

                currentEdge = new Edge { };

                foreach (KeyValuePair<string, System.Object> edgeBody in currentEdgeDB)
                {
                    //Debug.Log(edgeBody.Key + ": " + edgeBody.Value.ToString());
                    switch (edgeBody.Key)
                    {
                        case "Id":
                            currentEdge.Id = edgeBody.Value.ToString();
                            break;
                        case "Sfrom":
                            currentEdge.Sfrom = edgeBody.Value.ToString();
                            break;
                        case "BoardSize":
                            currentEdge.BoardSize = int.Parse(edgeBody.Value.ToString());
                            break;
                        case "TotalEven":
                            currentEdge.TotalEven = int.Parse(edgeBody.Value.ToString());
                            break;
                        case "TotalLost":
                            currentEdge.TotalLost = int.Parse(edgeBody.Value.ToString());
                            break;
                        case "TotalPass":
                            currentEdge.TotalPass = int.Parse(edgeBody.Value.ToString());
                            break;
                        case "TotalWin":
                            currentEdge.TotalWin = int.Parse(edgeBody.Value.ToString());
                            break;
                        case "Weight":
                            currentEdge.Weight = int.Parse(edgeBody.Value.ToString());
                            break;
                        case "fromlayer":
                            currentEdge.fromlayer = int.Parse(edgeBody.Value.ToString());
                            break;

                    }//end switch
                }//end foreach edgebody
                edges.Add(currentEdge);
                

            }//end foreach edge
            currentState.Edges.AddRange(edges);
            
            value.Add(currentState);
           
            State found;
            if (GameHeader.DicByLayer.ContainsKey(v))//has this layer, add the state to the list
            {

                found = GameHeader.DicByLayer[v].Find(s => s.Id.Equals(currentState.Id));
                if (found == null)//add the state
                {
                    GameHeader.DicByLayer[v].Add(currentState);
                }
                else//update the edges
                {
                    found = currentState;
                }

            }

        }
        //Debug.Log("dic contains " + DicByLayer.Count + " layers");
        //Debug.Log("my*********************** key is==== " + key + "  num of states ===  " + value.Count);
        if (!GameHeader.DicByLayer.ContainsKey(v))
        {
            GameHeader.DicByLayer.Add(v, value);//add to the dictionary
        }


    }//end on value changed





    public void printwinsoutside()
    {
        Debug.Log("wins outside:");
        foreach (string s in GameHeader.WinGeneSet)
            Debug.Log(s);
    }
    public static void printDic(int v)
    {
        

    }
    // Use this for initialization
    void Start () {

        if (GameHeader.DicByLayer == null)
        {
            Debug.Log("dic is null --- suppose to be 1 time !!!");
            GameHeader.DicByLayer = new Dictionary<int, List<State>>();
        }



    }
    private static void NextTurn()//Layer++
    {
        CurrentTurn += 1;
       

    }


    public static void SetNextPlayerToken()
    {
        NextTurn();
        CurrentToken = Tokens[CurrentTurn % numPlayers];
        

    }




   
}
