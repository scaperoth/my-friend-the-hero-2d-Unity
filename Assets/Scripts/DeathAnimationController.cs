using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathAnimationController : MonoBehaviour
{
    public UnityEvent OnDeathEvent;

    public void FireDeathEvent()
    {
        OnDeathEvent.Invoke();
    }
}
