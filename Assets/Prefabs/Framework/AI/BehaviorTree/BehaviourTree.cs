using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    AIControler _AIControler;

    private BTNode _Root;

    public void SetRoot(BTNode root)
    {
        _Root = root;
    }


    public virtual void Init(AIControler aIControler)
    {
        _AIControler = aIControler;
    }

    public void Run()
    {
        EBTTaskResult result = EBTTaskResult.Failure;
        if(!_Root.HasStarted())
        {
            result = _Root.Start();

            if(result != EBTTaskResult.Running)
            {
                _Root.Finish();
                return;
            }
        }

        result = _Root.UpdateTask();
        if(result != EBTTaskResult.Running)
        {
            _Root.Finish();
        }
    }    

}
