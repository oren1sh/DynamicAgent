﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void GoToGame()
    {
        SceneManager.LoadScene("Main");

    }



    // Update is called once per frame
    void Update () {
		
	}
}
