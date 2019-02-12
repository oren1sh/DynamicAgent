using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlComScript : MonoBehaviour {

    public Text HeadText;


    GameMaster Master;
    StateController stateController;


    public string WinState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        GameHeader.OnEditWin = true;
    }


    private void OnDisable()
    {
        Debug.Log("OnDisable");
        GameHeader.OnEditWin = false;
    }


    public void OnSet()
    {
        Debug.Log("OnSet");
        WinState = Master.GetBoard();


    }

    public void OnOk()
    {
        Debug.Log("OnOk");



    }

    public void OnSwith()
    {
        Debug.Log("OnSwith");
        GameHeader.NextPlayer();
        HeadText.text = @"now put {GameHeader.CurrentToken} and press SET when ready";


    }
}
