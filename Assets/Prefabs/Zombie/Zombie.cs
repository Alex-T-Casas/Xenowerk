using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    HealthComp HealthComponent;
    SightPerceptionComponent SightPerceptionComp;
    Player player;

    private Animator animationControler;
    int UpperBodyLayerIndex;
    [SerializeField] float DmamagePerHit = 10f;
    // Start is called before the first frame update
    void Start()
    {
        SightPerceptionComp = GetComponent<SightPerceptionComponent>();
        HealthComponent = GetComponent<HealthComp>();
        animationControler = GetComponent<Animator>();
        UpperBodyLayerIndex = animationControler.GetLayerIndex("UpperBody");

        if (HealthComponent)
        {
            HealthComponent.onDamageTaken += TookDamage;
            HealthComponent.onHitpointDepeleted += Dead;

            SightPerceptionComp.onPerceptionUpdated += PerceptionUpdated;
        }
    }

    private void PerceptionUpdated(bool successfullySensed, PerceptionStimuli stimuli)
    {
        
    }


    public void Attack ()
    {
        animationControler.SetLayerWeight(UpperBodyLayerIndex, 1);
    }

    public void StopAttack()
    {
        animationControler.SetLayerWeight(UpperBodyLayerIndex, 0);
    }

    private void Dead()
    {
        animationControler.SetTrigger("isDead");
    
    }

    private void TookDamage(int newAmt, int OldValue, GameObject Instigator)
    {
        GetComponent<AIControler>().SetBlackboardKey("Target", Instigator.transform.position);
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

    public void DamagePlayer()
    {
        player.TakeDamage(DmamagePerHit);
    }
}
