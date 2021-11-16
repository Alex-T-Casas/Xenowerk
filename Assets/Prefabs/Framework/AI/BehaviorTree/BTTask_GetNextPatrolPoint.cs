using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BTTask_GetNextPatrolPoint : BTNode
{
    public BTTask_GetNextPatrolPoint(AIControler aIControler) : base(aIControler)
    {
    }

    public override EBTTaskResult Execute()
    {
        GameObject nextPatrolPoint = AIC.GetComponent<PatrollingComp>().GetNextPatrolPoint();
        if(nextPatrolPoint != null)
        {
            AIC.SetBlackboardKey("patrolPoint", nextPatrolPoint);
            return EBTTaskResult.Success;
        }
        return EBTTaskResult.Failure;
    }

    public override EBTTaskResult Update()
    {
        return EBTTaskResult.Failure;
    }

    public override void FinishTask()
    {
        
    }

    public override EBTTaskResult UpdateTask()
    {
        throw new NotImplementedException();
    }
}

