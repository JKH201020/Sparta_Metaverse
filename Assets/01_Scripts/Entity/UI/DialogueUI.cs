using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueBarText;
    [SerializeField] private TextMeshProUGUI _nameBarText;
    [SerializeField] private InputActionReference _interactAction;

    private List<string> _currentSentences; // 현재 진행 중인 대사 리스트

    private int _currentIndex = 0; // 현재 대사 몇 번째 줄인지
    private bool _isDialogueActive = false; // 현재 대화 중인지

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
    /// 해당 Npc 대화 시작
    /// </summary>
    /// <param name="npcName">Npc 이름</param>
    public void StartDialogue(string npcName)
    {
        _currentSentences = DialogueManager.Instance.GetDialogueList(npcName);

        if (_currentSentences != null &&  _currentSentences.Count > 0)
        {
            _isDialogueActive = true;
            _currentIndex = 0;
            _nameBarText.text = npcName;
            UIManager.Instance.ShowDialogueUI(npcName);

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
        UIManager.Instance.CloseDialogueUI();
        Debug.Log("대화 종료");
    }
}
