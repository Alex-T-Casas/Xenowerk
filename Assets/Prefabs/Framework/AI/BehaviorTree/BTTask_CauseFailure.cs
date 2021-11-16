using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_CauseFailure : BTNode
{
    public BTTask_CauseFailure(AIControler aiControler) : base(aiControler)
    {

    }
    public override EBTTaskResult Execute()
    {
        return EBTTaskResult.Failure;
    }

    public override void FinishTask()
    {
        
    }

    public override EBTTaskResult Update()
    {
        return EBTTaskResult.Failure;
    }

    public override EBTTaskResult UpdateTask()
    {
        throw new System.NotImplementedException();
    }
}
