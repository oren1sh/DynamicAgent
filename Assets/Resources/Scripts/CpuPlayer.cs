using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CpuPlayer
{

    public string name { get; set; }//Token
    string CurrentState;
    string mod;
    string TargetEdge;
    List<int> TargetsOfOptinity;

    public Dictionary<string, Button> StrToBnt { get; set; }

    public List<Button> Buttons { get; set; }

    public List<State> states { get; set; }

    public State TheStates { get; set; }

    public GameMaster GameMaster { get; set; }

    private static Random random;
    private bool rand;

    int index;

    Button bnt;
    public Button PlayTurn()
    {
        if(GameHeader.BWin)
        {
            return null;
        }

        Debug.Log("cpuPlayer.PlayTurn()");
        GameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        //Debug.Log("PlayTurn()");
        //TargetsOfOptinity.Clear();
        TargetsOfOptinity = new List<int>();
        TargetsOfOptinity.Clear();
        CurrentState = GameHeader.Borad;//1-/W-____X___
        bool go = false;
        foreach (var B in CurrentState.Select((value, i) => new { i, value }))
        {
            //Debug.Log($"B {B}");
            if (B.value == '_')
            {

                //Debug.Log($"B.i {B.i}");
                //Debug.Log($"B.value {B.value}");
                TargetsOfOptinity.Add(B.i);
                TargetEdge += $",{B.i}-{GameHeader.CurrentToken}-{GameHeader.CurrentTurn}";//make edge id
            }
        }

        if (GameHeader.DicByLayer.Count != 0)//states and edges are loaded
        {

            List<Edge> AllEdges = EdgeController.AllEdges;
            List<Edge> GoEdges = new List<Edge>();
            states = new List<State>();
            rand = false;
           
            if (GameHeader.DicByLayer.ContainsKey(GameHeader.CurrentTurn))//load this layer
            {
                //Debug.Log("GameHeader.DicByLayer[GameHeader.CurrentTurn]; == " + GameHeader.DicByLayer[GameHeader.CurrentTurn]);
                states = GameHeader.DicByLayer[GameHeader.CurrentTurn];

                TheStates = states.Find(x => x.Id.Equals(CurrentState));

                if (TheStates!=null && TheStates.Edges.Count>0 &&TheStates.Edges[0] != null)
                {
                    Edge min = TheStates.Edges[0];



                    foreach (Edge E in TheStates.Edges)
                    {
                        if (E.Weight < min.Weight)
                            min = E;
                        TargetsOfOptinity.Remove(int.Parse(E.Id[0].ToString()));
                    }
                    Debug.Log("min.Id[0] " + min.Id[0]);
                    if (TheStates.Edges.Count >= ((GameHeader.BoradSize*GameHeader.BoradSize)-GameHeader.CurrentTurn))
                    {
                        //TargetsOfOptinity.Contains(int.Parse(min.Id[0].ToString()));
                        return GameMaster.Buttons[int.Parse(min.Id[0].ToString())];
                    }
                    index = Random.Range(0, TargetsOfOptinity.Count);
                    Debug.Log("TargetsOfOptinity[index] == " + TargetsOfOptinity[index]);
                    Debug.Log("[index] == " + index);
                    return GameMaster.Buttons[TargetsOfOptinity[index]];
                }
            }//end if DicByLayer have the layer

        }//end if DicByLayer has something

        index = Random.Range(0, TargetsOfOptinity.Count);
        Debug.Log("TargetsOfOptinity[index] == " + TargetsOfOptinity[index]);
        Debug.Log("[index] == " + index);
        return GameMaster.Buttons[TargetsOfOptinity[index]];


    }//end of playTurn()

}