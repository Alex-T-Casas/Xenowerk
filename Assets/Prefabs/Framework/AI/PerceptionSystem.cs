using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionSystem : MonoBehaviour
{
    private List<PerceptionComponent> Listeners = new List<PerceptionComponent>();
    private List<PerceptionStimuli> Stimulis = new List<PerceptionStimuli>();

    public void AddListener(PerceptionComponent perceptionComponent)
    {
        Listeners.Add(perceptionComponent);
    }

    public void RemoveListener(PerceptionComponent perceptionComponent)
    {
        Listeners.Remove(perceptionComponent);
    }

    public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        Stimulis.Add(stimuli);
    }

    public void UnregisterStimuli(PerceptionStimuli stimuli)
    {
        Stimulis.Remove(stimuli);
    }

    private void Update()
    {
        for(int i = 0; i < Stimulis.Count; i++)
        {
            for(int j = 0; j < Listeners.Count; j++)
            {
                PerceptionComponent listener = Listeners[j];
                if(listener)
                {
                    listener.EvaluatePerception(Stimulis[i]);
                }
            }
        }
    }
}
