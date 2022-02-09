using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    Image _healthBarImage;

    public void UpdateHealth(int newHealth, int startingHealth)
    {
        float healthChange = Mathf.Clamp(newHealth/(float)startingHealth, 0f, 1f);
        _healthBarImage.fillAmount = healthChange;
    }
}
