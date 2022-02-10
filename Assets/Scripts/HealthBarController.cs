using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    Image _healthBarImage;
    [SerializeField]
    SpriteRenderer _healthBarSprite;
    float _originalSize;

    [SerializeField]
    UnityEvent OnDeath;
    bool _dead;

    private void Start()
    {
        if (_healthBarSprite != null)
        {
            _originalSize = _healthBarSprite.size.x;
        }
    }

    public void UpdateHealth(int newHealth, int startingHealth)
    {
        if (_dead)
        {
            return;
        }

        if (_healthBarImage != null)
        {
            float healthChange = Mathf.Clamp(newHealth / (float)startingHealth, 0f, 1f);
            _healthBarImage.fillAmount = healthChange;
        }

        if (_healthBarSprite != null)
        {
            float healthChange = Mathf.Clamp(newHealth / (float)startingHealth, 0f, 1f);

            Vector2 size = _healthBarSprite.size;
            Vector2 newSize = new Vector2(healthChange * _originalSize, size.y);
            if (newSize.x == 0)
            {
                OnDeath.Invoke();
                _dead = true;
                return;
            }
            _healthBarSprite.size = newSize;
        }
    }
}
