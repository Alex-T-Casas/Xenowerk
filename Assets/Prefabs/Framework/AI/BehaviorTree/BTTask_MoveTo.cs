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
            
            if (GetDestination(out Vector3 destination))
            {
                _agent.SetDestination(destination);
                _agent.isStopped = false;
                return EBTTaskResult.Running;
            }
        }
        return EBTTaskResult.Failure;
    }

    public override EBTTaskResult Update()
    {
        if (GetDestination(out Vector3 destination))
        {
            _agent.SetDestination(destination);

            if (Vector3.Distance(AIC.transform.position, destination) <= _aceceptableRadius)
            {
                return EBTTaskResult.Success;
            }
        }
        else
        {
            return EBTTaskResult.Failure;
        }
        return EBTTaskResult.Running;
    }
    
    public override void FinishTask()
    {
        _agent.isStopped = true;
    }

    bool GetDestination(out Vector3 Destination)
    {
        Destination = Vector3.negativeInfinity;
        object value = AIC.GetBlackBoardValue(_keyName);

        if(value == null)
        {
            return false;
        }

        if (value.GetType() == typeof(GameObject))
        {
            Destination = ((GameObject)value).transform.position;
            return true;
        }

        if(value.GetType() == typeof(Vector3))
        {
            Destination = (Vector3)value;
            return true;
        }
        return false;
    }

    public override EBTTaskResult UpdateTask()
    {
        throw new NotImplementedException();
    }
}

