using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour 
{
    public float damage;
    public ParticleSystem ps;
    public GameObject otherPlayer;

    private PlayerController parentController;
    private Vector3 playerRot;
    private Animator animator;

    // inputs
    private string fire1;
    private string fire2;
    private string fire3;
    private string fire4;

    // booleans used to check if triggers are being pressed or released for the first time
    private bool trigger1Down; 
    private bool trigger1Up;
    private bool trigger2Down; 
    private bool trigger2Up;

    // floats to track time for animations
    private float trigger1Time;
    private float trigger2Time;
    private float recoilTime;

	// Use this for initialization
	void Start () 
    {
        parentController = transform.parent.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        fire1 = parentController.controlInput + parentController.controller + "_Fire1";
        fire2 = parentController.controlInput + parentController.controller + "_Fire2";
        fire3 = parentController.controlInput + parentController.controller + "_Fire3";
        fire4 = parentController.controlInput + parentController.controller + "_Fire4";

        trigger1Down = false;
        trigger1Up = false;
        trigger2Down = false;
        trigger2Up = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        triggerAnims();
	}

    // collision detection
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        Animator colAnim = obj.GetComponent<Animator>();

        if (obj.tag == "Sword")
        {
            // if left strike collides with right parry, recoil
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Left Strike") && colAnim.GetCurrentAnimatorStateInfo(0).IsName("Right Parry"))
            {
                animator.SetTrigger("LeftRecoilTrigger");
                ps.Play();
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Right Strike") && colAnim.GetCurrentAnimatorStateInfo(0).IsName("Left Parry"))
            {
                animator.SetTrigger("RightRecoilTrigger");
                ps.Play();
            }
        }
        else if (obj == otherPlayer)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Left Strike") || animator.GetCurrentAnimatorStateInfo(0).IsName("Right Strike"))
            {
                otherPlayer.GetComponent<PlayerController>().takeDamage(damage);
            }
        }
    }

    // function to trigger swinging animations
    private void triggerAnims()
    {
        // checking buttons coming
        fire1Up();
        fire2Up();

        // resetting booleans to finish animations
        if (Time.time - trigger1Time > 0.9)
        {
            animator.SetBool("LeftStrike", false);
        }
        if (Time.time - trigger2Time > 0.9)
        {
            animator.SetBool("RightStrike", false);
        }

        // checking for trigger presses
        if (fire1Down() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Left Strike"))
        {
            trigger1Time = Time.time;
            animator.SetBool("LeftStrike", true);
        }
        else if (fire2Down() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Right Strike"))
        {
            trigger2Time = Time.time;
            animator.SetBool("RightStrike", true);
        }

        // checking for button press, setting integers to trigger animations
        if (Input.GetButtonDown(fire3))
        {
            animator.SetBool("LeftParry", true);
        }
        else if (Input.GetButtonDown(fire4))
        {
            animator.SetBool("RightParry", true);
        }

        if (Input.GetButtonUp(fire3))
        {
            animator.SetBool("LeftParry", false);
        }
        else if (Input.GetButtonUp(fire4))
        {
            animator.SetBool("RightParry", false);
        }
    
    }

    // helper function to check if fire1 gets pressed down
    private bool fire1Down()
    {
        if (parentController.controlInput == "Controller")
        {
            if (Input.GetAxis(fire1) > 0 && !trigger1Down)
            {
                trigger1Down = true;
                trigger1Up = false;

                return true;
            }
        }
        else if (parentController.controlInput == "Keyboard")
        {
            return Input.GetButtonDown("Keyboard1_Fire1");
        }

        return false;
    }

    // helper function to check if fire1 gets released
    private bool fire1Up()
    {
        if (parentController.controlInput == "Controller")
        {
            if (Input.GetAxis(fire1) < 0 && !trigger1Up)
            {
                trigger1Down = false;
                trigger1Up = true;

                return true;
            }
        }
        else if (parentController.controlInput == "Keyboard")
        {
            return Input.GetButtonUp("Keyboard1_Fire1");
        }

        return false;
    }

    // helper function to check if fire2 gets pressed down
    private bool fire2Down()
    {
        if (parentController.controlInput == "Controller")
        {
            if (Input.GetAxis(fire2) > 0 && !trigger2Down)
            {
                trigger2Down = true;
                trigger2Up = false;

                return true;
            }
        }
        else if (parentController.controlInput == "Keyboard")
        {
            return Input.GetButtonDown("Keyboard1_Fire2");
        }

        return false;
    }

    // helper function to check if fire2 gets released
    private bool fire2Up()
    {
        if (parentController.controlInput == "Controller")
        {
            if (Input.GetAxis(fire2) < 0 && !trigger2Up)
            {
                trigger2Down = false;
                trigger2Up = true;

                return true;
            }
        }
        else if (parentController.controlInput == "Keyboard")
        {
            return Input.GetButtonUp("Keyboard1_Fire2");
        }

        return false;
    }
}
