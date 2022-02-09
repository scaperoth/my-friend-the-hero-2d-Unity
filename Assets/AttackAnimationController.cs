using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAnimationController : MonoBehaviour
{
    public UnityEvent OnAttackEvent;

    public void FireAttackEvent()
    {
        OnAttackEvent.Invoke();
    }
}
