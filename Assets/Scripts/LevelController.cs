using System.Collections;
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
        StartCoroutine(EaseInQuestBox());
    }

    void HideQuestBox(Quest quest)
    {
        StartCoroutine(EaseOutQuestBox());
        GameData.OnQuestComplete.RemoveListener(HideQuestBox);
        GameData.OnQuestUpdated.RemoveListener(UpdateQuest);
    }

    void UpdateQuest(Quest quest)
    {
        _questProgress.text = $"Progress: {quest.progress}/{quest.success}";
    }

    IEnumerator EaseInQuestBox()
    {
        RectTransform rect = _questBox.GetComponent<RectTransform>();
        Vector3 start = rect.anchoredPosition;
        Vector3 target = new Vector3(0, start.y, start.z);
        float animationTime = .5f;
        float currentTime = 0;
        float normalizedValue;

        while (currentTime <= animationTime)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / animationTime;
            Vector3 newPosition = Vector3.Lerp(start, target, normalizedValue);
            rect.anchoredPosition = newPosition;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator EaseOutQuestBox()
    {
        RectTransform rect = _questBox.GetComponent<RectTransform>();
        Vector3 start = rect.anchoredPosition;
        Vector3 target = new Vector3(rect.sizeDelta.x, start.y, start.z);
        float animationTime = .5f;
        float currentTime = 0;
        float normalizedValue;

        while (currentTime <= animationTime)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / animationTime;
            Vector3 newPosition = Vector3.Lerp(start, target, normalizedValue);
            rect.anchoredPosition = newPosition;
            yield return new WaitForEndOfFrame();
        }
    }
}
