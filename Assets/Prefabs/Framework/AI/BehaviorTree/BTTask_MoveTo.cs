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
        
        if(_agent)
        {
            _destination = (GameObject)AIC.GetBlackBoardValue(_keyName);
            if (_destination != null)
            {
                _agent.SetDestination(_destination.transform.position);
                _agent.isStopped = false;
                return EBTTaskResult.Running;
            }
        }
        return EBTTaskResult.Failure;
    }

    public override EBTTaskResult UpdateTask()
    {
        if (Vector3.Distance(AIC.transform.position, _destination.transform.position) <= _aceceptableRadius)
        {
            return EBTTaskResult.Success;
        }
        return EBTTaskResult.Running;
    }
    
    public override void FinishTask()
    {
        _agent.isStopped = true;
    }
}

