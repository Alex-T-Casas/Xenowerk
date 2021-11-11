using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PerceptionComponent : MonoBehaviour
{

    private void Start()
    {
        FindObjectOfType<PerceptionSystem>().AddListener(this);
    }

    private void OnDestroy()
    {
        PerceptionSystem system = FindObjectOfType<PerceptionSystem>();
        if (system != null)
        {
            system.RemoveListener(this);
        }
    }

    public abstract bool EvaluatePerception(PerceptionStimuli stimuli);
}
