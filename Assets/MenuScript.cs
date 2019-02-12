using System.Collections;
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



    // Use this for initialization
    void Start () {
		
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
        Debug.Log("OnStartNewGame");


    }
    public void OnEditWinState()
    {
        Debug.Log("OnEditWinState");
        settingsPanel.SetActive(false);
        HeadText.SetActive(false);
        ControlCom.SetActive(true);
        HeadTextCom.SetActive(true);
        Board3.SetActive(true);
        GameHeader.OnEditWin = true;

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
