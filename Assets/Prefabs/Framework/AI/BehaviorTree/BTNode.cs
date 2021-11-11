using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EBTTaskResult
{
    Success,
    Failure,
    Running
}

public abstract class BTNode
{

    private bool ShouldAbortTask;
    public int GetNodeIndexInParent()
    {
        if(parent.GetType() == typeof(Selector) || parent.GetType() == typeof(Sequence))
        {
           return ((Composite).parent)GetChildIndex(this);
        }
    }

    private void GetChildIndex(BTNode bTNode)
    {

        return _Children.FindIndex(0, target => { return bTNode == target; });
    }

    public BTNode parent { get; set; }
    public  AIControler AIC 
    {
        get { return _AIC; } 
    }
    private AIControler _AIC;
    public BTNode(AIControler aIControler)
    {
        _AIC = aIControler;
    }
    public bool HasStarted()
    {
        return _Started;
    }

    private bool _Started;
    private bool _Finished;

    public EBTTaskResult Update()
    {
        if(!ShouldAbortTask)
        {
          return UpdateTask();
        }

        return EBTTaskResult.Failure;

    }
    public EBTTaskResult Start()
    {
        if(!_Started)
        {
            _Started = true;
            _Finished = false;
            AIC.GetBehaviourTree().CurrentRunningNode = this;
            return Execute();
        }
        return EBTTaskResult.Running;
    }

    public virtual void Finish()
    {
        if (!_Finished && HasStarted())
        {
            _Finished = true;
            _Started = false;
            FinishTask();
        }
    }

    public abstract EBTTaskResult Execute();
    public abstract EBTTaskResult UpdateTask();
    public abstract void FinishTask();

}

