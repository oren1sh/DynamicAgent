using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateShower : MonoBehaviour {
    public Material lineMat;
    public GameObject panel;
    public List<Button> Buttons;


    // Use this for initialization
    void Start () {
        //GL.PushMatrix();
        //lineMat.SetPass(0);
        //GL.LoadOrtho();

        //GL.Begin(GL.LINES);
        //GL.Color(Color.red);
        //GL.Vertex3(0, 0, 0);
        //GL.Vertex3(1000, 1000, 1000);
        //GL.End();

        //GL.PopMatrix();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        panel.SetActive(true);
        SetBoard(this.name);
    }

    public void SetBoard(string Target)//set the board by state string
    {
        int index = 0;
        foreach (Button Bnt in Buttons)
        {
            Bnt.GetComponentInChildren<Text>().text = Target[index] + "";
            index++;
            //   Debug.Log("set button num=" + (int.Parse(Bnt.name.Replace("Button-", ""))));
            //   Debug.Log("set button text=" + Bnt.GetComponentInChildren<Text>());
        }
        //Debug.Log(index + " buttons text= " + Target);
    }


    //void OnPostRender()
    //{


    //        RenderLine();


    //    //for (int i = 0; i < (GameHeader.BoradSize * GameHeader.BoradSize); i++)
    //    //{

    //    //}
    //}
    //// To show the lines in the editor
    //void OnDrawGizmos()
    //{
    //    //Debug.Log("OnDrawGizmos");
    //    //RenderLine();

    //        RenderLine();


    //}


    //void RenderLine()
    //{
    //     Debug.Log("RenderLine()");
    //    GL.PushMatrix();
    //    lineMat.SetPass(0);
    //    GL.Begin(GL.LINES);

    //    //GL.LoadIdentity();


    //    GL.Color(Color.black);
    //    GL.Vertex(this.transform.position);
    //    GL.Vertex(new Vector3(0, 10, 0));
    //    GL.End();

    //    GL.PopMatrix();
    //   //GL.MultMatrix();
    //}
}
