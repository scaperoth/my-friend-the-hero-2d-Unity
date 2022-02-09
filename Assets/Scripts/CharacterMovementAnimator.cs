using UnityEngine;

public class CharacterMovementAnimator : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    float _speed = 5;
    Transform _animatorTransform;
    CharacterController2D _characterController;

    bool _facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        _animatorTransform = _animator.transform;
        _characterController = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(_animator.GetFloat("SpeedX"), _animator.GetFloat("SpeedY"), 0f);

        _characterController.Move(input * _speed);
    }
}
