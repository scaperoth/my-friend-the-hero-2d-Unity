using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public UnityEvent OnQuest1Started;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Quests.Count == 0)
        {
            GameData.StartQuest1();
            GameData.OnDialogClose.AddListener(OnQuest1Started.Invoke);
        }
    }
    private void OnDisable()
    {
        GameData.OnDialogClose.RemoveListener(OnQuest1Started.Invoke);
    }
}
