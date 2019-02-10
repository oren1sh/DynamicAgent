using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHeader : MonoBehaviour {

    public static int numPlayers { set; get; }
    public static int CurrentTurn { set; get; }
    public static string Borad { set; get; }
    public static int BoradSize { set; get; }
    public static Dictionary<string,Dictionary<int,BitArray>> WinGeneSet;
    public static string[] Tokens{ set; get; }
    public static string Win { set; get; }
    public static bool BWin { set; get; }
    public static string CurrentToken { get; set;}
    public static bool OnEditWin { get; set; }


    private void Awake()
    {
        CurrentToken = "X";
        Tokens = new string[4];
        Tokens[0] = "X";
        Tokens[1] = "O";
        Tokens[2] = "@";
        Tokens[3] = "&";
        Debug.Log("Tokens" + Tokens);
        numPlayers = 2;
        CurrentTurn = 0;
        BoradSize = 3;
        Borad = new string('_', BoradSize * BoradSize);
        BWin = false;
        OnEditWin = false;
        Debug.Log("numPlayers" + numPlayers);
        Debug.Log("CurrentTurn" + CurrentTurn);
        Debug.Log("BoradSize" + BoradSize);
        Debug.Log("Borad" + Borad);
        Debug.Log("BWin" + BWin);
    }

    //GeneSet

    //    Dictionary<int, Transform> childDic = new Dictionary<int, Transform>();
    //childDic.Add(childIndex, t);
    //    childIndex++;
    //    gamesTransformsLoadedIntoScreen2.Add(t.gameObject.name, childDic);

    // Use this for initialization
    void Start () {

        



    }
    private static void NextTurn()
    {
        CurrentTurn += 1;


    }


    public static void NextPlayer()
    {
        NextTurn();
        CurrentToken = Tokens[CurrentTurn % numPlayers];


    }

    public void NextPlayer(TextAlignment t)
    {
        NextTurn();
        CurrentToken = Tokens[CurrentTurn % numPlayers];


    }


    // Update is called once per frame
    void Update () {
		
	}
}
