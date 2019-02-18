using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonState
{
    public State State;


}

[System.Serializable]
public class State{
    /*
    public string Id { set; get; }//layer/W-123456789....
    public bool WinState{ set; get; }
    public int BoardSize { set; get; }
    public int Layer { set; get; }
    public List<Edge> Edges { set; get; }
    */
    public string Id;
    public bool WinState;
    public int BoardSize;
    public int Layer;
    public List<Edge> Edges;

}
