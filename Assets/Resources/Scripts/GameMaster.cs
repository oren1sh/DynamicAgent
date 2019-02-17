using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public GameObject TBoard;
    public GameHeader GameHeader;
    StateController  stateController;
    EdgeController edgeController;

    private Text Tt;
    private Text[] TtB;// = new Text[GameHeader.BoradSize * GameHeader.BoradSize];

    public List<Button> Buttons;
    public Dictionary<string, Button> StrToBnt;
    public bool BEnd;

    public string PrevState;
    public string CurrnetState;

    public int i;
    CpuPlayer cpuPlayer;
    EdgeRoot e;
    Button temp;
    // Use this for initialization
    IEnumerator Start () {
        
        string url = "https://dynamicagent-681fa.firebaseio.com/BoardSize/3/Layers/0/States/_________/Edges/2-X-0.json";
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            e = JsonUtility.FromJson<EdgeRoot>(www.text);
            Debug.Log("json === \n "+www.text);
            Debug.Log("from jjjjjjjjjjjjjjjjjj = " + e.Edge.Id);
        }
       
        stateController = new StateController();
        if(GameHeader.DicByLayer== null || (GameHeader.CurrentTurn==0 && !GameHeader.DicByLayer.ContainsKey(0)))//first play, get the layer 0's states
        {
            Debug.Log("GameHeader.CurrentTurn==0 and i'm loading the DIC");
            GameHeader.GetStatesForLayer();
        }

       

        edgeController = new EdgeController();
        Tt = TBoard.GetComponentInChildren<Text>();//get board header
        Buttons = TBoard.GetComponentsInChildren<Button>().ToList();//get currnet board buttons
        StrToBnt = new Dictionary<string, Button>();
        i = 0;
        foreach (Button bnt in Buttons)
        {
            if((bnt.name == "ButtonBackMenu"))
                {
                temp = bnt;
                 }
            ///Button bnt2 = new Button;
            StrToBnt.Add(bnt.name.Replace("Button-", ""), Buttons[i]);//set the dic
            //Debug.Log("StrToBnt " + StrToBnt[bnt.name.Replace("Button-", "")]);
            i++;
        }
        Buttons.Remove(temp);
        //for (i = 0; i < 9; i++)
        //{
        //    Debug.Log($"StrToBnt {i} {StrToBnt[""+1]}");
        //}
        TtB = new Text[GameHeader.BoradSize * GameHeader.BoradSize];
        //Debug.Log("GameHeader.BoradSize  == " + GameHeader.BoradSize);
        //Debug.Log("TtB size  == " + TtB.Length);




        SetBoard();//reset the board
        cpuPlayer = new CpuPlayer
        {
            StrToBnt = StrToBnt,
            name = GameHeader.Tokens[1],
            Buttons = Buttons,
            GameMaster = this,

        };
        //SetCpuPlayer();


    



    }

    private void OnEnable()
    {
        //Debug.Log("OnEnable() GameMaster");
    }
    private void OnDisable()
    {
        //Debug.Log("OnDisable() GameMaster");
    }


    // Update is called once per frame
    void Update () {
//if(e != null)
//            Debug.Log("edge from json ==== " + e.Id);

    }
    public static void OnCpuPress(Button Bnt)//,,like end of turn''
    {
        
    }

    public void OnPress(Button Bnt)//,,like end of turn''
    {
        PrevState = GetBoard();
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

        /*	if(computer)
		get put index=>the random function*/



        if (!GameHeader.CurrentToken.Equals("X"))//if not human player
        {
            //set btn by random ....
            Bnt = cpuPlayer.PlayTurn();
        }

        //GameHeader.SetNextPlayerToken
        /*	put in index*/
        string currentEdgeID = edgeController.MakeEdgeId(Bnt.name.Replace("Button-", ""));
        Edge currentEdge = edgeController.GetEdgeInLayer(currentEdgeID,GameHeader.CurrentTurn);
        if (currentEdge == null)//if not exists, add new 
        {
            Debug.Log("edge " + currentEdgeID + " is fuckin null!!!");
            currentEdge = edgeController.AddNewEdge(currentEdgeID, PrevState, GameHeader.CurrentTurn);
        }
        edgeController.CheckEdges.Add(currentEdge);//add to checked list
        Bnt.GetComponentInChildren<Text>().text = GameHeader.CurrentToken;
        Bnt.interactable = false;

        GameHeader.Borad = GetBoard();//get board


        /*	check win */
        if (CheckWin())//if someone wins, end it, else, continue play
        {
            Tt.text = "player " + GameHeader.CurrentToken + " has won";
            DisableAllButtons();
            //Debug.Log("checked edges === ");
            //string output = "";
            //foreach (Edge edge in edgeController.CheckEdges)
            //    output += edge.Id + " state == " + edge.Sfrom + "   layer = " + edge.fromlayer+"\n";
            //Debug.Log(output);
            edgeController.UpdateAllCheckedEdges();

            return;
        }
        //TODO: run on win list with currnet state
        /* if(win)
            end */



        /*  go to other player -set other player flag ==> player   */
        GameHeader.SetNextPlayerToken();
        Debug.Log("GameHeader.CurrentTurn1=====" + GameHeader.CurrentTurn);

        if (!GameHeader.DicByLayer.ContainsKey(GameHeader.CurrentTurn))//load this layer
        {
            Debug.Log("GameHeader.CurrentTurn2=====" + GameHeader.CurrentTurn);
            GameHeader.GetStatesForLayer();
            foreach (KeyValuePair<int, List<State>> kv in GameHeader.DicByLayer)
                Debug.Log(kv.Key + " 1contains " + kv.Value.Count + " 1states ");
        }
           
        ////Debug.Log("current status of layers dic:");
        //foreach (KeyValuePair<int, List<State>> kv in stateController.DicByLayer)
        //    Debug.Log(kv.Key + " contains " + kv.Value.Count + " states ");


        /* if (computer)
            put token */
        if (!GameHeader.CurrentToken.Equals("X"))//if not human player
        {
            OnPress(null);
        }

        //end nadav 
        /*
        Debug.Log("OnPress=>" + Bnt);
        PrevState = GetBoard();

        //CurrnetState = GetBoard();

        OnEndTurn();




        //check if new state
        //check if new edge


        */
    }

    private void DisableAllButtons()
    {
        foreach (Button b in Buttons)
            b.interactable = false;
    }

    private bool CheckWin()
    {
        //check if womeone wins
        //if yes, print it and return true
        //the default is to check if the board is full and if yes, print no one wins and return false
        //the global default is return false
        //if (GameHeader.WinGeneSet.Contains("W-" + GameHeader.Borad.Remove(0, 2)))
        string win, b;
        b = GameHeader.Borad;
        foreach (string a in GameHeader.WinGeneSet)
        {
            //Debug.Log("a =========>" + a);
            win = a.Replace("\"","").Remove(0, a.IndexOf("-") ) ;//a=" W-_______"
            if (StrAndSter(win, b))
            {
                GameHeader.Win = GameHeader.CurrentToken;
                GameHeader.BWin = true;
                return true;
            }

        }
        

        /* if full board ==> print no wins and end the game */
        if (!GameHeader.Borad.Contains("_"))//no one wins
        {
            Tt.text = "no one win!";
            GameHeader.Win = "no";
            //ToDO:add weight to edges = 1
            GameHeader.BWin = true;
            return true;
        }
        return false;
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
        if (GameHeader.OnEditWin)
        {
            str += "W-";
        }
        //else
        //{
        //    str += GameHeader.CurrentTurn + "-";
        //}


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


    //a = _X___X__
    //b = _X_O_X__
    //STemp = _X___X__
    public bool StrAndSter(string a,string b)//is a&b=B
    {
    string STemp ="";
        //Debug.Log("a length ======= " + a.Length + " and b length ====== " + b.Length);
        //Debug.Log("a is : " + a + " and b is : " + b);
    for (int i = 0; i < a.Length; i++)
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
