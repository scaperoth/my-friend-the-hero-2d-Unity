using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    CharacterController2D _characterController;
    [SerializeField]
    Transform[] _homeToMayorNavPoionts;

    Transform[] _currentPath;
    int currentPoint = 0;
    Transform _transform;
    string currentPathName = "";

    bool _moving = false;
    Transform _moveTarget;
    private void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    public void MoveFromHomeToMayorsHouse()
    {
        currentPathName = "HomeToMayor";
        _currentPath = _homeToMayorNavPoionts;
    }

    private void Update()
    {
        if(_currentPath == null)
        {
            return;
        }

        if(Vector3.Distance(_transform.position, _currentPath[currentPoint].position) < 0.1)
        {
            currentPoint++;
        }

        if (currentPoint >= _currentPath.Length)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _currentPath = null;

            if (currentPathName == "HomeToMayor")
            {
                currentPathName = "";
                gameObject.SetActive(false);
            }
            return;
        }

        Vector3 input = _characterController.MoveTowards(_currentPath[currentPoint]);

        _animator.SetFloat("SpeedX", input.x);
        _animator.SetFloat("SpeedY", input.y);
    }
}
