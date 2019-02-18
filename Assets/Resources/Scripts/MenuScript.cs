﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public GameObject ControlCom;
    public GameObject HeadText;
    public GameObject HeadTextCom;
    public GameObject Board3;
    public GameObject Board4;
    public GameObject Board5;
    public GameObject MainPanel;
    public GameObject settingsPanel;
    public GameObject SyncPanel;
    public GameMaster gameMaster;


    // Use this for initialization
    void Start () {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBackMenu()
    {
        Debug.Log("OnBackMenu");
        MainPanel.SetActive(true);
        ControlCom.SetActive(false);
        HeadText.SetActive(false);
        HeadTextCom.SetActive(false);
        Board3.SetActive(false);
        settingsPanel.SetActive(false);
        GameHeader.OnEditWin = false;


    }

    public void OnStartNewGame()
    {
        //Debug.Log("OnStartNewGame");
        // gameMaster.SetCpuPlayer();
 

        MainPanel.SetActive(false);
        Board3.SetActive(true);
        GameHeader.OnEditWin = false;
        if (GameHeader.DicByLayer == null || (GameHeader.CurrentTurn == 0 && !GameHeader.DicByLayer.ContainsKey(0)))//first play, get the layer 0's states
        {
            //Debug.Log("GameHeader.CurrentTurn==0 and i'm loading the DIC");
            GameHeader.GetStatesForLayer();//load 0 and 1
        }

    }
    public void OnEditWinState()
    {
        //Debug.Log("OnEditWinState");
        settingsPanel.SetActive(false);
        HeadText.SetActive(false);
        ControlCom.SetActive(true);
        HeadTextCom.SetActive(true);
        Board3.SetActive(true);
        GameHeader.OnEditWin = true;
        //Debug.Log(" GameHeader.OnEditWin " + GameHeader.OnEditWin);
    }
    public void OnSettings()
    {
        Debug.Log("OnSettings");
        MainPanel.SetActive(false);
        settingsPanel.SetActive(true);

    }
    public void OnSync()
    {


    }

    public void OnExit()
    {


    }
}
