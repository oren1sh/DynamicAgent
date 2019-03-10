using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CpuPlayer
{

    public string name { get; set; }//Token
    string CurrentState;
    public static string mod { get; set; }
    //public static string mod { get; set; }
    string TargetEdge;
    public List<int> TargetsOfOptinity;

    public Dictionary<string, Button> StrToBnt { get; set; }

    public List<Button> Buttons { get; set; }

    public List<State> states { get; set; }

    public State TheStates { get; set; }

    public GameMaster GameMaster { get; set; }

    private bool FMin = false, FBest = false;

    private static Random random;
    private bool rand;
    public float BestPar = 0;
    int index;

    Button bnt;
    public Button PlayTurn()
    {
        if(GameHeader.BWin)
        {
            return null;
        }

        //Debug.Log("cpuPlayer.PlayTurn()");
        GameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        TargetsOfOptinity = new List<int>();
        TargetsOfOptinity.Clear();
        CurrentState = GameHeader.Borad;//1-/W-____X___
        bool go = false;
        foreach (var B in CurrentState.Select((value, i) => new { i, value }))
        {
            if (B.value == '_')
            {
                TargetsOfOptinity.Add(B.i);
                TargetEdge += $",{B.i}-{GameHeader.CurrentToken}-{GameHeader.CurrentTurn}";//make edge id
            }
        }

        
        foreach (int T in TargetsOfOptinity)//check if I can win next turn 
        {
            StringBuilder sb = new StringBuilder(CurrentState);
            sb[T] = char.Parse(name);
            string tempS = sb.ToString();
            if (CheckWin(tempS))//if the state can win?
            {
                return GameMaster.Buttons[T];
            }
        }
        foreach (int T in TargetsOfOptinity)//check if the next player can win next turn 
        {
            StringBuilder sb = new StringBuilder(CurrentState);
            sb[T] = char.Parse(GameHeader.TokensL[(GameHeader.TokensL.IndexOf(name) + 1) % 2]);
            string tempS = sb.ToString();

            if (CheckWin(tempS))//if the state can win?
            {
                return GameMaster.Buttons[T];
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
                    Edge BestParmin = TheStates.Edges[0];
                    foreach (Edge E in TheStates.Edges)
                    {
                        if (E.Weight < min.Weight)
                        {
                            min = E;
                            FMin = true; 
                        }
                           
                        if(E.TotalPass != 0)
                        {
                            if(BestParmin.TotalPass/BestParmin.TotalWin < E.TotalPass/E.TotalWin)
                            {
                                BestParmin = E;
                                FBest = true;
                            }

                        }
                        TargetsOfOptinity.Remove(int.Parse(E.Id[0].ToString()));//remove discover Edges
                    }
                    if((GameHeader.BoradSize * GameHeader.BoradSize)<= min.Weight && TargetsOfOptinity.Count !=0)
                    {
                        FMin = false;
                    }
                    if (((BestParmin.TotalPass / BestParmin.TotalWin) <= 0.5) && TargetsOfOptinity.Count != 0)
                    {
                        FBest = false;
                    }

                    switch (mod)
                    {
                        case "Weight":
                            if(FMin)
                            return GameMaster.Buttons[int.Parse(min.Id[0].ToString())];
                            break;
                        case "BestPar":
                            if(FBest)
                            return GameMaster.Buttons[int.Parse(BestParmin.Id[0].ToString())];
                            break;
                        default:
                            index = Random.Range(0, TargetsOfOptinity.Count);
                            return GameMaster.Buttons[TargetsOfOptinity[index]];
                            break;


                    }

                    
                        
                }
            }//end if DicByLayer have the layer
        }//end if DicByLayer has something
        Debug.Log("the dic is empty");
        index = Random.Range(0, TargetsOfOptinity.Count);
        //Debug.Log("TargetsOfOptinity[index] == " + TargetsOfOptinity[index]);
        //Debug.Log("[index] == " + index);
        return GameMaster.Buttons[TargetsOfOptinity[index]];


    }//end of playTurn()

    private bool CheckWin(string b)
    {
        string win;
        foreach (string a in GameHeader.WinGeneSet)
        {
            //Debug.Log("a =========>" + a);
            win = a.Replace("\"", "").Remove(0, a.IndexOf("-"));//a=" W-_______"
            if (StrAndSter(win, b))
            {
                return true;
            }

        }
        return false;
    }


    //a = _X___X__
    //b = _X_O_X__
    //STemp = _X___X__
    public bool StrAndSter(string a, string b)//is currentState&WinSet[i]=WinSet[i]
    {
        string STemp = "";

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == b[i] && a[i] != '_')
            {
                STemp += a[i];
            }
            else
            {
                STemp += "_";
            }
        }
        if (a.Equals(STemp))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //public string ReplaceAt(this string input, int index, char newChar)
    //{
    //    if (input == null)
    //    {
    //        Debug.Log("input");
    //    }
    //    char[] chars = input.ToCharArray();
    //    chars[index] = newChar;
    //    return new string(chars);
    //}

}