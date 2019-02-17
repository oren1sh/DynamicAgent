using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.AccessControl;

public class JsonManager : MonoBehaviour
{
    public List<State> Statedata;
    public static List<Edge> Edgedata { get; set; }

    public string pathStatedata;
    public string pathEdgedata;

    public void Start()
    {
        //Debug.Log("Application.streamingAssetsPath== " + Application.streamingAssetsPath);
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

    
     static bob bob1;
    static FileStream fileStream;
    public static void SerializeEdgeData(List<Edge> NewEdges)
    {
        bob1 = new bob();
        string pathEdgedata;
        foreach (Edge edge in NewEdges)
        {
            bob1.Id = edge.Id;
            bob1.Sfrom = edge.Sfrom;
            bob1.Sto = edge.Sto;

            pathEdgedata = Directory.CreateDirectory(Application.streamingAssetsPath + @"/saved files" ).FullName;
            //File.Create(Application.streamingAssetsPath + @" / saved files");
            //pathEdgedata = Path.Combine(Application.streamingAssetsPath + @"/saved files");//, @"" + bob1 + ".json");
            Debug.Log("path is ="+ pathEdgedata);
            Debug.Log("edge.Id is =" + edge.Id);
            //File.Create(pathEdgedata + @"/" + bob1.Id + ".txt");
            Debug.Log("File.Create" + File.Create(pathEdgedata + @"/" + bob1.Id + ".txt"));
            File.SetAttributes(pathEdgedata, FileAttributes.Normal);
            //File.WriteAllText(pathEdgedata, bob1.ToString());
            //File.Create(pathEdgedata);
            string jsonDataString = JsonUtility.ToJson(edge.ToString());
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

    
}
[Serializable]
public class bob
{
    public string Id { get; set; }//bnt-token-fromlayer
    public string Sfrom { get; set; }
    public string Sto { get; set; }

}

