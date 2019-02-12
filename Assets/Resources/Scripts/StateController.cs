using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateController : MonoBehaviour {
    //this class need to hold all the states in the game
    public List<State> States;

    public List<State> WinStates;

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
    
    // Use this for initialization
    void Start()
    {

        //set up the bitarray
        BitX = new BitArray(GameHeader.BoradSize*GameHeader.BoradSize);
        BitO = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
        BitA = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize); 
        BitAND = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize); 


    }


    public void OnSetUp()//when need to pull States
    {

         
    }
    private bool SearchInLayers(string Target)
    {
        States = DicByLayer[GameHeader.CurrentTurn];
        foreach (State S in States)
        {
            if (S.id == Target)
                return true;

        }
        return false;

    }

    public void CheckState()//check if the state is new 
    {
        //TODO:if new add 
        //search in Layer Dic
        


    }

    public void AddWinState(string WinStr)//add a win state from control
    {
        


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

        return DicToRet;

    }



    
	
	// Update is called once per frame
	void Update () {
		
	}
}
