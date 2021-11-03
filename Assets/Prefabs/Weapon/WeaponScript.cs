using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] ParticleSystem BulletEmitter;
    [SerializeField] int BulletFired = 1;

    public void SetActive (bool active)
    {
        gameObject.SetActive(active);
    }

    public void Fire()
    {
        BulletEmitter.Emit(BulletEmitter.emission.GetBurst(BulletFired).maxCount);
    }
}
