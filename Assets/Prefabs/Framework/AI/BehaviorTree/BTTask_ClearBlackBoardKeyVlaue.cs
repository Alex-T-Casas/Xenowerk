using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_ClearBlackBoardKeyVlaue : BTNode
{
    private string _key;
    public BTTask_ClearBlackBoardKeyVlaue(AIControler aIControler, string key) : base(aIControler)
    {
        _key = key;
    }

    public override EBTTaskResult Execute()
    {
        AIC.SetBlackboardKey(_key, null);
        return EBTTaskResult.Success;
    }

    public override void FinishTask()
    {
        throw new System.NotImplementedException();
    }

    public override EBTTaskResult UpdateTask()
    {
        return EBTTaskResult.Success;
    }
}
