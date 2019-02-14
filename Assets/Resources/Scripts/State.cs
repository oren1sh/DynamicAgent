using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class State{
    public string Id { set; get; }//layer/W-123456789....
    public bool WinState{ set; get; }
    public int BoardSize { set; get; }
    public int Layer { set; get; }
    public List<string> Edges { set; get; }
    
    //public BitArray[] GeneSet { set; get; }

    //public BitArray GeneSetX { set; get; }



    public State()
    {

    }

    public State(string id, List<string> Edges, bool WinState, int BoardSize, int Layer, BitArray[] GeneSet)
    {
        this.Id = Id;
        this.WinState = WinState;
        this.BoardSize = BoardSize;
        this.Layer = Layer;
    }

    

    public State(State state)
    {
        this.Id = state.Id;
        this.WinState = state.WinState;

    }

    public State(string Id,bool WinState,BitArray GeneSet,int tok)//for win state
    {
        this.Id = Id;
        this.WinState = WinState;


    }
}
