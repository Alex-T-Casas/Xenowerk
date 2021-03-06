using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SightPerceptionComponent : PerceptionComponent
{
    private List<PerceptionStimuli> CurrentlySeeingStimulis = new List<PerceptionStimuli>();

    [SerializeField] private float SightRadius = 5f;
    [SerializeField] private float LoseSightRadius = 6f;
    [SerializeField] private float PeripheralAngleDeggres = 80f;
    [SerializeField] public Vector3 temphold;

    [SerializeField] private float eyeHeight = 1.6f;

    [SerializeField] private float AttackRange = 1f;

    Zombie zombie;

    public override bool EvaluatePerception(PerceptionStimuli stimuli)
    {
        bool InSightRange = this.InSightRange(stimuli);
        bool IsNotBlocked = this.IsNotBlocked(stimuli);
        bool IsInPeripheralAngleDeggres = this.IsInPeripheralAngleDeggres(stimuli);
        
        bool IsInAttackRange = this.InAttackRange(stimuli);

        bool Percepted = InSightRange && IsNotBlocked && IsInPeripheralAngleDeggres;

        if (Percepted && !CurrentlySeeingStimulis.Contains(stimuli))
        {
            CurrentlySeeingStimulis.Add(stimuli);
            if (onPerceptionUpdated != null)
            {
                Debug.Log($"I have seen: {stimuli.gameObject}");
                

                if(IsInAttackRange)
                {
                    zombie.Attack();
                }
                else
                {
                    zombie.StopAttack();
                }
                
                onPerceptionUpdated.Invoke(true, stimuli);
            }
        }

        if (!Percepted && CurrentlySeeingStimulis.Contains(stimuli))
        {
            CurrentlySeeingStimulis.Remove(stimuli);
            if (onPerceptionUpdated != null)
            {
                Debug.Log($"I lost sight of: {stimuli.gameObject}");
                temphold = this.GetComponent<NavMeshAgent>().destination;
                onPerceptionUpdated.Invoke(false, stimuli);
            }
        }
        return Percepted;
    }

    bool InSightRange(PerceptionStimuli stimuli)
    {
        Vector3 ownerPos = transform.position;
        Vector3 stimuliPos = stimuli.transform.position;
        float checkRadius = SightRadius;
        if (CurrentlySeeingStimulis.Contains(stimuli))
        {
            checkRadius = LoseSightRadius;
        }
        return Vector3.Distance(ownerPos, stimuliPos) < checkRadius;
    }

    bool IsNotBlocked(PerceptionStimuli stimuli)
    {
        Vector3 StimuliCheckPos = stimuli.GetComponent<Collider>().bounds.center;
        Vector3 EyePos = transform.position + Vector3.up * eyeHeight;
        Ray ray = new Ray(EyePos, (StimuliCheckPos - EyePos).normalized);
        if (Physics.Raycast(ray, out RaycastHit HitResult, LoseSightRadius))
        {
            if (HitResult.collider.gameObject == stimuli.gameObject)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    bool IsInPeripheralAngleDeggres(PerceptionStimuli Stimuli)
    {
        float AngleToStimuli = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(transform.forward, (Stimuli.transform.position - transform.position).normalized));
        return AngleToStimuli < PeripheralAngleDeggres / 2;
    }

    bool InAttackRange(PerceptionStimuli stimuli)
    {
        Vector3 ownerPos = transform.position;
        Vector3 stimuliPos = stimuli.transform.position;
        float checkRadius = AttackRange;
        return Vector3.Distance(ownerPos, stimuliPos) <= checkRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SightRadius);
        Gizmos.DrawWireSphere(transform.position, LoseSightRadius);
        Vector3 forward = transform.forward;
        Quaternion RotateLeft = Quaternion.AngleAxis(PeripheralAngleDeggres / 2, Vector3.up);
        Quaternion RotateRight = Quaternion.AngleAxis(-PeripheralAngleDeggres / 2, Vector3.up);
        Gizmos.DrawLine(transform.position, transform.position + RotateLeft * forward * LoseSightRadius);
        Gizmos.DrawLine(transform.position, transform.position + RotateRight * forward * LoseSightRadius);

        foreach (var Stimuli in CurrentlySeeingStimulis)
        {
            Gizmos.DrawLine(transform.position, Stimuli.transform.position);
        }
    }
}
