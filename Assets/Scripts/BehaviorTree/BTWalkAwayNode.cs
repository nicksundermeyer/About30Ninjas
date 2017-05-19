using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWalkAwayNode : BTNode {

    protected Vector3 NextDestination { get; set; }
    float speed = 3.0f;

    private GameObject player1;
    private GameObject player2;

    public BTWalkAwayNode(BehaviorTree tree) : base(tree)
    {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        NextDestination = Vector3.zero;

        if ((player1.transform.position - Tree.gameObject.transform.position).magnitude < 3)
        {
            FindNextDestination (player1);
        }
        if ((player2.transform.position - Tree.gameObject.transform.position).magnitude < 3)
        {
            FindNextDestination (player2);
        }
    }

    public override Result Execute ()
    {
        if (Tree.gameObject.transform.position == NextDestination) 
        {
            return Result.Success;
        } 
        else 
        {
            Tree.gameObject.transform.position = Vector3.MoveTowards (Tree.gameObject.transform.position, NextDestination, Time.deltaTime * speed);
            return Result.Running;
        }

    }

    public bool FindNextDestination(GameObject player)
    {
        NextDestination = -(player.transform.position - Tree.gameObject.transform.position).normalized;

        return true;
    }
}

