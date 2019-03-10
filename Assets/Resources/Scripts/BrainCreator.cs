using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BrainCreator : MonoBehaviour {

    public GameObject StateSimple;
    public GameMaster GameMaster;
    public GameHeader GameHeader;

    public GameObject theFater;


    public Ray Ray;
    public Material lineMat;

    public List<GameObject> nodes;

    public LineRenderer line;

    public List<State> states;
    public Edge Edge;

    public Dictionary<string, Vector3> NameToPosDic;

    public bool databaseLoaded;
        // Use this for initialization
    void Start () {
        databaseLoaded = false;
        GameHeader = GameObject.Find("GameHeader").GetComponent<GameHeader>();
    }
	
	// Update is called once per frame
	void Update () {
        if (databaseLoaded)
        {
            databaseLoaded = false;
            CreateBrain();

        }
	}
    public Color GetColorForState(State stateID)
    {
        string win;
        foreach (string a in GameHeader.WinGeneSet)
        {
            //Debug.Log("a =========>" + a);
            win = a.Replace("\"", "").Remove(0, a.IndexOf("-"));//a=" W-_______"
            if (StrAndSter(win, stateID.Id))
                return Color.green;
        }  
       return Color.black;
    }
    void CreateBrain()
    {
        //oren body for the generator
        Debug.Log("PlaceState()");

        NameToPosDic = new Dictionary<string, Vector3>();
        int index = 0;
        int j = 0;
        List<Color> color = new List<Color> { Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.grey, Color.red, Color.white, Color.yellow };
        //GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sp.transform.position = Vector3.zero;
        for (int i = 0; i <= (GameHeader.BoradSize * GameHeader.BoradSize); i++)
        {
            if (dicTarget.ContainsKey(i))
            {
                j = 0;
                foreach (State states in dicTarget[i])
                {

                    radius = (float)dicTarget[i].Count * i;
                    //Debug.Log("radius " + radius);
                    float angle = j * Mathf.PI * 2f / dicTarget[i].Count;
                    //Debug.Log("angle " + angle);
                    Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, i * 10, Mathf.Sin(angle) * radius);
                    //Debug.Log("newPos " + newPos);

                    GameObject go = Instantiate(StateSimple, newPos, Quaternion.identity);
                    go.name = states.Id;
                    go.GetComponent<MeshRenderer>().material.color = GetColorForState(states);
                    go.transform.parent = theFater.transform;
                    nodes.Add(go);
                    if(!NameToPosDic.ContainsKey(states.Id))
                    NameToPosDic.Add(states.Id, new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z));
                    //foreach (KeyValuePair<string, Vector3> kv in NameToPosDic)
                    //    Debug.Log(kv.Key + " * **---*** " + "val = " + kv.Value);


                    //foreach (Edge E in states.Edges)
                    //{
                    //    Color color = Color.blue;
                    //    Debug.DrawRay(GameObject.Find(E.Sto).transform.localPosition, newPos, color);
                    //    Debug.Log("DrawRay " + GameObject.Find(E.Sfrom).transform.localPosition+" to "+ newPos + " color "+ color);

                    //}

                    j++;
                }
            }
        }
        //foreach (KeyValuePair<string, Vector3> kv in NameToPosDic)
        //    Debug.Log(kv.Key + " * **---*** " + "val  after= " + kv.Value);
        Color color1;
        for (int i = 0; i < (GameHeader.BoradSize * GameHeader.BoradSize); i++)
        {
            if (dicTarget.ContainsKey(i))
            {
                j = 0;
                foreach (State states in dicTarget[i])
                {
                    radius = (float)dicTarget[i].Count * i;
                    //Debug.Log("radius " + radius);
                    float angle = j * Mathf.PI * 2f / dicTarget[i].Count;
                    //Debug.Log("angle " + angle);
                    Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, i * 10, Mathf.Sin(angle) * radius);
                    //Debug.Log("newPos " + newPos);

                    foreach (Edge E in states.Edges)
                    {
                        if (E.Sto != null && NameToPosDic.ContainsKey(E.Sto))
                        {

                            Vector3 pos = newPos;
                            Vector3 dir = (NameToPosDic[E.Sto] - newPos).normalized;
                            float dir1 = (NameToPosDic[E.Sto] - newPos).magnitude;

                            if (i % 2 == 0)
                            {
                                //Debug.Log("DrawRay-Color.blue");
                                color1 = Color.blue;
                                lineMat.color = color1;
                            }
                            else
                            {
                                //Debug.Log("DrawRay-Color.red");

                                color1 = Color.red;
                                lineMat.color = color1;
                            }
                            //Debug.Log("start Drawing line");
                            ////GL.PushMatrix();
                            ////lineMat.SetPass(0);
                            ////GL.LoadOrtho();

                            ////GL.Begin(GL.LINES);
                            ////GL.Color(Color.red);
                            ////GL.Vertex(newPos);
                            ////GL.Vertex(NameToPosDic[E.Sto]);
                            ////GL.End();

                            ////GL.PopMatrix();


                            //GL.PushMatrix();
                            //lineMat.SetPass(0);
                            //GL.LoadOrtho();

                            //GL.Begin(GL.LINES);
                            //GL.Color(Color.red);
                            //GL.Vertex3(0, 0, 0);
                            //GL.Vertex3(1000 * i, 1000 * i, 1000 * i);
                            //GL.End();

                            //GL.PopMatrix();
                            //Debug.Log("end Drawing line");
                            Debug.DrawRay(newPos, dir1 * dir, color1, 120, true);
                        }


                    }

                    j++;
                }
            }
        }

    }//end create brain


    float radius = 1f;
    public Dictionary<int, List<State>> dicTarget;

    /*
    void OnDrawGizmosSelected()
    {
        int j = 0;

        for (int i = 0; i < (GameHeader.BoradSize * GameHeader.BoradSize); i++)
        {
            if (GameHeader.DicByLayer.ContainsKey(i))
            {
                j = 0;
                foreach (State states in GameHeader.DicByLayer[i])
                {
                    radius = (float)i * 10;
                    //Debug.Log("radius " + radius);
                    float angle = j * Mathf.PI * 2f / GameHeader.DicByLayer[i].Count;
                    //Debug.Log("angle " + angle);
                    Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
                    //Debug.Log("newPos " + newPos);

                    foreach (Edge E in states.Edges)
                    {
                        if (E.Sto != null && NameToPosDic.ContainsKey(E.Sto))
                        {

                            //line.positionCount++;
                            //line.SetPosition(index++, newPos);
                            //line.SetPosition(index++, NameToPosDic[E.Sto]);
                            Color color = Color.blue;
                            //Gizmos.DrawLine(newPos, transform.TransformDirection(NameToPosDic[E.Sto]));
                            Debug.DrawRay(Vector3.zero, (newPos - Vector3.zero), Color.yellow);
                            // Draws a 5 unit long red line in front of the object
                            Gizmos.color = Color.red;
                            Vector3 direction = transform.TransformDirection((newPos - Vector3.zero)) * 5;
                            Gizmos.DrawRay(Vector3.zero, direction);
                            Debug.Log("DrawRay " + Vector3.zero + " to " + (newPos - Vector3.zero) + " color " + Color.yellow);

                        }


                    }

                    j++;
                }
            }
        }
        // Draws a 5 unit long red line in front of the object
        //Gizmos.color = Color.red;
        //Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        //Gizmos.DrawRay(transform.position, direction);
    }

    */

    public void PlaceState()
    {
        dicTarget = new Dictionary<int, List<State>>();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://dynamicagent-681fa.firebaseio.com/");

        DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance
     .GetReference("BoardSize").Child("" + GameHeader.BoradSize).Child("Layers").ValueChanged +=(sender,e)=> BrainCreator_ValueChanged(sender,e,dicTarget);
        Debug.Log("things before loading db");
        databaseLoaded = false;
          
        
      }

    private void BrainCreator_ValueChanged(object sender, ValueChangedEventArgs e, Dictionary<int, List<State>> dicTarget)
    {
        List<object> valueMadaFucka;

        DataSnapshot snapshot = e.Snapshot;
        Dictionary<string, object> dic = new Dictionary<string, object>();

        //Debug.Log("so far so good");
        //Debug.Log("snapshot va.lue === " + snapshot.Value);

        try
        {
            valueMadaFucka = (List<object>)snapshot.Value;
            for (int v = 0; v < valueMadaFucka.Count; v++)
            {
                object obj = valueMadaFucka[v];
                dic = (Dictionary<string, System.Object>)obj;


                dic = (Dictionary<string, System.Object>)((Dictionary<string, System.Object>)obj)["States"];


                //    Debug.Log("start load       v=" + v);
                //foreach (KeyValuePair<string, object> p in dic)
                //    Debug.Log("key in dic now is " + p.Key);

                GameHeader.LoadSnapshotToDictionary(dic, dicTarget, v);
                //Debug.Log("task complete       v=" + v);

            }//end for v

            Debug.Log("SnapShot Changed === " + valueMadaFucka.Count +" layers loaded");

            Debug.Log("all database loaded, change flag here");
            databaseLoaded = true;
        }//end try
        catch (Exception ex) { Debug.Log(ex.Message); }
    }//end value changed


    //a = _X___X__
    //b = _X_O_X__
    //STemp = _X___X__
    public bool StrAndSter(string a, string b)//is a&b=B
    {
        string STemp = "";

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == b[i] && a[i]!='_')
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

}//end class

