using UnityEngine;
using System.Collections;

public class BTRepeaterNode : BTDecoratorNode {

    public BTRepeaterNode(BehaviorTree t, BTNode child) : base(t, child)
    {
    }

    public override Result Execute()
    {
        Child.Execute();
        return Result.Running;
    }
}