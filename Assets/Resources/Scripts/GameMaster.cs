using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public GameObject TBoard;


    private Text Tt;
    private Text[] TtB;// = new Text[GameHeader.BoradSize * GameHeader.BoradSize];
    private List<Button> Buttons;

    GameHeader GameHeader;

   
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




    public void SetBoard()
    {
        foreach (Button Bnt in Buttons)
        {
            Bnt.GetComponentInChildren<Text>().text = "";

            Debug.Log("set button num=" + (int.Parse(Bnt.name.Replace("Button-", ""))));
            Debug.Log("set button text=" + Bnt.GetComponentInChildren<Text>());
        }

    }

    public string GetBoard()
    {
        string str;

        foreach (Button Bnt in Buttons)
        {
            int num = int.Parse(Bnt.name.Replace("Button-", ""));
            //Debug.Log("int num is == " + num);
            //Debug.Log("get button num= " + (int.Parse(Bnt.name.Replace("Button-", ""))));
            //Debug.Log("get button text= " + Bnt.GetComponentInChildren<Text>());

            TtB[num-1] = Bnt.GetComponentInChildren<Text>();
            
        }
        str = TtB.ToString();
        if (GameHeader.NeedToTrns)
        {
            foreach (string key in GameHeader.TokenTrns.Keys)
                str.Replace(key, GameHeader.TokenTrns[key]);
        }

        return str;
    }

    private string SetText()
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

        GameHeader.NextPlayer();
        Tt.text = SetText();


    }



}
