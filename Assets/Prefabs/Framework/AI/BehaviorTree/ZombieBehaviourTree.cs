using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviourTree : BehaviourTree
{
    public override void Init(AIControler aiController)
    {
        base.Init(aiController);
        //aiController.AddBackboardkey("patrolPoint");
        Sequence WaitTest = new Sequence(aiController);
        WaitTest.AddChild(new BTTask_CauseFailure(aiController));
        WaitTest.AddChild(new BTTask_Wait(aiController, 3));
        WaitTest.AddChild(new BTTask_Wait(aiController, 4));


        SetRoot(WaitTest);






        /*Sequence PatrollingSequence = new Sequence(aiController);
            PatrollingSequence.AddChild(new BTTask_GetNextPatrolPoint(aiController));
            PatrollingSequence.AddChild(new BTTask_MoveTo(aiController, "patrolPoint", 1f));
            PatrollingSequence.AddChild(new BTTask_Wait(aiController, 3));


        SetRoot(PatrollingSequence);*/
    }
}
