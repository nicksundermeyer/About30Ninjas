using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BTCompositeNode : BTNode
{
    public List<BTNode> Children { get; set; }

    public BTCompositeNode(BehaviorTree t, BTNode [] nodes) : base(t)
    {
        Children = new List<BTNode> (nodes);
    }
}