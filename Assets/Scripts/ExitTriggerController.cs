using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTriggerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (collision.gameObject.name != "Player")
        {
            return;
        }

        if (sceneName == "HomeTown")
        {

            if (GameData.Quests.Count >= 2)
            {
                LoadScene("Forest");
            }
            else
            {
                GameData.OpenDialog(new string[] {
                    "You: I should wait and see what happens with Hiro before leaving."
                });
            }
        }
        else if (sceneName == "Forest")
        {
            LoadScene("HomeTown");
        }
    }

    void LoadScene(string sceneName)
    {
        GameData.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

}
