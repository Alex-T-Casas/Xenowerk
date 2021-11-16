using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void OnBlackboardKeyUpdated(string key, object value);
public class AIControler : MonoBehaviour
{
    [SerializeField] BehaviourTree _BehaviourTree;
    [SerializeField] PerceptionComponent _perceptionComp;
    public OnBlackboardKeyUpdated onBlackboardKeyUpdated;
    private Dictionary<string, object> _Blackboard = new Dictionary<string, object>();

    public void AddBackboardkey(string key, object defaultValue = null)
    {
        if(!_Blackboard.ContainsKey(key))
        {
            _Blackboard.Add(key, defaultValue);
        }
    }

    public void SetBlackboardKey(string key, object value)
    {
        if(_Blackboard.ContainsKey(key))
        {
            _Blackboard[key] = value;
            if(onBlackboardKeyUpdated != null)
            {
                onBlackboardKeyUpdated.Invoke(key, value);
            }
        }
    }

    public object GetBlackBoardValue (string key)
    {
        return _Blackboard[key];
    }

    public BehaviourTree GetBehaviourTree()
    {
        return _BehaviourTree;
    }


    void Start()
    {
        if (_BehaviourTree != null)
        {
            _BehaviourTree.Init(this);
        }

        if(_perceptionComp != null)
        {
            _perceptionComp.onPerceptionUpdated += PerceptionUpdated;
        }
        
    }

    private void PerceptionUpdated(bool successfulySensed, PerceptionStimuli stimuli)
    {
        if (successfulySensed)
        {
            SetBlackboardKey("Target", stimuli.gameObject);
        }
        else
        {
            SetBlackboardKey("Target", null);
            SetBlackboardKey("LastSeenLocation", stimuli.transform.position);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (_BehaviourTree)
        {
            _BehaviourTree.Run();
        }

        
    }
}
