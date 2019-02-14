using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public GameObject TBoard;
    public GameHeader GameHeader;
    StateController stateController;
    EdgeController edgeController;

    private Text Tt;
    private Text[] TtB;// = new Text[GameHeader.BoradSize * GameHeader.BoradSize];

    public List<Button> Buttons;
    public static Dictionary<string, Button> StrToBnt;
    public bool BEnd;

    public string PrevState;
    public string CurrnetState;

    CpuPlayer cpuPlayer;

    Button temp;
    // Use this for initialization
    void Start () {
        stateController = new StateController();
        edgeController = new EdgeController();
        Tt = TBoard.GetComponentInChildren<Text>();//get board header
        Buttons = TBoard.GetComponentsInChildren<Button>().ToList();//get currnet board buttons
        Dictionary<string, Button> StrToBnt = new Dictionary<string, Button>();
        
        foreach (Button bnt in Buttons)
        {
            if((bnt.name == "ButtonBackMenu"))
                {
                temp = bnt;
                 }
            Button bnt2 = new Button;
            StrToBnt.Add(bnt.name.Replace("Button-", ""),bnt);//set the dic

        }
        Buttons.Remove(temp);
        TtB = new Text[GameHeader.BoradSize * GameHeader.BoradSize];
        Debug.Log("GameHeader.BoradSize  == " + GameHeader.BoradSize);
        Debug.Log("TtB size  == " + TtB.Length);




        SetBoard();//reset the board

        SetCpuPlayer();

    }

    private void OnEnable()
    {
        Debug.Log("OnEnable() GameMaster");
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable() GameMaster");
    }


    // Update is called once per frame
    void Update () {

		
	}
    public static void OnCpuPress(Button Bnt)//,,like end of turn''
    {
        
    }

    public void OnPress(Button Bnt)//,,like end of turn''
    {
        Debug.Log("OnPress=>" + Bnt);
        PrevState = GetBoard();
        edgeController.AddNewEdge(edgeController.MakeEdgeId(Bnt.name.Replace("Button-", "")), PrevState, GameHeader.CurrentTurn);
        
        if (GameHeader.Dirty)//are we OnEdit1
        {
            SetBoard();
            GameHeader.Dirty = false;
        }
        if (GameHeader.OnEditWin)//are we OnEdit2
        {
            
            Bnt.GetComponentInChildren<Text>().text = GameHeader.CurrentToken;
            return;


        }
        if(GameHeader.BWin)//is the game won
        {
            Tt.text = SetNextPlayerText();
            return;
        }
        GameHeader.Borad = GetBoard();//get board
        
        Bnt.GetComponentInChildren<Text>().text = GameHeader.CurrentToken;//puts the Token

        




        //CurrnetState = GetBoard();

        OnEndTurn();

        if (GameHeader.CurrentToken != "X")//if not human player
        {
            cpuPlayer.PlayTurn();
        }



        //check if new state
        //check if new edge



    }



    public void SetCpuPlayer()
    {
        cpuPlayer = new CpuPlayer(GameHeader.Tokens[1], "", "");
    }



    //start SetBoard funs
    public void SetBoard()//clean the board
    {
        foreach (Button Bnt in Buttons)
        {
            Bnt.GetComponentInChildren<Text>().text = "_";

           // Debug.Log("set button num=" + (int.Parse(Bnt.name.Replace("Button-", ""))));
            //Debug.Log("set button text=" + Bnt.GetComponentInChildren<Text>().text);
        }

    }
    int index = 0;
    public void SetBoard(string Target)//set the board by state string
    {
        index = 0;
        foreach (Button Bnt in Buttons)
        {
            Bnt.GetComponentInChildren<Text>().text = Target[index]+"";
            index++;
         //   Debug.Log("set button num=" + (int.Parse(Bnt.name.Replace("Button-", ""))));
         //   Debug.Log("set button text=" + Bnt.GetComponentInChildren<Text>());
        }
        //Debug.Log(index + " buttons text= " + Target);
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
    string str;
    public string GetBoard()//get board data and return it as a string Layer/w-123456789...
    {

        str = "";
        if(GameHeader.OnEditWin)
        {
            str += "W-";
        }
        else
        {
            str += GameHeader.CurrentTurn + "-";
        }
        

        foreach (Button Bnt in Buttons)
        {
            str += Bnt.GetComponentInChildren<Text>().text; 
        }
        if (GameHeader.NeedToTrns)//if we use diffrent Tokens=> if Tokens!= X O @ &
        {
            foreach (string key in GameHeader.TokenTrns.Keys)
                str.Replace(key, GameHeader.TokenTrns[key]);
        }
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

   
    
    public bool StrAndSter(string a,string b,int index)//is a&b=B
    {
        string STemp ="";
        
        for (int i = 0; i < index; i++)
        {
            if(a[i]==b[i])
            {
                STemp += a[i];
            }
            else
            {
                STemp += "_";
            }
        }
        if (a.Equals(STemp))
        {
            return true;
        }
        else
        {
            return false;
        }

    }



}
