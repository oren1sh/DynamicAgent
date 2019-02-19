using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainCreator : MonoBehaviour {

    public GameObject StateSimple;
    public GameMaster GameMaster;
    public GameHeader GameHeader;
    public Ray Ray;

    public LineRenderer line;

    public List<State> states;
    public Edge Edge;

    public Dictionary<string, Vector3> NameToPosDic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    float radius = 1f;

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
        Debug.Log("PlaceState()");
        NameToPosDic = new Dictionary<string, Vector3>();
        int index = 0;
        int j = 0;
        for (int i = 0; i < (GameHeader.BoradSize* GameHeader.BoradSize); i++)
        {
            if (GameHeader.DicByLayer.ContainsKey(i))
            {
                j = 0;
                foreach (State states in GameHeader.DicByLayer[i])
                {
                    
                    radius = (float)i*10;
                    Debug.Log("radius " + radius);
                    float angle = j * Mathf.PI * 2f / GameHeader.DicByLayer[i].Count;
                    Debug.Log("angle " + angle);
                    Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, i*10, Mathf.Sin(angle) * radius);
                    Debug.Log("newPos " + newPos);
                    GameObject go = Instantiate(StateSimple, newPos, Quaternion.identity);
                    go.name = states.Id;
                    
                    NameToPosDic.Add(states.Id, new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z));
                    foreach (KeyValuePair<string, Vector3> kv in NameToPosDic)
                      Debug.Log(kv.Key + " * **---*** " + "val = " + kv.Value);


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
        foreach (KeyValuePair<string, Vector3> kv in NameToPosDic)
            Debug.Log(kv.Key + " * **---*** " + "val  after= " + kv.Value);
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
                    Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, i * 10, Mathf.Sin(angle) * radius);
                    //Debug.Log("newPos " + newPos);

                    foreach (Edge E in states.Edges)
                    {
                        if (E.Sto != null && NameToPosDic.ContainsKey(E.Sto))
                        {

                            Vector3 pos = newPos;
                            Vector3 dir = (NameToPosDic[E.Sto] - newPos).normalized;
                            float dir1 = (NameToPosDic[E.Sto] - newPos).magnitude;

                            Color color = Color.blue;

                            Debug.DrawRay(newPos, dir1 * dir , Color.yellow , Mathf.Infinity ,true);
                        }
                       

                    }

                    j++;
                }
            }
        }
    }
}
