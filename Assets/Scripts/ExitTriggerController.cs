using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTriggerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name != "Player")
        {
            return;
        }
        if(GameData.Quests.Count >= 2)
        {
            SceneManager.LoadScene("Forest");
        }
        else
        {
            GameData.OpenDialog(new string[]
            {
                "You: I should wait and see what happens with Hiro before leaving."
            });
        }
    }

}
