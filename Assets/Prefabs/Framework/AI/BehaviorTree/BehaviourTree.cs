using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    AIControler _AIControler;
    public BTNode CurrentRunningNode { get; set; }
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
        if(! _Root.HasStarted())
        {
            result = _Root.Start();

            if(result != EBTTaskResult.Running)
            {
                _Root.Finish();
                return;
            }
        }

        result = _Root.Update();
        if(result != EBTTaskResult.Running)
        {
            _Root.Finish();
        }
    }    

    public void ReStart()
    {
        _Root.Finish();
    }

    public bool IsRunning(BTNode Node)
    {
        if(GetHierachy(CurrentRunningNode).Contains(Node))
        {
            return true;
        }
        return false;
    }

    List<BTNode> GetHierachy(BTNode Node)
    {
        List<BTNode> Hierachy = new List<BTNode>();
        BTNode NextInHierarchy = Node;
        Hierachy.Add(NextInHierarchy);
        while(NextInHierarchy.parent != null)
        {
            Hierachy.Add(NextInHierarchy.parent);
            NextInHierarchy = NextInHierarchy.parent;
        }
        Hierachy.Reverse();
        return Hierachy;
    }

    public bool IsCurrentLowerThan(BlackboardDecorator blackboardDecorator)
    {
        List<BTNode> CurrentRunningHierachy = GetHierachy(CurrentRunningNode);
        List<BTNode> NodeHierachy = GetHierachy(Node);
        for (int i = 0; i < CurrentRunningHierachy.Count && i < NodeHierachy.Count; i++)
        {
            BTNode CurrentRunningHierarchy
        }
    }
}
