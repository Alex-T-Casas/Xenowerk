using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : BTNode
{
    private BTNode _child;

    public BTNode GetChild ()
    {
        return _child;
    }

    public Decorator(AIControler aIControler, BTNode child) : base(aIControler)
    {
        _child = child;
        _child.parent = this;
    }

    public override void Finish()
    {
        _child.Finish();
        base.Finish();
    }
}
