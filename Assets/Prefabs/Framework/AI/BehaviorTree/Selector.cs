using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Composite
{
    public Selector(AIControler aIControler) : base(aIControler)
    {

    }

    public override EBTTaskResult DetermineResult(EBTTaskResult result)
    {
        if(result == EBTTaskResult.Success)
        {
            return EBTTaskResult.Success;
        }

        if(result == EBTTaskResult.Failure)
        {
            if(RunNext())
            {
                return EBTTaskResult.Running;
            }
            return EBTTaskResult.Failure;
        }
        return EBTTaskResult.Running;
    }
}
