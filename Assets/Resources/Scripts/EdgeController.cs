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
    public static List<Edge> CheckEdges;
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
        Debug.Log("MakeEdgeId");
        STemp = "";
        STemp = $"{ButtonName}-{GameHeader.CurrentToken}-{GameHeader.CurrentTurn}";
        return STemp;

    }

    private void AddEdge(string Id, string Sfrom, string Sto, int fromlayer)
    {
        ETemp = new Edge(Id, Sfrom, Sto, fromlayer);

        //bnt-token-fromlayer

    }
    FireSupport fireSupport = new FireSupport();

    public void AddNewEdge(string Id, string Sfrom, int fromlayer)
    {
        Debug.Log("AddNewEdge");
        foreach (Edge item in AllEdges)
        {
            if (item.Id == Id)
            {
                Debug.Log("edge with id: " + Id + "already exict");
                return;
            }
        }
        ETemp = new Edge();
        ETemp.Id = Id;
        ETemp.Sfrom = Sfrom;
        ETemp.fromlayer = fromlayer;

        NewEdges.Add(ETemp);

        SaveNewEdge(ETemp);
        //bnt-token-fromlayer
        //JsonManager.SerializeEdgeData(NewEdges);
        //fireSupport.SaveNewEdge(ETemp);
    }


    string key;

    public void SaveNewEdge(Edge edge)
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");


        // Get the root reference location of the database.
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        key = mDatabaseRef.Child($"BoardSize:{GameHeader.BoradSize}").Child("Edges").Push().Key;
        Edge ETemp = new Edge(edge);
        string Stemp = ETemp.ToString();

        //key = edge.Id;

        string json = JsonConvert.SerializeObject(ETemp);

        Debug.Log("json==" + json);

        mDatabaseRef.Child("BoardSize").Child(ETemp.BoardSize.ToString())
            .Child("Edges").Child(ETemp.Id).SetRawJsonValueAsync(json);

        //mDatabaseRef.SetValueAsync($"BoardSize/{GameHeader.BoradSize.ToString()} " +
        //    $"Layer/{ETemp.fromlayer.ToString()}" +
        //    $"ID/{ETemp.Id}" , json);
        /*
         "BoardSize" : [ {
                 "Layer" : [ {
                        "ID" : {
                            "1-X-0" : {
         */

    }

    public void LoadEdgeLists()
    {
        int boardSize = GameHeader.BoradSize;
        int TargetLayer = GameHeader.CurrentTurn;
        //AllEdges;
        //CheckEdges;
        //NewEdges;
        //EdgeByLayer;
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");


        // Get the root reference location of the database.
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseDatabase.DefaultInstance
                    .GetReference("BoardSize").Child(boardSize.ToString()).Child("Edges")
                    .GetValueAsync().ContinueWith(task => {
                                  if (task.IsFaulted)
                                  {
                                          // Handle the error...
                                      }
                                  else if (task.IsCompleted)
                                  {
                                      DataSnapshot snapshot = task.Result;
                            Debug.Log("we get that from dataBase === "+snapshot.ToString());
                            // Do something with snapshot...

                        }
                              });



    }






}