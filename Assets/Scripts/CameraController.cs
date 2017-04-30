using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player1, player2;
    public float minWidth;

    private Vector3 middle;
    private float width, height;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        setCameraPos();
        setCameraSize();
	}

    void setCameraSize()
    {
        // find horizontal and vertical distances between the players
        width = Mathf.Abs(player1.transform.position.x - player2.transform.position.x)/2;
        height = Mathf.Abs(player1.transform.position.y - player2.transform.position.y)/2 + 2;

        Camera.main.orthographicSize = Mathf.Max(width, height, minWidth);
    }

    void setCameraPos()
    {
        middle = (player1.transform.position + player2.transform.position)/2;
        transform.position = new Vector3(middle.x, middle.y, transform.position.z);
    }
}
