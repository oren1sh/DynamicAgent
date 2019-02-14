
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;



public class FireSupport : MonoBehaviour
{
    DatabaseReference mDatabaseRef;
    string key;
    void Start()
    {
        
    }
    //FireSupport()
    //{
    //    // Set this before calling into the realtime database.
    //    FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");


    //    // Get the root reference location of the database.
    //    DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;


    //    key = "";

    //}

    private void Awake()
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");


        // Get the root reference location of the database.
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;


        key = "";
    }



    public void SaveNewEdge(Edge edge)
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");


        // Get the root reference location of the database.
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        key = mDatabaseRef.Child($"BoardSize:{GameHeader.BoradSize}").Child("Edges").Push().Key;
        Edge ETemp = new Edge(edge);
        string Stemp = ETemp.ToString();

        key = edge.Id;

        string json = JsonConvert.SerializeObject(ETemp);

        Debug.Log("json==" + json);

        mDatabaseRef.Child("NewEdges").SetRawJsonValueAsync(json);

        //var setting = new JsonSerializerSettings();
        //setting.Formatting = Formatting.Indented;
        //setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //
        //// write
        //var accountsFromCode = new List<Account> { accountJames, accountOnion };
        //var json = JsonConvert.SerializeObject(accountsFromCode, setting);
        //var path = Path.Combine(Application.dataPath, "hi.json");
        //File.WriteAllText(path, json);


        Debug.Log("json==" + json);
        //
        //mDatabaseRef.Child("NewEdges").SetValueAsync(childUpdates);

        mDatabaseRef.Child("NewEdges").SetRawJsonValueAsync(json);


        //User user = new User(name, email);
        //string json = JsonUtility.ToJson(user);

        //mDatabaseRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
}