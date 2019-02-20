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
/// all States func and data manipulation are here
/// </summary>
public class StateController {
    //this class need to hold all the states in the game
    public List<State> States;

    public static List<State> WinStates;

    public List<State> NewStates;



   

    public static Dictionary<string, List<string>> StateToEdges;

    private List<bool> BoolListX;
    private List<bool> BoolListO;
    private List<bool> BoolListA;
    private List<bool> BoolListAND;
    
    private string EmptyStr;
    // Use this for initialization
    public StateController()
    {
       

        WinStates = new List<State>();
        NewStates = new List<State>();
        States = new List<State>();
        //set up the bitarray
        EmptyStr = new string('_',GameHeader.BoradSize * GameHeader.BoradSize);

        GetWinGene();


    }

    
    public void AddState()
    {


    }


    public void OnSetUp()//when need to pull States
    {

         
    }
    
    

    public void CheckState()//check if the state is new 
    {
        //TODO:if new add 
        //search in Layer Dic
        


    }
    public void GetWinGene()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseDatabase.DefaultInstance
     .GetReference("BoardSize").Child("" + GameHeader.BoradSize).Child("WinStates")
     .ValueChanged += StateController_ValueChanged;
    }
    private void StateController_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot
        DataSnapshot snapshot = e.Snapshot;

        Dictionary<string, object> dic = (Dictionary<string, object>)snapshot.Value;

        //IEnumerator enumerator = snapshot.get;
        foreach (var item in dic)
        {
            GameHeader.WinGeneSet.Add(item.Key);
            //Debug.Log("adding the key fuck! " + item.Key);
        }

    }//end on value changed
    


    
    

    public void AddWinGene(string WinStr,string Token)//add a win state from control to Header
    {
        if (string.IsNullOrEmpty(WinStr))
        {
            //Debug.Log("null == WinStr");
            return;
        }
        foreach (string WinS in GameHeader.WinGeneSet)
        {
            if(StrAndSter(WinS, WinStr,WinS.Length))
            {
                Debug.Log("WinStr already exicte");
                return;
            }
        }
        //Debug.Log($"adding {WinStr} to WinGeneSet");
        GameHeader.WinGeneSet.Add(new String(WinStr.ToCharArray()));
        //Debug.Log($"WinGeneSet in new at {GameHeader.WinGeneSet.Count} Count");


        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");


        // Get the root reference location of the database.
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        //key = mDatabaseRef.Child($"BoardSize:{GameHeader.BoradSize}").Child("Edges").Push().Key;
        
        

        //key = edge.Id;

        string json = JsonConvert.SerializeObject(WinStr);

        //Debug.Log("json==" + json);

        //mDatabaseRef.Child("BoardSize").Child(""+GameHeader.BoradSize).Child("WinStates").Child(json).SetRawJsonValueAsync("1");

        

    }

    int tok;
    string WinStr2;
    string Token2;
    BitArray BitX2;

    public void LoadStatesFromDatabase()
    {
 


    }







   

    private bool StrAndSter(string WinS, string b, int index)//is a&b=B
    {
        string STemp = "";

        for (int i = 0; i < index; i++)
        {
            if (WinS[i] == b[i])
            {
                STemp += WinS[i];
            }
            else
            {
                STemp += "_";
            }
        }
        if (WinS.Equals(STemp))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void AddWinStateToDB()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");


        // Get the root reference location of the database.
        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        mDatabaseRef.Child("BoardSize").Child(GameHeader.BoradSize.ToString()).Child("Layers").Child((GameHeader.CurrentTurn+1).ToString()).Child("States")
       .Child(GameHeader.Borad)
           .Child("Win").SetValueAsync("True");
        mDatabaseRef.Child("BoardSize").Child(GameHeader.BoradSize.ToString()).Child("Layers").Child((GameHeader.CurrentTurn + 1).ToString()).Child("States")
       .Child(GameHeader.Borad)
           .Child("LayerID").SetValueAsync((GameHeader.CurrentTurn+1));
    }
}
