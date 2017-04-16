using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {
    public int offset;

    private PlayerController parentController;
    private Vector3 playerRot;
    private Animator animator;
    private bool striking = 0;

    // inputs
    private string fire1;
    private string fire2;

	// Use this for initialization
	void Start () {
        parentController = transform.parent.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        fire1 = parentController.controlInput + parentController.controller + "_Fire1";
        fire2 = parentController.controlInput + parentController.controller + "_Fire2";
	}
	
	// Update is called once per frame
	void Update () {
//        Debug.Log("Fire1: " + Input.GetAxis(fire1));
        Debug.Log("Fire2: " + Input.GetAxis(fire2));

        // checking for button press, and making sure that 
        if (Input.GetAxis(fire1) == 1)
        {
            if (!striking)
            {
                animator.SetTrigger("Left Strike");
            }
        }
        else if (Input.GetAxis(fire2) == 1)
        {
            if (!striking)
            {
                animator.SetTrigger("Right Strike");
            }
        }
	}
}
