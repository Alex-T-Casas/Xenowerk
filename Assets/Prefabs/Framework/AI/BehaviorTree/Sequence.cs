using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Sequence : Composite
{
    public Sequence(AIControler aiController) : base(aiController)
    {

    }
    public override EBTTaskResult DetermineResult(EBTTaskResult result)
    {
        if(result == EBTTaskResult.Failure)
        {
            return EBTTaskResult.Failure;
        }

        if(result == EBTTaskResult.Success)
        {
            if (RunNext())
            {
                return EBTTaskResult.Running;
            }
            else
            {
                return EBTTaskResult.Success;
            }

        }
        return EBTTaskResult.Running;
    }
}

