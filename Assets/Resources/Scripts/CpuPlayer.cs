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

    public GameMaster GameMaster { get; set; }

    private static Random random;




    Button bnt;
    public Button PlayTurn()
    {
        //Debug.Log("PlayTurn()");
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
       // Debug.Log("TargetsOfOptinity");
        if (EdgeController.AllEdges.Count != 0)
        {

            List<Edge> AllEdges = EdgeController.AllEdges;
            List<Edge> GoEdges = new List<Edge>();
            foreach (string item in TargetEdge.Split(','))
            {
                Debug.Log("AllEdges.Find(x => x.Id == item); " + AllEdges.Find(x => x.Id == item));
                if (null != AllEdges.Find(x => x.Id == item))
                {
                    GoEdges.Add(AllEdges.Find(x => x.Id == item));
                    go = true;
                }

            }
            if (go)
            {

            }

            //int index = random //////.Next(TargetsOfOptinity.Length);
            //var name = names[index];

        }

        //random cho
        
        //foreach (string s in str)
        //{
        //    Debug.Log($"s {s}");
        //}
        //Debug.Log($"str {str.Length}");
        int index = Random.Range(0, TargetsOfOptinity.Count);
        //Debug.Log("TargetsOfOptinity[index] " + TargetsOfOptinity[index]); 
        //Debug.Log("Buttons[str] " + Buttons[TargetsOfOptinity[index]]);

        //Debug.Log("before OnPress(bnt);");
        //Debug.Log("list of buttons:");
        //foreach (Button b in Buttons)
        //    Debug.Log(b.gameObject.name);
        //Debug.Log("list of indexes:");
        //foreach (int indx in TargetsOfOptinity)
        //    Debug.Log(indx);
        return Buttons[TargetsOfOptinity[index]];
        //for (int i = 0; i < 9; i++)
        //{
        //    Debug.Log("Buttons[i].name " + Buttons[i].name);
        //    if (Buttons[i].name == Buttons[int.Parse(str[index]) - 1].name)
        //    {
        //        bnt = Buttons[i];
        //    }
        //}







    }

}