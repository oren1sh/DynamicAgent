using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHeader : MonoBehaviour {


    public GameObject SceneObjects;


    public static int numPlayers { set; get; }
    public static int CurrentTurn { set; get; }
    public static string Borad { set; get; }
    public static int BoradSize { set; get; }
    public static Dictionary<string,List<BitArray>> WinGeneSet { get; set; }//key=token,vale = bitarray
    public static Dictionary<string, string> TokenTrns;
    public static string[] Tokens{ set; get; }
    public static string Win { set; get; }
    public static bool BWin { set; get; }
    public static string CurrentToken { get; set;}
    public static bool OnEditWin { get; set; }
    public static bool NeedToTrns { get; set; }
    public static bool Dirty { get; set; }

    private void Awake()
    {
        //general setup for a game(3X3)
        CurrentToken = "X";
        WinGeneSet = new Dictionary<string, List<BitArray>>(4);
        Tokens = new string[4];
        Tokens[0] = "X";
        WinGeneSet.Add("X", new List<BitArray>());
        Tokens[1] = "O";
        WinGeneSet.Add("O", new List<BitArray>());
        Tokens[2] = "@";
        WinGeneSet.Add("@", new List<BitArray>());
        Tokens[3] = "&";
        WinGeneSet.Add("&", new List<BitArray>());
        Debug.Log("Tokens" + Tokens);
        Debug.Log("WinGeneSet" + WinGeneSet);
        numPlayers = 2;
        CurrentTurn = 0;
        BoradSize = 3;
        Borad = new string('_', BoradSize * BoradSize);
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

 


    // Use this for initialization
    void Start () {

        



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
