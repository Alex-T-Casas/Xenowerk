using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    HealthComp HealthComponent;
    SightPerceptionComponent SightPerceptionComp;

    private Animator animationControler;
    // Start is called before the first frame update
    void Start()
    {
        SightPerceptionComp = GetComponent<SightPerceptionComponent>();
        HealthComponent = GetComponent<HealthComp>();
        animationControler = GetComponent<Animator>();

        if(HealthComponent)
        {
            HealthComponent.onDamageTaken += TookDamage;
            HealthComponent.onHitpointDepeleted += Dead;

            SightPerceptionComp.onPerceptionUpdated += PerceptionUpdated;
        }
    }

    private void PerceptionUpdated(bool successfullySensed, PerceptionStimuli stimuli)
    {
        
    }

    private void Dead()
    {
        animationControler.SetTrigger("isDead");
    
    }

    private void TookDamage(int newAmt, int OldValue)
    {
        Debug.Log($"I took {OldValue - newAmt} of damage!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHasDied()
    {
        Invoke("DestroySelf", 1f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
