using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlComScript : MonoBehaviour {

    GameMaster master;

    public string WinState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        GameHeader.OnEditWin = true;
    }


    private void OnDisable()
    {
        GameHeader.OnEditWin = false;
    }


    public void OnSet()
    {
        WinState = master.GetBoard();


    }

    public void OnOk()
    {



    }

    public void OnSwith()
    {



    }
}
