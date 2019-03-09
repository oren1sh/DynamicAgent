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
using UnityEngine.SceneManagement;

public class GameHeader : MonoBehaviour {


    public GameObject SceneObjects;

    public SceneManager Manager;


    public static int numPlayers { set; get; }
    public static int CurrentTurn { set; get; }
    public static string Borad { set; get; }
    public static int BoradSize = 3;
    public static List<string> WinGeneSet { get; set; }//key=token,vale = bitarray
    public static Dictionary<string, string> TokenTrns;
    public static string[] Tokens { set; get; }
    public static List<string> TokensL { set; get; }
    public static string Win { set; get; }
    public static bool BWin { set; get; }
    public static string CurrentToken { get; set; }
    public static bool OnEditWin { get; set; }
    public static bool NeedToTrns { get; set; }
    public static bool Dirty { get; set; }
    public static string LastState=null;
    public static Dictionary<int, List<State>> DicByLayer;
    
    private void Awake()
    {
        


        DontDestroyOnLoad(this.gameObject);
        //general setup for a game(3X3)
        CurrentToken = "X";
        WinGeneSet = new List<string>();
        Tokens = new string[4];
        TokensL = new List<string>(4);
        Tokens[0] = "X";
        TokensL.Add("X");
        Tokens[1] = "O";
        TokensL.Add("O");
        Tokens[2] = "@";
        TokensL.Add("@");
        Tokens[3] = "&";
        TokensL.Add("&");
        // Debug.Log("Tokens" + Tokens);
        //Debug.Log("WinGeneSet" + WinGeneSet);
        numPlayers = 2;
        CurrentTurn = 0;
        BoradSize = 3;
        //Borad = "0-";
        Borad += new string('_', BoradSize * BoradSize);
        BWin = false;
        Dirty = false;
        OnEditWin = false;
        NeedToTrns = false;
        //Debug.Log("numPlayers" + numPlayers);
        //Debug.Log("CurrentTurn" + CurrentTurn);
        Debug.Log("BoradSize" + BoradSize);
        //Debug.Log("Borad" + Borad);
        //Debug.Log("BWin" + BWin);
        //end setup game

        //active the game
        SceneObjects.SetActive(true);
        //
    }
    public static int v = 0;
    public static void GetStatesForLayer(Dictionary<int,List<State>> dicTarget)
    {
        if (v == -1)
            v = 1;
        else
            if (GameHeader.CurrentTurn == ((BoradSize * BoradSize) - 1))
            return;
            else
                if(GameHeader.CurrentTurn>0 )
                    v = GameHeader.CurrentTurn+1;

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");

        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        //get states from data base - in each state get the edges
        // Debug.Log("loading layer " + v);

        //value = edges
        FirebaseDatabase.DefaultInstance
      .GetReference("BoardSize").Child("" + GameHeader.BoradSize).Child("Layers").Child("" + v).Child("States")
      .ValueChanged += (sender, e) => GameHeader_ValueChanged(sender, e, dicTarget); 



        /*
                FirebaseDatabase.DefaultInstance
                 .GetReference("BoardSize").Child("" + GameHeader.BoradSize).Child("Layers").Child((v + 1).ToString()).Child("States")
                 .ValueChanged += GameHeader_ValueChanged1;
                    FirebaseDatabase.DefaultInstance
                 .GetReference("BoardSize").Child("" + GameHeader.BoradSize).Child("Layers").Child((v + 2).ToString()).Child("States")
                 .ValueChanged += GameHeader_ValueChanged2;
                */

    }

    public static void LoadSnapshotToDictionary(Dictionary<string, object> snapshotValue,Dictionary<int,List<State>> dicTarget, int currentV)
    {
        try {
            
            List<State> value = new List<State>();
            State currentState;
            //Debug.Log("dic contains " + staticDic.Count + " elements");
            //foreach (KeyValuePair<string, object> kv in staticDic)
            //  Debug.Log(kv.Key + " * **---*** " + "val = " + kv.Value);
            Dictionary<string, object> edgesDic;
            List<Edge> edges = new List<Edge>();
            Dictionary<string, object> valuesDic;

            Edge currentEdge;
            foreach (KeyValuePair<string, System.Object> item in snapshotValue)
            {
                edges.Clear();

                //key=edges,value=list
                //key = layerID, value = layer number
                valuesDic = (Dictionary<string, System.Object>)item.Value;

                //edgesDic = (Dictionary<string, System.Object>)valuesDic["Edges"];
               

                currentState = new State
                {
                    Id = item.Key,
                    Edges = new List<Edge>(),
                    Layer = (int.Parse(valuesDic["LayerID"].ToString())),
                    WinState = false
                };
                if (valuesDic.ContainsKey("Win"))
                    currentState.WinState = bool.Parse(valuesDic["Win"].ToString());
                
                //foreach (KeyValuePair<string, System.Object> keyValuePair in valuesDic)
                //    Debug.Log("key in values dic ===== " + keyValuePair.Key);
                if (!valuesDic.ContainsKey("Edges"))
                    edgesDic = new Dictionary<string, System.Object>();
                else
                    edgesDic = (Dictionary<string, System.Object>)valuesDic["Edges"];



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
                            case "Sto":
                                currentEdge.Sto = edgeBody.Value.ToString();
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
                currentV = currentState.Layer;

                value.Add(currentState);
                //Debug.Log("layer " + currentState.Layer + " and state " + currentState.Id);
                State found;
                if (dicTarget.ContainsKey(currentState.Layer))//has this layer, add the state to the list
                {

                    found = dicTarget[currentState.Layer].Find(s => s.Id.Equals(currentState.Id));
                    if (found == null)//add the state
                    {
                        dicTarget[currentState.Layer].Add(currentState);
                    }
                    else//update the edges
                    {
                        found = currentState;
                    }

                }

            }
            //Debug.Log("dic contains " + DicByLayer.Count + " layers");
            //Debug.Log("my*********************** key is==== " + key + "  num of states ===  " + value.Count);
            if (!dicTarget.ContainsKey(currentV))
            {
                dicTarget.Add(currentV, value);//add to the dictionary
            }

            if (v == 0)
            {
                v = -1;
                GetStatesForLayer(dicTarget);
            }
        }
        catch (Exception ex)
        {
            Debug.Log("we fuck up with the database == " + ex.Message);
        }

    }//end load snapshot to dic
    private static void GameHeader_ValueChanged(object sender, ValueChangedEventArgs e,Dictionary<int,List<State>> dicTarget)
    {
        if (dicTarget == null)
            dicTarget = GameHeader.DicByLayer;
        
            int currentV = v;

            if (e.DatabaseError != null)
            {
                Debug.LogError(e.DatabaseError.Message);
                return;
            }

            // Do something with the data in args.Snapshot
            DataSnapshot snapshot = e.Snapshot;
        Dictionary<string, object> dic = (Dictionary<string, object>)snapshot.Value;
        LoadSnapshotToDictionary(dic, dicTarget, currentV);
    }//end on value changed //end of func!
    
    /*
    private static void GameHeader_ValueChanged1(object sender, ValueChangedEventArgs e)
    {


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
        //  Debug.Log(kv.Key + " * **---*** " + "val = " + kv.Value);
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
            Debug.Log("layer " + (v+1) + " and state " + currentState.Id);
            State found;
            if (GameHeader.DicByLayer.ContainsKey((v + 1)))//has this layer, add the state to the list
            {

                found = GameHeader.DicByLayer[(v + 1)].Find(s => s.Id.Equals(currentState.Id));
                if (found == null)//add the state
                {
                    GameHeader.DicByLayer[(v + 1)].Add(currentState);
                }
                else//update the edges
                {
                    found = currentState;
                }

            }

        }
        //Debug.Log("dic contains " + DicByLayer.Count + " layers");
        //Debug.Log("my*********************** key is==== " + key + "  num of states ===  " + value.Count);
        if (!GameHeader.DicByLayer.ContainsKey((v + 1)))
        {
            GameHeader.DicByLayer.Add((v + 1), value);//add to the dictionary
        }


    }//end on value changed
    private static void GameHeader_ValueChanged2(object sender, ValueChangedEventArgs e)
    {


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
        //  Debug.Log(kv.Key + " * **---*** " + "val = " + kv.Value);
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
            Debug.Log("layer " + (v+2) + " and state " + currentState.Id);
            State found;
            if (GameHeader.DicByLayer.ContainsKey((v + 2)))//has this layer, add the state to the list
            {

                found = GameHeader.DicByLayer[(v + 2)].Find(s => s.Id.Equals(currentState.Id));
                if (found == null)//add the state
                {
                    GameHeader.DicByLayer[(v + 2)].Add(currentState);
                }
                else//update the edges
                {
                    found = currentState;
                }

            }

        }
        //Debug.Log("dic contains " + DicByLayer.Count + " layers");
        //Debug.Log("my*********************** key is==== " + key + "  num of states ===  " + value.Count);
        if (!GameHeader.DicByLayer.ContainsKey((v + 2)))
        {
            GameHeader.DicByLayer.Add((v + 2), value);//add to the dictionary
        }


    }//end on value changed

    */

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
            //Debug.Log("dic is null --- suppose to be 1 time !!!");
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
        //GetStatesForLayer();//load layer
        if (!GameHeader.DicByLayer.ContainsKey(GameHeader.CurrentTurn + 1))//load this layer
        {
            //Debug.Log("GameHeader.CurrentTurn2=====" + GameHeader.CurrentTurn);
            GameHeader.GetStatesForLayer(DicByLayer);
            //foreach (KeyValuePair<int, List<State>> kv in GameHeader.DicByLayer)
            //    Debug.Log(kv.Key + " 1contains " + kv.Value.Count + " 1states ");
        }
        CurrentToken = Tokens[CurrentTurn % numPlayers];
        



    }

    public void GoToBrain()
    {
        SceneManager.LoadScene("BrainView");

    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Main");

    }





}
