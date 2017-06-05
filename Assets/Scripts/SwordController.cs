using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour 
{
    public float damage;
    public ParticleSystem ps;
    public GameObject otherPlayer;
    public AudioSource recoil;
    public AudioSource swing;
    public GameObject SwordTrail;

    private PlayerController parentController;
    private Vector3 playerRot;
    private Animator animator;

    // inputs
    private string leftAttack;
    private string rightAttack;
    private string leftParry;
    private string rightParry;

    // booleans used to check if triggers are being pressed or released for the first time
    private bool trigger1Down; 
    private bool trigger1Up;
    private bool trigger2Down; 
    private bool trigger2Up;

    // double to track animation time
    private double endTime;

	// Use this for initialization
	void Start () 
    {
        parentController = transform.parent.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        leftAttack = parentController.controlInput + " " + parentController.controller + " - Attack Left";
        rightAttack = parentController.controlInput + " " + parentController.controller + " - Attack Right";
        leftParry = parentController.controlInput + " " + parentController.controller + " - Parry Left";
        rightParry = parentController.controlInput + " " + parentController.controller + " - Parry Right";

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
            // if left strike collides with right parry, recoil and play recoil sound
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Left Strike") && colAnim.GetCurrentAnimatorStateInfo(0).IsName("Right Parry"))
            {
                animator.SetTrigger("LeftRecoilTrigger");
                ps.Play();
                recoil.Play();
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Right Strike") && colAnim.GetCurrentAnimatorStateInfo(0).IsName("Left Parry"))
            {
                animator.SetTrigger("RightRecoilTrigger");
                ps.Play();
                recoil.Play();
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

        // checking for trigger presses
        if (fire1Down() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Left Strike"))
        {
            StartCoroutine(delayedSound(swing, 0.05));
            StartCoroutine(setAnimBool("LeftStrike", 0.9));
        }
        else if (fire2Down() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Right Strike"))
        {
            StartCoroutine(delayedSound(swing, 0.05));
            StartCoroutine(setAnimBool("RightStrike", 0.9));
        }

        // checking for button press, setting integers to trigger animations
        if (Input.GetButtonDown(leftParry))
        {
            animator.SetBool("LeftParry", true);
        }
        else if (Input.GetButtonDown(rightParry))
        {
            animator.SetBool("RightParry", true);
        }

        if (Input.GetButtonUp(leftParry))
        {
            animator.SetBool("LeftParry", false);
        }
        else if (Input.GetButtonUp(rightParry))
        {
            animator.SetBool("RightParry", false);
        }
    
    }

    // coroutine to play swinging sword sound after a short delay
    private IEnumerator delayedSound(AudioSource source, double delay)
    {
        endTime = Time.time + delay;

        while (Time.time < endTime)
        {
            yield return null;
        }

        source.Play();
    }

    // coroutine to set animation triggers for proper timing
    private IEnumerator setAnimBool(string name, double delay)
    {
        SwordTrail.SetActive(true);
        endTime = Time.time + delay;

        while (Time.time < endTime)
        {
            animator.SetBool(name, true);
            yield return null;
        }

        animator.SetBool(name, false);
        SwordTrail.SetActive(false);
    }

    // helper function to check if fire1 gets pressed down
    private bool fire1Down()
    {
        if (parentController.controlInput == "Controller")
        {
            if ((Input.GetAxis(leftAttack) > 0 || Input.GetButton(leftAttack)) && !trigger1Down)
            {
                trigger1Down = true;
                trigger1Up = false;

                return true;
            }
        }
        else if (parentController.controlInput == "Keyboard")
        {
            return Input.GetButtonDown(leftAttack);
        }

        return false;
    }

    // helper function to check if fire1 gets released
    private bool fire1Up()
    {
        if (parentController.controlInput == "Controller")
        {
            if ((Input.GetAxis(leftAttack) < 0 || Input.GetButtonUp(leftAttack)) && !trigger1Up)
            {
                trigger1Down = false;
                trigger1Up = true;

                return true;
            }
        }
        else if (parentController.controlInput == "Keyboard")
        {
            return Input.GetButtonUp(leftAttack);
        }

        return false;
    }

    // helper function to check if fire2 gets pressed down
    private bool fire2Down()
    {
        if (parentController.controlInput == "Controller")
        {
            if ((Input.GetAxis(rightAttack) > 0 || Input.GetButton(rightAttack)) && !trigger2Down)
            {
                trigger2Down = true;
                trigger2Up = false;

                return true;
            }
        }
        else if (parentController.controlInput == "Keyboard")
        {
            return Input.GetButtonDown(rightAttack);
        }

        return false;
    }

    // helper function to check if fire2 gets released
    private bool fire2Up()
    {
        if (parentController.controlInput == "Controller")
        {
            if ((Input.GetAxis(rightAttack) < 0 || Input.GetButtonUp(rightAttack)) && !trigger2Up)
            {
                trigger2Down = false;
                trigger2Up = true;

                return true;
            }
        }
        else if (parentController.controlInput == "Keyboard")
        {
            return Input.GetButtonUp(rightAttack);
        }

        return false;
    }
}
