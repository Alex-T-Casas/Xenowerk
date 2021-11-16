using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDamageTaken(int newAmt, int OldValue, GameObject Instigator);
public delegate void OnHitpointDepeleted();
public class HealthComp : MonoBehaviour
{
    [SerializeField] int hitPoint;

    public OnDamageTaken onDamageTaken;
    public OnHitpointDepeleted onHitpointDepeleted;


    private void OnParticleCollision(GameObject other)
    {
        WeaponScript weapon = other.GetComponentInParent<WeaponScript>();

        if(weapon!=null)
        {
            TakeDamage(weapon.GetBulletDamage(), other);
        }
    }

    void TakeDamage(int anmt, GameObject Instigator)
    {
        int OldValue = hitPoint;
        hitPoint -= anmt;
        if(hitPoint <= 0)
        {
            hitPoint = 0;
            if(onHitpointDepeleted!=null)
            {
                onHitpointDepeleted.Invoke();
            } 
        }
        else
        {
            //show damage was taken
            if(OldValue != hitPoint)
            {
                if(onDamageTaken!=null)
                {
                   onDamageTaken.Invoke(hitPoint, OldValue, Instigator);
                }

            }
        }
    }
}
