using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EKeyQuery
{
    Set,
    NotSet
}

public enum EObserverAborts
{
    None,
    Self,
    LowerPriorty,
    Both
}
public class BlackboardDecorator : Decorator
{
    private string _keyName;
    private EKeyQuery _keyQuery;
    private EObserverAborts _observerAborts;

    public BlackboardDecorator(AIControler aIControler, BTNode child, string keyName, EKeyQuery keyQuery, EObserverAborts observerAborts) : base(aIControler, child)
    {
        _keyName = keyName;
        _keyQuery = keyQuery;
        _observerAborts = observerAborts;
        aIControler.onBlackboardKeyUpdated += KeyUpdated;
    }

    public override EBTTaskResult Execute()
    {
        object value = AIC.GetBlackBoardValue(_keyName);
        if(ShouldDoTask(value))
        {
            return EBTTaskResult.Running;
        }
        return EBTTaskResult.Failure;
    }

    public override void Finish()
    {
        base.Finish();
    }

    private void KeyUpdated(string key, object value)
    {
        Debug.Log($"{key} has changed to: {value}");
    }

    public override EBTTaskResult UpdateTask()
    {
        if (!GetChild().HasStarted())
        {
            return GetChild().Start();
        }
        return GetChild().UpdateTask();
    }

    public override void FinishTask()
    {
        throw new System.NotImplementedException();
    }

    bool ShouldDoTask(object value)
    {
        switch(_keyQuery)
        {
            case EKeyQuery.Set:
                return value != null;
            case EKeyQuery.NotSet:
                return value == null;
            default:
                return false;
        }
    }
}
