using System.Collections;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    CharacterController2D _characterController;
    [SerializeField]
    Transform[] _homeToMayorNavPoionts;
    [SerializeField]
    Transform[] _mayorToHomeNavPoints;

    Transform[] _currentPath;
    int _currentPoint = 0;
    Transform _transform;
    string currentPathName = "";

    private void Start()
    {
        _transform = transform;
        GameData.OnQuestComplete.AddListener(HandleQuestComplete);
    }

    private void OnDisable()
    {
        GameData.OnQuestComplete.RemoveListener(HandleQuestComplete);
    }

    // Update is called once per frame
    public void MoveFromHomeToMayorsHouse()
    {
        currentPathName = "HomeToMayor";
        _currentPath = _homeToMayorNavPoionts;
    }

    private void Update()
    {
        if (_currentPath == null)
        {
            return;
        }

        if (GameData.DialogOpen && GameData.CurrentActiveDialogCharacter == gameObject.name)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            return;
        }

        if (_currentPoint >= _currentPath.Length)
        {
            _animator.SetFloat("SpeedX", 0);
            _animator.SetFloat("SpeedY", 0);
            _currentPath = null;
            _currentPoint = 0;

            if (currentPathName == "HomeToMayor")
            {
                currentPathName = "";
                _animator.gameObject.SetActive(false);
            }
            return;
        }

        Vector3 input = _characterController.MoveTowards(_currentPath[_currentPoint]);

        _animator.SetFloat("SpeedX", input.x);
        _animator.SetFloat("SpeedY", input.y);

        if (Vector3.Distance(_transform.position, _currentPath[_currentPoint].position) < 0.1)
        {
            _currentPoint++;
        }
    }

    void HandleQuestComplete(Quest quest)
    {
        if(quest.name == GameData.Quests[0].name)
        {
            StartCoroutine(ShowAfterAFewSeconds());
        }
    }

    IEnumerator ShowAfterAFewSeconds()
    {
        while (GameData.DialogOpen)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        _animator.gameObject.SetActive(true);

        GameData.CurrentActiveDialogCharacter = gameObject.name;
        GameData.OnDialogOpen.Invoke(new string[] {
            "Hiro: Hey, meet me at the house, let's talk."
        });
        GameData.DefaultCharacterDialog[gameObject.name] = new string[] { "Hiro: Meet me at the house so we can talk" };

        currentPathName = "MayorToHome";
        _currentPath = _mayorToHomeNavPoints;

        yield return null;
    }
}
