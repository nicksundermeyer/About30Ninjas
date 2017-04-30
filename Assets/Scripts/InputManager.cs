//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    public string Horizontal;
    public string Vertical;
    public string RHorizontal;
    public string RVertical;
    public string LT;
    public string RT;
    public string LB;
    public string RB;

	// Use this for initialization
	void Start () {
		
	}
	
    public float GetAxis(string axis)
    {
        switch (axis)
        {
            case "Horizontal":
                return Input.GetAxis(Horizontal);
            case "Vertical":
                return Input.GetAxis(Vertical);
            case "RHorizontal":
                return Input.GetAxis(RHorizontal);
            case "RVertical":
                return Input.GetAxis(RVertical);
            case "LT":
                return Input.GetAxis(LT);
            case "RT":
                return Input.GetAxis(RT);
            default:
                Debug.Log("Incorrect Axis Input");
                return 0;
        }
    }
}
