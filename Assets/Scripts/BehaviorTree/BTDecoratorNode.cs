using UnityEngine;
using System.Collections;

public class BTDecoratorNode : BTNode {
    
    public BTNode Child { get; set; }

    public BTDecoratorNode(BehaviorTree t, BTNode c) : base(t)
    {
        Child = c;
    }
}