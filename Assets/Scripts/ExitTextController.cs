using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTextController : MonoBehaviour
{
    [SerializeField]
    GameObject _text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            return;
        }
        _text.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            return;
        }
        _text.SetActive(false);
    }
}
