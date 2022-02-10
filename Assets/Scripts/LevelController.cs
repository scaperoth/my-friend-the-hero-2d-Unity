using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

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
        if (GameData.CurrentQuest != null )
        {
            StartQuest(GameData.CurrentQuest);
        }

        GameData.OnQuestComplete.AddListener(HideQuestBox);
        GameData.OnQuestUpdated.AddListener(UpdateQuest);
        GameData.OnQuestStarted.AddListener(StartQuest);

        if (GameData.Quests.Count == 0 && SceneManager.GetActiveScene().name == "HomeTown")
        {
            GameData.StartQuest1();
        }
    }
    private void OnDisable()
    {
        GameData.OnQuestStarted.RemoveListener(StartQuest);
        GameData.OnQuestComplete.RemoveListener(HideQuestBox);
        GameData.OnQuestUpdated.RemoveListener(UpdateQuest);
    }

    void StartQuest(Quest quest)
    {
        ShowQuestBox(quest);

        if (GameData.Quests[0].name == quest.name)
        {
            OnQuest1Started.Invoke();
        }
    }

    void ShowQuestBox(Quest quest)
    {
        _questTitle.text = $"Quest: {quest.name}";

        if (quest.success > 0)
        {
            _questProgress.text = $"Progress: {quest.progress}/{quest.success}";
        }
        else
        {
            _questProgress.text = "";
        }
        _questBox.SetActive(true);
        StartCoroutine(EaseInQuestBox());
    }

    void HideQuestBox(Quest quest)
    {
        StartCoroutine(EaseOutQuestBox());
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
