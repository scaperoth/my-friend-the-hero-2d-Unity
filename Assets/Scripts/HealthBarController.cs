using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    RectTransform _healthImageTransform;

    public void UpdateHealth(int newHealth, int startingHealth)
    {
        float healthChange = 1 - (newHealth / startingHealth);
        _healthImageTransform.anchoredPosition = new Vector2(healthChange, _healthImageTransform.anchoredPosition.y);
    }
}
