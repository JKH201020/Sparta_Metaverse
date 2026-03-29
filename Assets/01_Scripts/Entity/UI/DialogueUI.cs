using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueBarText;
    [SerializeField] private TextMeshProUGUI _nameBarText;
    [SerializeField] private InputActionReference _interactAction;

    private List<string> _currentSentences; // 현재 진행 중인 대사 리스트

    private int _currentIndex = 0; // 현재 대사 몇 번째 줄인지
    private bool _isDialogueActive = false; // 현재 대화 중인지

    private Action _onDialogueEnded; // 대화 종료 시 실행할 콜백 함수

    private const string DialogueTextPathString = "DialogueBar/DialogueText";
    private const string NameTextPathString = "NameBar/NameText";

    private void Reset()
    {
        _dialogueBarText = transform.Find(DialogueTextPathString).GetComponent<TextMeshProUGUI>();
        _nameBarText = transform.Find(NameTextPathString).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (_interactAction != null) _interactAction.action.performed += OnInteractPressed;
    }

    private void OnDisable()
    {
        if (_interactAction != null) _interactAction.action.performed -= OnInteractPressed;
    }

    private void OnInteractPressed(InputAction.CallbackContext context) // 상호작용 키(F) 눌렀을 때
    {
        if (_isDialogueActive) ShowNextSentence();
    }

    /// <summary>
    /// 해당 Zombie 대화 시작
    /// </summary>
    /// <param name="npcID">Zombie 오브젝트 이름</param>
    public void StartDialogue(string npcID, Action onEnd = null)
    {
        _currentSentences = DialogueManager.Instance.GetDialogueList(npcID, out string kName);

        if (_currentSentences != null && _currentSentences.Count > 0)
        {
            _onDialogueEnded = onEnd;
            _isDialogueActive = true;
            _currentIndex = 0;
            _nameBarText.text = kName;

            ShowNextSentence();
        }
    }

    private void ShowNextSentence() // 다음 대사
    {
        if (_currentIndex >= _currentSentences.Count)
        {
            EndDialogue();
            return;
        }

        _dialogueBarText.text = _currentSentences[_currentIndex];
        _currentIndex++;
    }

    private void EndDialogue() // 대화 종료
    {
        _isDialogueActive = false;

        if (UIManager.Instance != null) UIManager.Instance.CloseDialogueUI();

        if (_onDialogueEnded != null)
        {
            _onDialogueEnded.Invoke();
            _onDialogueEnded = null;
        }
    }
}
