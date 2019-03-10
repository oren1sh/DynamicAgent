using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// all edge func and data manipulation are here
/// </summary>
public class EdgeController
{
    public static List<Edge> AllEdges;
    public List<Edge> CheckEdges;
    public List<Edge> NewEdges;
    public Dictionary<int, List<Edge>> EdgeByLayer;

    public Edge ETemp;
    public string STemp;

    private enum list{
        AllEdges,
        CheckEdges,
        NewEdges
    }

    private List<bool> BoolListX;
    private List<bool> BoolListO;
    private List<bool> BoolListA;
    private List<bool> BoolListAND;



    public EdgeController()
    {
        AllEdges = new List<Edge>();
        CheckEdges = new List<Edge>();
        NewEdges = new List<Edge>();
        EdgeByLayer = new Dictionary<int, List<Edge>>();


    }

    private void AddEdgeToList(Edge edge , list chose)
    {
        if(chose == list.CheckEdges)//if check so just point at it
        {
            ETemp = edge;
        }
        else
        {
            ETemp = new Edge(edge);
        }
        

        switch (chose)
        {
            case list.AllEdges:
                AllEdges.Add(ETemp);
                break;
            case list.CheckEdges:
                CheckEdges.Add(ETemp);
                break;
            case list.NewEdges:
                NewEdges.Add(ETemp);
                break;
            default:
                Debug.Log("AddEdgeToList faild");
                break;

        }


    }

    
    public string MakeEdgeId(string ButtonName)
    {
        //Debug.Log("MakeEdgeId");
        STemp = "";
        STemp = $"{ButtonName}-{GameHeader.CurrentToken}-{GameHeader.CurrentTurn}";
        return STemp;

    }
    public void UpdateAllCheckedEdges()
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
        bool check = true;
        for (int i = 0; i < CheckEdges.Count; i++, check = true)
        {

            if (GameHeader.Win.Equals("no")&&check)//teko
            {
                CheckEdges[i].TotalEven++;
                CheckEdges[i].TotalPass++;
                CheckEdges[i].Weight++;
                check=false;//go to next edge
            }
            if (CheckEdges[i].Id.Split('-')[1].Equals(GameHeader.Win)&&check)//wins
            {
                CheckEdges[i].TotalWin++;
                CheckEdges[i].TotalPass++;
                CheckEdges[i].Weight--;
                check=false;
            }
            if (!CheckEdges[i].Id.Split('-')[1].Equals(GameHeader.Win)&&check)//loose
            {
                CheckEdges[i].TotalLost++;
                CheckEdges[i].TotalPass++;
                CheckEdges[i].Weight+=2;
                check=false;
            }


            //update in database:
            string key = mDatabaseRef.Child("BoardSize")
                .Child("" + GameHeader.BoradSize).Child("Layers")
                .Child(CheckEdges[i].fromlayer.ToString())
                .Child("States")
                .Child(CheckEdges[i].Sfrom)
                .Child("Edges")
                .Child(CheckEdges[i].Id).Key;
            Edge entry = CheckEdges[i];
            Dictionary<string, System.Object> entryValues = entry.ToDictionary();

            string dicKey = "BoardSize/" + GameHeader.BoradSize.ToString() + "/Layers/" + entry.fromlayer.ToString() + "/States/" + entry.Sfrom + "/Edges/";
            //Debug.Log("add the key " + dicKey);
            try
            {
                childUpdates.Add(dicKey + key, entryValues);//give ==>ArgumentException: An item with the same key has already been added. Key

            }
            catch
            {
                childUpdates[dicKey + key] = entryValues;
            }
                       
        }
        //Debug.Log("update in DB:");
        //foreach (KeyValuePair<string, System.Object> keyValuePair in childUpdates)
          //  Debug.Log("key ===" + keyValuePair.Key + " value ===== wait for it");
        //update all the dictionary
         mDatabaseRef.UpdateChildrenAsync(childUpdates);

    }
    private void AddEdge(string Id, string Sfrom, string Sto, int fromlayer)
    {
        ETemp = new Edge(Id, Sfrom, Sto, fromlayer);

        //bnt-token-fromlayer

    }
    //FireSupport fireSupport = new FireSupport();

    public Edge AddNewEdge(string Id, string Sfrom, int fromlayer)
    {
        //Debug.Log("old ==== " + GameHeader.Borad);

        ETemp = new Edge();
        ETemp.Id = Id;
        System.Text.StringBuilder newBoard = new System.Text.StringBuilder(GameHeader.Borad);
        newBoard[int.Parse(Id[0].ToString())] = Id[2];
        string newBoard2 = newBoard.ToString();      
        
        //Debug.Log("the new one will be    ====      " + newBoard2);
        ETemp.Sto = newBoard2;
        ETemp.Sfrom = Sfrom;
        
        ETemp.fromlayer = fromlayer;
        ETemp.Weight = (GameHeader.BoradSize * GameHeader.BoradSize);
        NewEdges.Add(ETemp);//add to my list

        SaveNewEdge(ETemp);//add to database
        
        return ETemp;
    }
    public Edge GetEdgeInLayer(string edgeID,int layer)
    {
        List<State> states = new List<State>();
       // Debug.Log("looking for layer " + layer + " in the dic");
        //foreach (KeyValuePair<int, List<State>> kv in GameHeader.DicByLayer)
        //    Debug.Log(kv.Key+" * **---*** " + "cntt = " + kv.Value.Count);
        //Debug.Log("--------------------end of dic -------------");
        if(GameHeader.DicByLayer.ContainsKey(layer))
            states = GameHeader.DicByLayer[layer];//get states in layer
        //Debug.Log("states in layer " + layer + " === " + states.Count);
        Edge output;

        foreach (State state in states)//check in all states if they have the edge
        {
            //Debug.Log("state === " + state.Id + " and list of edges === ");
            //foreach (Edge e in state.Edges)
            //    Debug.Log(e.Id + "=?" + edgeID);
            //Debug.Log("===============================================");
            
            output = state.Edges.Find(e => e.Id.Equals(edgeID));
            if (output != null)//edge found
                return output;
        }
        return null;
    }

    string key;

    public void SaveNewEdge(Edge edge)
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");


        // Get the root reference location of the database.
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        //key = mDatabaseRef.Child($"BoardSize:{GameHeader.BoradSize}").Child("Edges").Push().Key;
        Edge ETemp = new Edge(edge);
        string Stemp = ETemp.ToString();

        //key = edge.Id;

        string json = JsonConvert.SerializeObject(ETemp);

        //Debug.Log("json==" + json);
        State currentState = new State { Id = GameHeader.Borad,
                                         Layer = GameHeader.CurrentTurn};

        mDatabaseRef.Child("BoardSize").Child(GameHeader.BoradSize.ToString()).Child("Layers").Child((GameHeader.CurrentTurn).ToString()).Child("States")
        .Child(currentState.Id)
            .Child("LayerID").SetValueAsync(currentState.Layer);
        
        mDatabaseRef.Child("BoardSize").Child(GameHeader.BoradSize.ToString()).Child("Layers").Child((GameHeader.CurrentTurn).ToString()).Child("States")
        .Child(currentState.Id)
            .Child("Edges").Child(ETemp.Id).SetRawJsonValueAsync(json);

        

    }
    






}