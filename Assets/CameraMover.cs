using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    public VariableJoystick joystickRot;
    public VariableJoystick joystickMov;
    public bool Generate = false;

    public BrainCreator creator;
    public Material lineMat;

    public GameHeader GameHeader;

    private Rigidbody camera;
    Quaternion rot;
    // Use this for initialization
    void Start () {

        camera = GetComponent<Rigidbody>();
        lineMat = creator.lineMat;

    }
	
	// Update is called once per frame
	void Update () {

        camera.AddRelativeForce(new Vector3(camera.velocity.x,
                                       camera.velocity.y,
                                        joystickMov.Vertical * 100f),ForceMode.Force);
        camera.AddRelativeForce(new Vector3(joystickMov.Horizontal * 100f,
                                       camera.velocity.y,
                                        camera.velocity.z), ForceMode.Force);
        //rot.eulerAngles = new Vector3(joystickRot.Vertical * -10f,
        //                               camera.velocity.y,
        //                                joystickRot.Horizontal * 10f);

        camera.AddRelativeTorque(new Vector3(joystickRot.Vertical * -10f,
                                       joystickRot.Horizontal *10,
                                       camera.velocity.z), ForceMode.Force);


    }

    public void OnPress()
    {
        if (Generate)
        {
            Generate = false;
        }
        else
        {
            Generate = true;
        }

    }

    void OnPostRender()
    {

        if (Generate)
        {
            creator.PlaceState();
            //RenderLine();
            Generate = false;
        }

        for (int i = 0; i < (GameHeader.BoradSize * GameHeader.BoradSize); i++)
        {

        }
    }
    //// To show the lines in the editor
    //void OnDrawGizmos()
    //{
    //    Debug.Log("OnDrawGizmos");
    //    //RenderLine();
    //    if(Generate)
    //    {
    //        creator.PlaceState();
    //        RenderLine();
    //        Generate = false;
    //    }

    //}


    //void RenderLine()
    //{
    //    Debug.Log("RenderLine()");
    //    GL.PushMatrix();
    //    lineMat.SetPass(0);
    //    GL.LoadOrtho();

    //    GL.Begin(GL.LINES);
    //    GL.Color(Color.red);
    //    GL.Vertex3(0,0,0);
    //    GL.Vertex3(1000,1000,1000);
    //    GL.End();

    //    GL.PopMatrix();

    //}
}
