using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    GameObject _interactionIndicator;

    bool _showInteraction;
    string interactingWith;

    // Update is called once per frame
    void Update()
    {
        if (GameData.DialogOpen)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            return;
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        _animator.SetFloat("SpeedX", input.x);
        _animator.SetFloat("SpeedY", input.y);

        if(_showInteraction && Input.GetButtonDown("Jump"))
        {
            GameData.StartCharacterDialog(interactingWith);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            interactingWith = collision.gameObject.name;
            _showInteraction = true;
            _interactionIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Interactable"))
        {
            _showInteraction = false;
            _interactionIndicator.SetActive(false);
        }
    }
}
