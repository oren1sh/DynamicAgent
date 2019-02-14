using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// all edge func and data manipulation are here
/// </summary>
public class EdgeController
{
    public List<Edge> AllEdges;
    public static List<Edge> CheckEdges;
    public List<Edge> NewEdges;
    public Dictionary<int, List<Edge>> EdgeByLayer;

    public Edge ETemp;
    public string STemp;

    private enum list{
        AllEdges,
        CheckEdges,
        NewEdges
    }

    private List<bool> BoolListX;
    private List<bool> BoolListO;
    private List<bool> BoolListA;
    private List<bool> BoolListAND;



    public EdgeController()
    {
        AllEdges = new List<Edge>();
        CheckEdges = new List<Edge>();
        NewEdges = new List<Edge>();
        EdgeByLayer = new Dictionary<int, List<Edge>>();


    }

    private void AddEdgeToList(Edge edge , list chose)
    {
        if(chose == list.CheckEdges)//if check so just point at it
        {
            ETemp = edge;
        }
        else
        {
            ETemp = new Edge(edge);
        }
        

        switch (chose)
        {
            case list.AllEdges:
                AllEdges.Add(ETemp);
                break;
            case list.CheckEdges:
                CheckEdges.Add(ETemp);
                break;
            case list.NewEdges:
                NewEdges.Add(ETemp);
                break;
            default:
                Debug.Log("AddEdgeToList faild");
                break;

        }


    }

    FireSupport fireSupport = new FireSupport();

    public void AddNewEdge(string Id, string Sfrom, int fromlayer)
    {
        Debug.Log("AddNewEdge");
        foreach (Edge item in AllEdges)
        {
            if(item.Id == Id)
            {
                Debug.Log("edge with id: " + Id + "already exict");
                return;
            }
        }
        ETemp = new Edge();
        ETemp.Id = Id;
        ETemp.Sfrom = Sfrom;
        ETemp.fromlayer = fromlayer;

        NewEdges.Add(ETemp);

        //bnt-token-fromlayer
        //JsonManager.SerializeEdgeData(NewEdges);
        fireSupport.SaveNewEdge(ETemp);
    }
    public string MakeEdgeId(string ButtonName)
    {
        Debug.Log("MakeEdgeId");
        STemp = "";
        STemp = $"{ButtonName}-{GameHeader.CurrentToken}-{GameHeader.CurrentTurn}";
        return STemp;

    }

    private void AddEdge(string Id, string Sfrom, string Sto, int fromlayer)
    {
        ETemp = new Edge(Id, Sfrom, Sto, fromlayer);

        //bnt-token-fromlayer

    }






}