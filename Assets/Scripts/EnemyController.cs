using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    CharacterController2D _characterController;
    Vector3 _startPosition;

    string _movingTowardsTag;
    Transform _movingTowardsTransform;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        float movingTowardsDistance;
        if (_movingTowardsTransform == null)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _animator.SetBool("Attack", false);
        }

        movingTowardsDistance = Vector3.Distance(_movingTowardsTransform.position, transform.position);


        if (movingTowardsDistance > .5f)
        {
            Vector3 input = _characterController.MoveTowards(_movingTowardsTransform);
            _animator.SetFloat("SpeedX", input.x);
            _animator.SetFloat("SpeedY", input.y);
            _animator.SetBool("Attack", false);
        }
        else if (movingTowardsDistance <= .5f && _movingTowardsTag != null)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Hiro"))
        {
            Transform collisionTransform = collision.transform;
            if (Vector3.Distance(collisionTransform.position, transform.position) > 1f)
            {
                _movingTowardsTransform = collisionTransform;
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
            if (_movingTowardsTag == null)
            {
                return;
            }

            if (collision.CompareTag(_movingTowardsTag))
            {
                _movingTowardsTransform = null;
                _movingTowardsTag = null;
            }
        }
    }
}
