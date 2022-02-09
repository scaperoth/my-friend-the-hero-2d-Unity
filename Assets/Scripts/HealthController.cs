using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    int _startingHealth = 100;
    int _currentHealth;

    [SerializeField]
    UnityEvent<int, int> OnHealthChanged;
    [SerializeField]
    UnityEvent<int> OnDamageTaken;
    [SerializeField]
    UnityEvent OnHealthEmpty;

    private void Start()
    {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _startingHealth);
        OnDamageTaken.Invoke(damage);
        OnHealthChanged.Invoke(_currentHealth, _startingHealth);

        if(_currentHealth <= 0)
        {
            OnHealthEmpty.Invoke();
        }
    }

}
