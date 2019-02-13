using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public GameObject TBoard;
    public GameHeader GameHeader;

    
    private Text Tt;
    private Text[] TtB;// = new Text[GameHeader.BoradSize * GameHeader.BoradSize];

    public List<Button> Buttons;

    

   
    // Use this for initialization
    void Start () {
        Tt = TBoard.GetComponentInChildren<Text>();
        Buttons = TBoard.GetComponentsInChildren<Button>().ToList();
        
        TtB = new Text[GameHeader.BoradSize * GameHeader.BoradSize];
        Debug.Log("GameHeader.BoradSize  == " + GameHeader.BoradSize);
        Debug.Log("TtB size  == " + TtB.Length);




        SetBoard();

    }

    // Update is called once per frame
    void Update () {

		
	}

    public void OnPress(Button Bnt)//turn swich
    {
        Debug.Log("OnPress=>" + Bnt);
        if (GameHeader.Dirty)
        {
            SetBoard();
            GameHeader.Dirty = false;
        }
        if (GameHeader.OnEditWin)
        {
            
            Bnt.GetComponentInChildren<Text>().text = GameHeader.CurrentToken;
            return;


        }



        
        Bnt.GetComponentInChildren<Text>().text = GameHeader.CurrentToken;

        GetBoard();

        //TODO:check Win is win
        //check if new state
        //check if new edge

        

    }



    //start SetBoard funs
    public void SetBoard()//clean the board
    {
        foreach (Button Bnt in Buttons)
        {
            Bnt.GetComponentInChildren<Text>().text = "_";

            Debug.Log("set button num=" + (int.Parse(Bnt.name.Replace("Button-", ""))));
            Debug.Log("set button text=" + Bnt.GetComponentInChildren<Text>().text);
        }

    }

    public void SetBoard(string Target)//set the board by state string
    {
        int index = 0;
        foreach (Button Bnt in Buttons)
        {
            Bnt.GetComponentInChildren<Text>().text = Target[index]+"";
            index++;
            Debug.Log("set button num=" + (int.Parse(Bnt.name.Replace("Button-", ""))));
            Debug.Log("set button text=" + Bnt.GetComponentInChildren<Text>());
        }
        Debug.Log(index + " buttons text= " + Target);
    }
    
    public void SetBoard(BitArray Target , string token)//set the board by bitarray 
    {
        int index = 0;
        
        foreach (Button Bnt in Buttons)
        {
            if (Target[index])
            {
                Bnt.GetComponentInChildren<Text>().text = token;
            }
            
            
            Debug.Log(index + " buttons text= " + Target[index]);
            Debug.Log("set button num=" + (int.Parse(Bnt.name.Replace("Button-", ""))));
            Debug.Log("set button text=" + Bnt.GetComponentInChildren<Text>().text);
            index++;
        }
        
    }

    //end SetBoard funs

    public string GetBoard()//get board data and return it as a string
    {

        string str = "";


        foreach (Button Bnt in Buttons)
        {
            int num = int.Parse(Bnt.name.Replace("Button-", ""));
            Debug.Log("num = " + num);

          
            str += Bnt.GetComponentInChildren<Text>().text;

            
            Debug.Log("Bnt = " + Bnt.GetComponentInChildren<Text>().text);
            Debug.Log("str.Insert(num - 1   " + str[num-1]);
            Debug.Log("num = " + num);

        }
        if (GameHeader.NeedToTrns)//if we use diffrent Tokens=> if Tokens!= X O @ &
        {
            foreach (string key in GameHeader.TokenTrns.Keys)
                str.Replace(key, GameHeader.TokenTrns[key]);
        }
        Debug.Log("GetBoard()==============================str = " + str);
        return str;
    }

    private string SetNextPlayerText()
    {
        if(GameHeader.BWin)
        {
            return "" + GameHeader.CurrentToken + " Has Won!";

        }

        return "it's " + GameHeader.CurrentToken + " turn!";
    }


    public void OnEndTurn()
    {
        Debug.Log("OnEndTurn()");

        GameHeader.SetNextPlayerToken();
        Tt.text = SetNextPlayerText();


    }



}
