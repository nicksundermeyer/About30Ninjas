using UnityEngine;
using System.Collections;

public class BTRandomWalkNode : BTNode {

    protected Vector3 NextDestination { get; set; }
    float speed = 3.0f;

    public BTRandomWalkNode(BehaviorTree tree) : base(tree)
    {
        NextDestination = Vector3.zero;
        FindNextDestination ();
    }

    public override Result Execute ()
    {
        // if we've made it to the destination
        if (Tree.gameObject.transform.position == NextDestination) {
            if (!FindNextDestination ())
                return Result.Failure;

            return Result.Success;
        } else {
            Tree.gameObject.transform.position = 
                Vector3.MoveTowards (Tree.gameObject.transform.position,
                    NextDestination, Time.deltaTime * speed);
            return Result.Running;
        }

    }

    public bool FindNextDestination()
    {
        object o;
        bool found = false;
        found = Tree.Blackboard.TryGetValue ("WorldBounds", out o);
        if (found) {
            Rect bounds = (Rect)o;
            float x = Random.value * bounds.width;
            float y = Random.value * bounds.height;
            NextDestination = new Vector3 (x, y, NextDestination.z);
        }

        return found;
    }
}