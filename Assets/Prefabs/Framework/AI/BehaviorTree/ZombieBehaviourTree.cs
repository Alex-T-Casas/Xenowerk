using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviourTree : BehaviourTree
{
    public override void Init(AIControler aiController)
    {
        base.Init(aiController);
            aiController.AddBackboardkey("patrolPoint");
            aiController.AddBackboardkey("Target");
            aiController.AddBackboardkey("LastSeenLocation");
        Selector RootSelector = new Selector(aiController);
            BTTask_MoveTo MoveToTarget = new BTTask_MoveTo(aiController, "Target", 1.5f);
            BlackboardDecorator MoveToTargetDeco = new BlackboardDecorator(aiController, MoveToTarget, "Target", EKeyQuery.Set, EObserverAborts.Both);

            BTTask_MoveTo MoveToLastTarget = new BTTask_MoveTo(aiController, "LastSeenLocation", 1.5f);
            BlackboardDecorator MoveToLastTargetDeco = new BlackboardDecorator(aiController, MoveToTarget, "LastSeenLocation", EKeyQuery.Set, EObserverAborts.Both);

        RootSelector.AddChild(MoveToTargetDeco);
        RootSelector.AddChild(MoveToLastTargetDeco);
        Sequence PatrollingSequence = new Sequence(aiController);
           PatrollingSequence.AddChild(new BTTask_Wait(aiController, 3));
            PatrollingSequence.AddChild(new BTTask_GetNextPatrolPoint(aiController));
                PatrollingSequence.AddChild(new BTTask_MoveTo(aiController, "patrolPoint", 1f));

        RootSelector.AddChild(PatrollingSequence);
        SetRoot(RootSelector);
    }
}
