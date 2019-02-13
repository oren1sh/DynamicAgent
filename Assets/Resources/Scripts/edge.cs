using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Edge {
    public string Id;
    public State From;
    public State To;
    public string Sfrom;
    public string Sto;
    public int Weight;
    public int TotalPass;
    public int TotalWin;
    public int TotalLost;
    public int TotalEven;
    public int Layer;

    public Edge(string Id, State From, State To, string Sfrom, string Sto, int Weight, int TotalPass, int TotalWin, int TotalLost, int TotalEven)
    {
        this.Weight = Weight;
        this.TotalPass = TotalPass;
        this.TotalWin = TotalWin;
        this.TotalLost = TotalLost;
        this.TotalEven = TotalEven;
        this.Sto = Sto;
        this.Sfrom = Sfrom;
        this.Id = Id;
        this.From = From;
        this.To = To;
    }

    public Edge()
    {

    }






}             
