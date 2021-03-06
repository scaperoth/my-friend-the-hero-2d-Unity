using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    GameObject _dialogPanel;
    [SerializeField]
    TextMeshProUGUI _textMesh;

    string[] _currentDialog;
    int _currentDialogIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        _dialogPanel.SetActive(false);
        GameData.OnDialogOpen.AddListener(OpenDialog);
    }

    private void OnDisable()
    {
        GameData.OnDialogOpen.RemoveListener(OpenDialog);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentDialog == null)
        {
            return;
        }

        if (Input.anyKeyDown)
        {
            _currentDialogIndex++;
            if (_currentDialogIndex > _currentDialog.Length - 1)
            {
                CloseDialog();
                return;
            }

            _textMesh.text = _currentDialog[_currentDialogIndex];
        }
    }

    void OpenDialog(string[] textToRun)
    {
        _currentDialogIndex = 0;
        _textMesh.text = textToRun[_currentDialogIndex];
        _dialogPanel.SetActive(true);
        StartCoroutine(WaitForNextFrameToEnableDialog(textToRun));   
    }

    IEnumerator WaitForNextFrameToEnableDialog(string[] textToRun)
    {
        yield return new WaitForEndOfFrame();
        _currentDialog = textToRun;
    }

    void CloseDialog()
    {
        _dialogPanel.SetActive(false);
        _currentDialogIndex = 0;
        _currentDialog = null;
        GameData.CloseDialog();
    }
}
