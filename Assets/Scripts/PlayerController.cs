using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    GameObject _interactionIndicator;

    bool _allowInteraction;
    string interactingWith;

    int _attackDamage = 5;
    float _lastAttackTime = 0f;
    float _attackDuration = .8f;
    float _attackDistance = 1f;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "HomeTown" && GameData.PreviousScene == null)
        {
            transform.localPosition = new Vector3(5.29f, -8.87f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.DialogOpen)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            return;
        }

        if (_lastAttackTime + _attackDuration > Time.time)
        {
            return;
        }

        if (SceneManager.GetActiveScene().name != "HomeTown" && _allowInteraction && _lastAttackTime + _attackDuration < Time.time && Input.GetButtonDown("Jump"))
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _animator.SetTrigger("Attack");
            _lastAttackTime = Time.time;
            return;
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        _animator.SetFloat("SpeedX", input.x);
        _animator.SetFloat("SpeedY", input.y);


        if (_allowInteraction && Input.GetButtonDown("Interact"))
        {
            GameData.StartCharacterDialog(interactingWith);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _collisionGO = collision.gameObject;
        if (_collisionGO.CompareTag("Interactable"))
        {
            interactingWith = collision.gameObject.name;
            _allowInteraction = true;
            _interactionIndicator.SetActive(true);
        }
        else if (_collisionGO.CompareTag("Hiro"))
        {
            interactingWith = collision.gameObject.name;
            _allowInteraction = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        GameObject _collisionGO = collision.gameObject;
        if (_collisionGO.CompareTag("Interactable"))
        {
            _allowInteraction = false;
            _interactionIndicator.SetActive(false);
        }
        else if (_collisionGO.CompareTag("Hiro"))
        {
            interactingWith = collision.gameObject.name;
            _allowInteraction = false;
            _interactionIndicator.SetActive(false);
        }
    }

    public void CheckIfAttackHit()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackDistance, LayerMask.GetMask("Enemy"));
        foreach(Collider2D hit in hits) {
            GameObject hitGo = hit.gameObject;
            HealthController healthController = hitGo.GetComponent<HealthController>();
            healthController.TakeDamage(_attackDamage);
        }
    }
}
