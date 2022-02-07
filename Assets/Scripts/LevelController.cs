using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    GameObject _questBox;
    [SerializeField]
    TextMeshProUGUI _questTitle;
    [SerializeField]
    TextMeshProUGUI _questProgress;

    public UnityEvent OnQuest1Started;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Quests.Count == 0)
        {
            GameData.OnQuestComplete.AddListener(HideQuestBox);
            GameData.OnQuestUpdated.AddListener(UpdateQuest);

            GameData.StartQuest1();
            GameData.OnDialogClose.AddListener(StartQuest1);
        }
    }
    private void OnDisable()
    {
        GameData.OnDialogClose.RemoveListener(OnQuest1Started.Invoke);
    }

    void StartQuest1()
    {
        ShowQuestBox(GameData.CurrentQuest);
        OnQuest1Started.Invoke();
    }

    void ShowQuestBox(Quest quest)
    {
        _questTitle.text = $"Quest: {quest.name}";
        _questProgress.text = $"Progress: {quest.progress}/{quest.success}";
        _questBox.SetActive(true);
    }

    void HideQuestBox(Quest quest)
    {
        _questBox.SetActive(false);
        _questTitle.text = $"Quest: None";
        _questProgress.text = $"Progress: 0/0";
        GameData.OnQuestStarted.RemoveListener(ShowQuestBox);
        GameData.OnQuestComplete.RemoveListener(HideQuestBox);
        GameData.OnQuestUpdated.RemoveListener(UpdateQuest);
    }

    void UpdateQuest(Quest quest)
    {
        _questProgress.text = $"Progress: {quest.progress}/{quest.success}";
    }
}
