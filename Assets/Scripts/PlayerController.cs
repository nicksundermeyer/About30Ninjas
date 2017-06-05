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
    public GameObject otherPlayer;
    public GameObject player1;
    public GameObject player2;
    public AudioSource grunt;

    private float xMove, yMove, xRot, yRot;
    private float rotAngle;
    private Vector3 vecToOpponent;
    private Rigidbody2D rb;

    // strings to hold input names
    private string horizontal;
    private string vertical;
    private string rHorizontal;
    private string rVertical;

    private GameModel saveManager;
    private PlayerController player1Script;
    private PlayerController player2Script;

	// Use this for initialization
	void Start () 
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<GameModel>();
        player1Script = GameObject.Find("Player 1").GetComponent<PlayerController>();
        player2Script = GameObject.Find("Player 2").GetComponent<PlayerController>();

        horizontal = controlInput + " " + controller + " - LS Horizontal";
        vertical = controlInput + " " + controller + " - LS Vertical";
        rHorizontal = controlInput + " " + controller + " - RS Horizontal";
        rVertical = controlInput + " " + controller + " - RS Vertical";

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (saveManager.twoControllers)
        {
            player1Script.controlInput = "Controller";
            player1Script.controller = 1;

            player2Script.controlInput = "Controller";
            player2Script.controller = 2;
        }
        else
        {
            player1Script.controlInput = "Keyboard";
            player1Script.controller = 1;

            player2Script.controlInput = "Controller";
            player2Script.controller = 1;
        }

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
        grunt.Play();
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
