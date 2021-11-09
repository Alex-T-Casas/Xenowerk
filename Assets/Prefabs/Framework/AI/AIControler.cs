using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControler : MonoBehaviour
{
    [SerializeField] BehaviourTree _BehaviourTree;

    private Dictionary<string, object> _Blackboard; 
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
    // Start is called before the first frame update
    void Start()
    {
        if (_BehaviourTree != null)
        {
            _BehaviourTree.Init(aIControler: this);
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
