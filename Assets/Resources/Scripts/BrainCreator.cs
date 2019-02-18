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
                    line.positionCount++;
                    radius = (float)i*10;
                    Debug.Log("radius " + radius);
                    float angle = j * Mathf.PI * 2f / GameHeader.DicByLayer[i].Count;
                    Debug.Log("angle " + angle);
                    Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, i*10, Mathf.Sin(angle) * radius);
                    Debug.Log("newPos " + newPos);
                    GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), newPos, Quaternion.identity);
                    go.name = states.Id;
                    line.SetPosition(index, go.transform.position);
                    NameToPosDic.Add(states.Id, new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z));
                    index++;


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
                            Color color = Color.blue;
                            //Gizmos.DrawLine(newPos, transform.TransformDirection(NameToPosDic[E.Sto]));
                            Debug.DrawRay(newPos , transform.TransformDirection(NameToPosDic[E.Sto])*100, Color.green);
                            Debug.Log("DrawRay " + transform.TransformDirection(NameToPosDic[E.Sto]) + " to " + newPos + " color " + color);

                        }
                       

                    }

                    j++;
                }
            }
        }
    }
}
