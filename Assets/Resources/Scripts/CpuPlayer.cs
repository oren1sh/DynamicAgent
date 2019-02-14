using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CpuPlayer : MonoBehaviour {

    string name;//Token
    string CurrentState;
    string mod;
    string TargetEdge;
    string TargetsOfOptinity;

    public GameMaster GameMaster;

    private static Random random;




    public CpuPlayer(string Token, string currentState, string mod)
    {
        Debug.Log("CpuPlayer(");
        this.name = Token;
        CurrentState = currentState;
        this.mod = mod;
        random = new Random();

    }

    public void PlayTurn()
    {
        Debug.Log("PlayTurn()");
        TargetsOfOptinity = "";
        CurrentState = GameHeader.Borad;
        bool go = false;
        foreach (var B in CurrentState.Select((value, i) => new { i, value }))
        {
            if (B.value == '_')
            {
                TargetsOfOptinity += $",{B.i}";
                TargetEdge += $",{B.i}-{GameHeader.CurrentToken}-{GameHeader.CurrentTurn}";
            }
        }
        Debug.Log("TargetsOfOptinity");
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
        int index = Random.Range(0, TargetsOfOptinity.Length);
        Debug.Log("TargetsOfOptinity.Split(',')[index] " + TargetsOfOptinity.Split(',')[index]);
        string str = TargetsOfOptinity.Split(',')[index];
        Debug.Log("str " + str);
        Debug.Log("GameMaster.StrToBnt[str] " + GameMaster.StrToBnt[str]);
        Button bnt = GameMaster.StrToBnt[str];
        Debug.Log("before OnPress(bnt);");
        GameMaster.OnPress(bnt);





    }

    // Use this for initialization
    void Start () {

        name="";//Token
        CurrentState="";
        mod="";
        TargetEdge = "";
        GameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
