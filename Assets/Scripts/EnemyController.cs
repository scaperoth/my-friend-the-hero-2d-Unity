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

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 0f, 0f, .2f);
        Gizmos.DrawSphere(this.transform.position, 5f);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f, 6);
        Debug.Log($"Hitting {colliders.Length} colliders with layer {LayerMask.LayerToName(6)}");
        if (colliders.Length > 0)
        {
            Debug.Log($"COLLIDING WITH {colliders.Length} THINGS");
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player") || collider.CompareTag("Hiro"))
                {
                    Transform collisionTransform = collider.transform;
                    _movingTowardsTransform = collisionTransform;
                    _movingTowardsTag = collider.tag;
                    break;
                }
            }
        }
        else
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _animator.SetBool("Attack", false);
        }

        float movingTowardsDistance;
        if (_movingTowardsTransform == null)
        {
            Debug.Log("TRANFORM IS NULL");
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _animator.SetBool("Attack", false);
            return;
        }

        Debug.Log($"_movingTowardsTransform: {_movingTowardsTransform}");
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


    public void CheckIfAttackhit()
    {
        float movingTowardsDistance = Vector3.Distance(_movingTowardsTransform.position, transform.position);
        if (movingTowardsDistance < .5f)
        {
            HealthController healthController = _movingTowardsTransform.GetComponent<HealthController>();
            healthController.TakeDamage(10);
        }
    }
}
