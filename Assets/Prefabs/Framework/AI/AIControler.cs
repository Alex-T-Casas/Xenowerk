using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControler : MonoBehaviour
{
    [SerializeField] BehaviourTree _BehaviourTree;
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
