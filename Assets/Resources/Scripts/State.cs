using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {
    public string id;

    public Dictionary<string, BitArray> GeneSet = new Dictionary<string, BitArray>();

    public edge[] Edges;

    public bool WinState{ set; get; }
    private int BoardSize { set; get; }
    private int Layer { set; get; }

    State(string Board,edge[] edges,int BoardSize)
    {
        this.id = Board;
        this.Edges = edges;
        this.BoardSize = BoardSize;
        this.Layer = GameHeader.CurrentTurn;


    }

    private void Awake()
    {
        
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
