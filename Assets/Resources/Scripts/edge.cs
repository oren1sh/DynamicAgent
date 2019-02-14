using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Edge{
    public string Id { get; set; }//bnt-token-fromlayer
    public string Sfrom { get; set; }
    public string Sto { get; set; }
    public double Weight { get; set; }
    public double TotalPass { get; set; }
    public double TotalWin { get; set; }
    public double TotalLost { get; set; }
    public double TotalEven { get; set; }
    public double fromlayer { get; set; }

    [NonSerialized]
    public State From;
    [NonSerialized]
    public State To;


    public Edge()
    {

    }

    public Edge(Edge edge)
    {
        Id = edge.Id;
        Sfrom = edge.Sfrom;
        Sto = edge.Sto;
        //Weight = edge.Weight;
        //TotalPass = edge.TotalPass;
        //TotalWin = edge.TotalWin;
        //TotalLost = edge.TotalLost;
        //TotalEven = edge.TotalEven;
        fromlayer = edge.fromlayer;
        //From = edge.From;
        //To = edge.To;
    }

    public Edge(string Id, State From, State To, string Sfrom, string Sto, int Weight, int TotalPass, int TotalWin, int TotalLost, int TotalEven)
    {
        this.Id = Id;
        this.Weight = Weight;
        this.TotalPass = TotalPass;
        this.TotalWin = TotalWin;
        this.TotalLost = TotalLost;
        this.TotalEven = TotalEven;
        this.Sto = Sto;
        this.Sfrom = Sfrom;
        
       // this.From = From;
       // this.To = To;
    }

    public Edge(string Id, string Sfrom, string Sto, int fromlayer)
    {
        this.Id = Id;
        this.Sto = Sto;
        this.Sfrom = Sfrom;
        this.fromlayer = fromlayer;
       
    }


  //  public override string ToString()
  //  {
  //      string output = "Id: " + Id + "\n";
  //      output += "Sto: " + Sto + "\n";
  //      output += "Sfrom: " + Sfrom + "\n";
  //      output += "fromlayer: " + fromlayer + "\n";
  //
  //      return output;
  //
  //  }//end to string
  //
  //  public string ToJson()
  //  {
  //      string output = "Id: " + Id + "\n";
  //      output += "Sto: " + Sto + "\n";
  //      output += "Sfrom: " + Sfrom + "\n";
  //      output += "fromlayer: " + fromlayer + "\n";
  //
  //      return output;
  //
  //  }//end to string
  //







}             
