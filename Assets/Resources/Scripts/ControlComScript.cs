using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlComScript : MonoBehaviour {

    public GameMaster Master;
    public JsonManager Json;

    StateController stateController;

    public Text HeadText;
    public List<BitArray> CurrnetListOfBit;
    public Button next;
    public Button prev;

    int i = 0;



    public string WinState;

	// Use this for initialization
	void Start () {
        stateController = new StateController();
        next.interactable = false;
        prev.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {

       




    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        GameHeader.OnEditWin = true;
        Master.SetBoard();//clean board
        HeadText.text = "now put " + GameHeader.CurrentToken + " and press SET when ready to set a win State";
    }


    private void OnDisable()
    {
        Debug.Log("OnDisable");
        Master.SetBoard();//clean board
        GameHeader.OnEditWin = false;
    }


    public void OnSet()
    {
        //TODO: save win state to database
        Debug.Log("OnSet");
        WinState = Master.GetBoard();
        Debug.Log("WinState " + WinState);
       
        stateController.AddWinGene(WinState, GameHeader.CurrentToken);
        Master.SetBoard();//clean board

        next.interactable = true;
        prev.interactable = true;
    }

    public void OnOk()
    {
        Debug.Log("OnOk");

        //Json.SaveWinState();

    }

    public void OnRestWin()
    {
        Debug.Log("OnRestWin");



    }

    public void OnNextPress()
    {
        
        GameHeader.Dirty = true;
        Debug.Log("OnNextPress");//do we a winGene
        LoadGene();
        
        Master.SetBoard();
        Master.SetBoard(CurrnetListOfBit[mod(i++,CurrnetListOfBit.Count)], GameHeader.CurrentToken);//clean board
        

        



    }

    public void OnPrevPress()
    {
        GameHeader.Dirty = true;
        Debug.Log("OnPrevPress");
        LoadGene();
 
        Master.SetBoard();

        Master.SetBoard(CurrnetListOfBit[mod(i--,CurrnetListOfBit.Count)], GameHeader.CurrentToken);//clean board

    }

    public int mod(int x, int m)
    {
        if (x == 0 || m == 0)
        {
            return 0;
        }
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    public void OnSwith()
    {
        Debug.Log("OnSwith");
        Master.SetBoard();//clean board
        GameHeader.SetNextPlayerToken();
        HeadText.text = "now put " + GameHeader.CurrentToken + " and press SET when ready to set a win State";
        LoadGene();

    }

    private void LoadGene()
    {
        //TODO: fix or delete
       
        if (CurrnetListOfBit.Count == null)
        {
            try
            {
                //CurrnetListOfBit = GameHeader.WinGeneSet[GameHeader.CurrentToken];
                foreach (BitArray bit in CurrnetListOfBit)
                {
                    Debug.Log("bit " + bit.Count);
                }
              
            }
            catch
            {
                Debug.Log("CurrnetListOfBit is Null!!");
           
            }

        }
        
    }

}
