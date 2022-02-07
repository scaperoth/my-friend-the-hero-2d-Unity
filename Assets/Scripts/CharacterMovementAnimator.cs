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

        //TryFlipSprite(input);

        _characterController.Move(input * _speed);
    }

    void TryFlipSprite(Vector3 input)
    {
        Vector3 localScale = _animatorTransform.localScale;
        if (!_facingRight && input.x > 0)
        {
            localScale.x *= -1;
            _facingRight = true;
        }
        else if (_facingRight && input.x < 0)
        {
            localScale.x *= -1;
            _facingRight = false;
        }

        _animatorTransform.localScale = localScale;

    }
}
