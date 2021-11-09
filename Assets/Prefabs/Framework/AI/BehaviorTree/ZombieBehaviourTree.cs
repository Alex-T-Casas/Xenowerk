using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviourTree : BehaviourTree
{
    public override void Init(AIControler aIControler)
    {
        base.Init(aIControler);
        BTTask_Wait wait = new BTTask_Wait(aIControler, 4);
        SetRoot(wait);
    }
}
