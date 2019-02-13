﻿using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class JsonManager : MonoBehaviour
{
    public List<State> Statedata;
    public static List<Edge> Edgedata { get; set; }

    public string pathStatedata;
    public string pathEdgedata;

    public void Start()
    {
        Debug.Log("Application.streamingAssetsPath== " + Application.streamingAssetsPath);
        Statedata = new List<State>();
        Edgedata = new List<Edge>();
        //data = new JsonData(1, "Alfred Jodl");
        //"jar:file://" + Application.dataPath + "!/assets/";

        pathStatedata = Path.Combine(Application.streamingAssetsPath, "Statedata.json");

        //pathStatedata = Path.Combine(Application.streamingAssetsPath + "/saved files", "Statedata.json");
        pathEdgedata = Path.Combine(Application.streamingAssetsPath + "/saved files", "Edgedata.json");
        //SerializeData();
        //DeserializeData();
    }

    //public void SerializeData()
    //{
    //    string jsonDataString = JsonUtility.ToJson(data, true);

    //    File.WriteAllText(path, jsonDataString);

    //    Debug.Log(jsonDataString);
    //}

   

    //public void DeserializeData()
    //{
    //    string loadedJsonDataString = File.ReadAllText(path);

    //    data = JsonUtility.FromJson<JsonData>(loadedJsonDataString);

    //    Debug.Log("id: " + data.id.ToString() + " | name: " + data.name);
    //}

    public void SaveWinState()
    {
        Debug.Log(" SaveWinState() " + StateController.WinStates.ToList<State>());
        foreach (State item in StateController.WinStates)
        {
            Statedata.Add(new State(item));

        }
        pathStatedata = Path.Combine(Application.streamingAssetsPath, "StateWindata.json");

        SerializeStateData();

    }

    
    public static void SerializeEdgeData(List<Edge> NewEdges)
    {
        string pathEdgedata;
        foreach (Edge edge in NewEdges)
        {
            File.Create(Application.streamingAssetsPath + @"\saved files\");
            pathEdgedata = Path.Combine(Application.streamingAssetsPath + @"\saved files\", @"" + edge.Id + ".json");
            Debug.Log("path is ="+ pathEdgedata);
            Debug.Log("edge.Id is =" + edge.Id);
            File.Create(pathEdgedata);
            string jsonDataString = JsonUtility.ToJson(edge.Id);
            Debug.Log(jsonDataString);
            File.WriteAllText(pathEdgedata, jsonDataString);

        }
        Debug.Log("end of writing data");


    }

    public void SerializeStateData()
    {
        //foreach (State item in Statedata)
        // {
        string item = "WTF";
            string jsonDataString = JsonUtility.ToJson(item);
        Debug.Log(jsonDataString);
        //File.Create(pathStatedata);
        File.WriteAllText(pathStatedata, jsonDataString);

            
       // }
        
    }

    public void DeserializeStateData()
    {
        string loadedJsonDataString = File.ReadAllText(pathStatedata);

        Statedata.Add(new State(JsonUtility.FromJson<State>(loadedJsonDataString)));

        Debug.Log("Statedata: " + Statedata.Capacity + " | Statedata.Count: " + Statedata.Count);

        Debug.Log("Statedata: " + Statedata.Capacity + " | Statedata.Count: " + Statedata.Count);
    }
}

