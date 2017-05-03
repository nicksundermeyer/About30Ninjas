using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public string controlInput;
    public int controller, acceleration;
    public Camera camera;
    public float health;
    public Slider healthSlider;
    public Text winText;
    public GameObject otherPlayer;

    private float xMove, yMove, xRot, yRot;
    private float rotAngle;
    private Vector3 vecToOpponent;
    private Rigidbody2D rb;

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

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        vecToOpponent = otherPlayer.transform.position - transform.position;

        // get axis of movement (either keyboard or mouse)
        xMove = Input.GetAxis(horizontal);
        yMove = Input.GetAxis(vertical);

        rotatePlayer();
	}

    void FixedUpdate()
    {
        if (vecToOpponent.magnitude < 5)
        {
            rb.AddForce(new Vector3(xMove * (acceleration-150), 0, 0));
            rb.AddForce(new Vector3(0, yMove * (acceleration-150), 0));

            // keeping below max speed
            if (rb.velocity.magnitude > (speed/2))
            {
                rb.velocity = rb.velocity.normalized * (speed/2);
            }
        }
        else
        {
            rb.AddForce(new Vector3(xMove * acceleration, 0, 0));
            rb.AddForce(new Vector3(0, yMove * acceleration, 0));

            // keeping below max speed
            if (rb.velocity.magnitude > (speed))
            {
                rb.velocity = rb.velocity.normalized * (speed);
            }
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health/100;

        if (health <= 0)
        {
            winText.text = otherPlayer.name + " Wins!";
        }
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
