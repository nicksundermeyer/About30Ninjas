using System.Collections;

public class BTSequencerNode : BTCompositeNode {

    // variable to keep track of current node
    private int currentNode = 0; 

    public BTSequencerNode(BehaviorTree tree, BTNode [] children) 
        : base(tree, children)
    {
    }

    public override Result Execute ()
    {
        if (currentNode < Children.Count) {
            // store the result for the following tests
            Result result = Children [currentNode].Execute ();
            if (result == Result.Running)
                return Result.Running;
            else if (result == Result.Failure) {
                currentNode = 0;
                return Result.Failure;
            } else {
                // result is Result.Success
                currentNode++;
                if (currentNode < Children.Count)
                    return Result.Running;
                else {
                    currentNode = 0;
                    return Result.Success;
                }
            }
        }
        return Result.Success;
    }
}