using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    CharacterController2D _characterController;
    Transform _startPosition;

    string _movingTowardsTag;
    Transform _movingTowards;

    private void Start()
    {
        _startPosition = transform;
    }

    private void Update()
    {
       if(_movingTowards == null || _movingTowardsTag == null)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            return;
        }
        else
        {
            Vector3 input = _characterController.MoveTowards(_movingTowards);
            _animator.SetFloat("SpeedX", input.x);
            _animator.SetFloat("SpeedY", input.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Hiro"))
        {
            Debug.Log("COLLISION TRIGGER");
            Transform collisionTransform = collision.transform;
            if (Vector3.Distance(collisionTransform.position, transform.position) > 1f)
            {
                Debug.Log("COLLISION TRIGGER2");
                _movingTowards = collisionTransform;
                _movingTowardsTag = collision.tag;
                Vector3 input = _characterController.MoveTowards(collisionTransform);
                _animator.SetFloat("SpeedX", input.x);
                _animator.SetFloat("SpeedY", input.y);
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Hiro"))
        {
            if(_movingTowardsTag == null)
            {
                return;
            }

            if(collision.CompareTag(_movingTowardsTag))
            {
                _movingTowards = null;
                _movingTowardsTag = null;
            }
        }
    }

    void Attack(Transform attackTransform, string tag)
    {
        if (Vector3.Distance(attackTransform.position, transform.position) > 1f)
        {
            _movingTowardsTag = tag;
            Vector3 input = _characterController.MoveTowards(attackTransform);
            _animator.SetFloat("SpeedX", input.x);
            _animator.SetFloat("SpeedY", input.y);
            return;
        }
    }
}
