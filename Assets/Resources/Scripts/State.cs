using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class State{

    public string id { set; get; }
    public Dictionary<string, BitArray> GeneSet { set; get; }
    public edge[] Edges { set; get; }
    public bool WinState{ set; get; }
    private int BoardSize { set; get; }
    private int Layer { set; get; }
}
