using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed;
    public string controlInput;
    public int controller;
    public Camera camera;

    private float xMove, yMove, xRot, yRot;
    private float rotAngle;

    // strings to hold input names
    private string horizontal;
    private string vertical;
    private string rHorizontal;
    private string rVertical;

	// Use this for initialization
	void Start () 
    {
        horizontal = controlInput + controller + "_Horizontal";
        vertical = controlInput + controller + "_Vertical";
        rHorizontal = controlInput + controller + "_RHorizontal";
        rVertical = controlInput + controller + "_RVertical";
	}
	
	// Update is called once per frame
	void Update () 
    {
        movePlayer();
        rotatePlayer();
	}

    void movePlayer()
    {
        // get axis of movement (either keyboard or mouse)
        xMove = Input.GetAxis(horizontal);
        yMove = Input.GetAxis(vertical);

        transform.Translate(new Vector3(xMove * Time.deltaTime * speed, 0, 0), Space.World);
        transform.Translate(new Vector3(0, yMove * Time.deltaTime * speed, 0), Space.World);
    }

    void rotatePlayer()
    {
        if (controlInput == "Keyboard")
        {
            // rotate towards mouse
            xRot = (Input.mousePosition.x - camera.WorldToScreenPoint(transform.position).x);
            yRot = (Input.mousePosition.y - camera.WorldToScreenPoint(transform.position).y);

            rotAngle = Mathf.Atan2(xRot, -yRot) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotAngle);
        }
        else
        {
            // rotate in direction of controller right stick

            xRot = Input.GetAxis(rHorizontal);
            yRot = Input.GetAxis(rVertical);

            // if rotation is not in any direction, rotate in direction of movement
            if (xRot == 0 && yRot == 0)
            {
                rotAngle = Mathf.Atan2(xMove, -yMove) * Mathf.Rad2Deg;
            }
            else
            {
                rotAngle = Mathf.Atan2(xRot, yRot) * Mathf.Rad2Deg;
            }

            transform.rotation = Quaternion.Euler(0, 0, rotAngle);
        }
    }
}
