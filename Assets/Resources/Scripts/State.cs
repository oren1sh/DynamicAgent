using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class State{

    public string Id { set; get; }
    public bool WinState{ set; get; }
    private int BoardSize { set; get; }
    private int Layer { set; get; }
    //public BitArray[] GeneSet { set; get; }
    public BitArray GeneSetX { set; get; }
    //public List<Edge> Edges { set; get; }

    public State()
    {
        //this.GeneSet[0] = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
        //this.GeneSet[1] = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
        //this.GeneSet[2] = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
        //this.GeneSet[3] = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);

    }

    public State(string id, List<Edge> Edges, bool WinState, int BoardSize, int Layer, BitArray[] GeneSet)
    {
        this.Id = Id;
        //this.Edges =new List<Edge>(Edges);
        //this.GeneSet[0] = new BitArray(GeneSet[0]);
        //this.GeneSet[1] = new BitArray(GeneSet[1]);
        //this.GeneSet[2] = new BitArray(GeneSet[2]);
        //this.GeneSet[3] = new BitArray(GeneSet[3]);
        this.WinState = WinState;
        this.BoardSize = BoardSize;
        this.Layer = Layer;
    }

    

    public State(State state)
    {
        this.Id = state.Id;
        this.WinState = state.WinState;
        this.GeneSetX = new BitArray(state.GeneSetX);
       // this.GeneSet[0] = new BitArray(GeneSet[0]);
       // this.GeneSet[1] = new BitArray(GeneSet[1]);
       // this.GeneSet[2] = new BitArray(GeneSet[2]);
       // this.GeneSet[3] = new BitArray(GeneSet[3]);


    }

    public State(string Id,bool WinState,BitArray GeneSet,int tok)//for win state
    {
        this.GeneSetX = new BitArray(GeneSet);
       // this.GeneSet[1] = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
       // this.GeneSet[2] = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);
       // this.GeneSet[3] = new BitArray(GameHeader.BoradSize * GameHeader.BoradSize);

        this.Id = Id;
        this.WinState = WinState;
       // GeneSet[tok] = new BitArray(GeneSet2);
       // this.GeneSet[1] = new BitArray(GeneSet[1]);
       // this.GeneSet[2] = new BitArray(GeneSet[2]);
       // this.GeneSet[3] = new BitArray(GeneSet[3]);


    }
}
