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
    float _attackDistance = 1.5f;

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f);

        float minDistance = Mathf.Infinity;
        _movingTowardsTransform = null;
        _movingTowardsTag = null;

        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player") || collider.CompareTag("Hiro"))
                {
                    
                    Transform collisionTransform = collider.transform;
                    float distance = Vector3.Distance(collisionTransform.position, transform.position);
                    if (distance < minDistance)
                    {
                        _movingTowardsTransform = collisionTransform;
                        _movingTowardsTag = collider.tag;
                        minDistance = distance;
                    }
                }
            }
        }


        if (_movingTowardsTransform == null && Vector3.Distance(_startPosition, transform.position) > .1f)
        {
            Vector3 input = _characterController.MoveTowards(_startPosition);
            _animator.SetFloat("SpeedX", input.x);
            _animator.SetFloat("SpeedY", input.y);
            _animator.SetBool("Attack", false);
        }
        else
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _animator.SetBool("Attack", false);
        }

        if (_movingTowardsTransform == null)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _animator.SetBool("Attack", false);
            return;
        }

        float movingTowardsDistance = Vector3.Distance(_movingTowardsTransform.position, transform.position);
        if (movingTowardsDistance > _attackDistance)
        {
            Vector3 input = _characterController.MoveTowards(_movingTowardsTransform);
            _animator.SetFloat("SpeedX", input.x);
            _animator.SetFloat("SpeedY", input.y);
            _animator.SetBool("Attack", false);
        }
        else if (movingTowardsDistance <= _attackDistance && _movingTowardsTag != null)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _animator.SetBool("Attack", true);
        }
    }


    public void CheckIfAttackhit()
    {
        Debug.Log("CHECKING IF ATTACK HIT");
        float movingTowardsDistance = Vector3.Distance(_movingTowardsTransform.position, transform.position);
        if (movingTowardsDistance <= _attackDistance)
        {
            Debug.Log("ATTACK DID HIT");
            HealthController healthController = _movingTowardsTransform.GetComponent<HealthController>();
            healthController.TakeDamage(10);
        }
    }
}
