using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviourTree : BehaviourTree
{
    public override void Init(AIControler aiController)
    {
        base.Init(aiController);
        Sequence PatrollingSequence = new Sequence(aiController);
            PatrollingSequence.AddChild(new BTTask_Wait(aiController, 4));
            PatrollingSequence.AddChild(new BTTask_Wait(aiController, 3));


        SetRoot(PatrollingSequence);
    }
}
