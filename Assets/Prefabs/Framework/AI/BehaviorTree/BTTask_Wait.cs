using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BTTask_Wait : BTNode
{
    private float _waitTime;
    private float _counter;

    public BTTask_Wait(AIControler aiController, float waitTime) : base(aiController)
    {

        _waitTime = waitTime;
    }

   
    public override EBTTaskResult Execute()
    {
        Debug.Log($"Wait Started, wait for {_waitTime}");
        _counter = 0;
        return EBTTaskResult.Running;
    }

    public override EBTTaskResult Update()
    {
        _counter += Time.deltaTime;
        if(_counter >= _waitTime)
        {
            return EBTTaskResult.Success;
        }
        return EBTTaskResult.Running;
    }

    public override void FinishTask()
    {
        Debug.Log("Wait finished");
    }
}

