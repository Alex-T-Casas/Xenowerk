using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveTo : BTNode
{
    private string _keyName;
    private NavMeshAgent _agent;
    private GameObject _destination;
    private float _aceceptableRadius;


    public BTTask_MoveTo(AIControler aIControler, string keyName, float aceceptableRadius) : base(aIControler)
    {
        _keyName = keyName;
        _agent = aIControler.GetComponent<NavMeshAgent>();
        _aceceptableRadius = aceceptableRadius;
    }

    public override EBTTaskResult Execute()
    {
        throw new NotImplementedException();
    }

    public override void FinishTask()
    {
        throw new NotImplementedException();
    }

    public override EBTTaskResult UpdateTask()
    {
        throw new NotImplementedException();
    }
}

