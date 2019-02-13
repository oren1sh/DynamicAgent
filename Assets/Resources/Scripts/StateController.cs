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

    public Dictionary<int, List<State>> DicByLayer;

    private List<bool> BoolListX;
    private List<bool> BoolListO;
    private List<bool> BoolListA;
    private List<bool> BoolListAND;
    private BitArray BitX;
    private BitArray BitO;
    private BitArray BitA;
    private BitArray BitAND;
    private string EmptyStr;
    // Use this for initialization
    public StateController()
    {
        WinStates = new List<State>();
        //set up the bitarray
        BitX = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
        BitO = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
        BitA = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize); 
        BitAND = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
        EmptyStr = new string('_',GameHeader.BoradSize * GameHeader.BoradSize);

        Debug.Log("BitX " + BitX.Length);
        Debug.Log("BitO " + BitO.Length);
        Debug.Log("BitA " + BitA.Length);
        Debug.Log("BitAND " + BitAND.Length);


    }


    public void OnSetUp()//when need to pull States
    {

         
    }
    
    private bool SearchInLayers(string Target)
    {
        States = DicByLayer[GameHeader.CurrentTurn];
        foreach (State S in States)
        {
            if (S.Id == Target)
                return true;

        }
        return false;

    }

    public void CheckState()//check if the state is new 
    {
        //TODO:if new add 
        //search in Layer Dic
        


    }

    public void AddWinGene(string WinStr,string Token)//add a win state from control to Header
    {
        if(EmptyStr == WinStr)
        {
            Debug.Log("EmptyStr == WinStr");
            return;
        }
        string DebugStr ="";
        int i = 0;
        foreach (char t in WinStr)
        {
            //Debug.Log(" i = " + i);
            //Debug.Log(" t = " + t + "Token = " + Token + "t.Equals(char.Parse(Token)) = " + t.Equals(char.Parse(Token)));
            BitX[i] = t.Equals(char.Parse(Token));
            DebugStr += t.Equals(char.Parse(Token));
            DebugStr += "-";
            i++;
        }

        if (!GameHeader.WinGeneSet[Token].Exists(x => BitArrayComp(x, BitX)))
        {
            
            GameHeader.WinGeneSet[Token].Add(new BitArray(BitX));
            SetSaveDate(WinStr, Token, BitX);
            Debug.Log("GameHeader.WinGeneSet[Token].Count " + GameHeader.WinGeneSet[Token].Count);

        }

        BitX.SetAll(false);//reset the bitarray
    }

    int tok;
    string WinStr2;
    string Token2;
    BitArray BitX2;

    public void SetSaveDate(string WinStr, string Token,BitArray BitX)
    {
        WinStr2= WinStr;
        Token2= Token;
        BitX2=new BitArray(BitX);

        Debug.Log("SetSaveDate(WinStr, Token, BitX)");
        switch(Token)
        {
            case "X":
                tok = 0;
                break;
            case "O":
                tok = 1;
                break;
            case "@":
                tok = 2;
                break;
            case "&":
                tok = 3;
                break;
        }
    
        WinStates.Add(new State("M-" + WinStr2, true, BitX2, tok));


    }



    public static bool BitArrayComp(BitArray a , BitArray b)
    {
        
        for (int i = 0; i < a.Count; i++)
        {
            if (!(a[i] == b[i]))
                return false;
        }
        return true;

    }



    public Dictionary<string, BitArray> StringToBitArray(string Target)//add a win state from control
    {
        int i = 0;
        foreach (char t in Target)
        {
            BitX.Set(i, t.Equals("X"));
            BitO.Set(i, t.Equals("O"));
            BitA.Set(i, t.Equals("@"));
            BitAND.Set(i, t.Equals("&"));
            i++;
        }

        Dictionary<string, BitArray> DicToRet = new Dictionary<string, BitArray>();

        switch (GameHeader.numPlayers)
        {
            case 2:
                DicToRet.Add("X", BitX);
                DicToRet.Add("O", BitO);
                break;
            case 3:
                DicToRet.Add("X", BitX);
                DicToRet.Add("O", BitO);
                DicToRet.Add("@", BitA);
                break;
            case 4:
                DicToRet.Add("X", BitX);
                DicToRet.Add("O", BitO);
                DicToRet.Add("@", BitA);
                DicToRet.Add("&", BitAND);
                break;
            default:
                Debug.Log("error!!!  number of player is== " + GameHeader.numPlayers);
                break;
        }
        BitX.SetAll(false);
        BitO.SetAll(false);
        BitA.SetAll(false);
        BitAND.SetAll(false);

        return DicToRet;

    }




}
