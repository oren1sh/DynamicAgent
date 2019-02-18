using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonEdge
{
    public string Id;//bnt-token-fromlayer
    public string Sfrom;
    public string Sto;
    public double Weight;
    public double TotalPass;
    public double TotalWin;
    public double TotalLost;
    public double TotalEven;
    public double fromlayer;
    public int BoardSize;
}
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
    public int BoardSize { set; get; }

    

    [NonSerialized]
    public State From;
    [NonSerialized]
    public State To;


    public Edge()
    {

    }

    public Edge(JsonEdge jsonEdge)
    {
        this.Id = jsonEdge.Id;//bnt-token-fromlayer
        this.Sfrom = jsonEdge.Sfrom;
        this.Sto = jsonEdge.Sto;
        this.Weight = jsonEdge.Weight;
        this.TotalPass = jsonEdge.TotalPass;
        this.TotalWin = jsonEdge.TotalWin;
        this.TotalLost = jsonEdge.TotalLost;
        this.TotalEven = jsonEdge.TotalEven;
        this.fromlayer = jsonEdge.fromlayer;
        this.BoardSize = jsonEdge.BoardSize;

}//end copy constructor
    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["Id"] = Id;
        result["Sfrom"] = Sfrom;
        result["Sto"] = Sto;
        result["Weight"] = Weight;
        result["TotalPass"] = TotalPass;
        result["TotalWin"] = TotalWin;
        result["TotalLost"] = TotalLost;
        result["TotalEven"] = TotalEven;
        result["fromlayer"] = fromlayer;
        result["BoardSize"] = BoardSize;



        return result;
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

    public Edge(string id, string sfrom, string sto, double weight, double totalPass, double totalWin, double totalLost, double totalEven, double fromlayer, int boardSize, State from, State to)
    {
        Id = id;
        Sfrom = sfrom;
        Sto = sto;
        Weight = weight;
        TotalPass = totalPass;
        TotalWin = totalWin;
        TotalLost = totalLost;
        TotalEven = totalEven;
        this.fromlayer = fromlayer;
        BoardSize = boardSize;
        From = from;
        To = to;
    }

    public Edge(string Id, string Sfrom, string Sto, int fromlayer)
    {
        this.Id = Id;
        this.Sto = Sto;
        this.Sfrom = Sfrom;
        this.fromlayer = fromlayer;
       
    }

    public override bool Equals(object obj)
    {
        var edge = obj as Edge;
        return edge != null &&
               Id == edge.Id &&
               Sfrom == edge.Sfrom &&
               Sto == edge.Sto &&
               Weight == edge.Weight &&
               TotalPass == edge.TotalPass &&
               TotalWin == edge.TotalWin &&
               TotalLost == edge.TotalLost &&
               TotalEven == edge.TotalEven &&
               fromlayer == edge.fromlayer &&
               BoardSize == edge.BoardSize &&
               EqualityComparer<State>.Default.Equals(From, edge.From) &&
               EqualityComparer<State>.Default.Equals(To, edge.To);
    }

    public override int GetHashCode()
    {
        var hashCode = 1949959348;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Sfrom);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Sto);
        hashCode = hashCode * -1521134295 + Weight.GetHashCode();
        hashCode = hashCode * -1521134295 + TotalPass.GetHashCode();
        hashCode = hashCode * -1521134295 + TotalWin.GetHashCode();
        hashCode = hashCode * -1521134295 + TotalLost.GetHashCode();
        hashCode = hashCode * -1521134295 + TotalEven.GetHashCode();
        hashCode = hashCode * -1521134295 + fromlayer.GetHashCode();
        hashCode = hashCode * -1521134295 + BoardSize.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<State>.Default.GetHashCode(From);
        hashCode = hashCode * -1521134295 + EqualityComparer<State>.Default.GetHashCode(To);
        return hashCode;
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
public class EdgeRoot {
    public Edge Edge { get; set; }
}
