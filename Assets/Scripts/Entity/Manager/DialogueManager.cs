using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    #region 기존 코드

    //[SerializeField] GameObject go_dialogueBar;
    //[SerializeField] GameObject go_NameBar;

    //[SerializeField] TMP_Text txt_dialogue;
    //[SerializeField] TMP_Text txt_name;

    //string[] dialogueSentences = 
    //    { "오우오우\nFlappy Plane은 스페이스 바를 눌러서\n장애물들을 피하는 게임이야" ,
    //      "시작하고 싶으면 [F]를 눌러줘"};

    //public int currentTextIndex = 0;
    //public bool isWaitingInput = false; // 대화 중인지 확인
    //public System.Action OnDialogueEnd; // 대화 종료 시 호출될 콜백 함수

    //public void ShowDialogue()
    //{
    //    txt_name.text = "ㄱ스트"; // 오타 아님
    //    currentTextIndex = 0;
    //    ShowCurrentText();
    //    SettingUI(true);
    //}

    //void Update()
    //{
    //    if (isWaitingInput && Input.GetKeyDown(KeyCode.F))
    //    {
    //        //currentTextIndex++; // 다음 인덱스로 넘어가 다음 대화를 호출
    //        if (currentTextIndex < dialogueSentences.Length)
    //        {
    //            ShowCurrentText();
    //            currentTextIndex++; // 다음 인덱스로 넘어가 다음 대화를 호출
    //        }
    //        else
    //        {
    //            SettingUI(false);
    //            isWaitingInput = false;

    //            OnDialogueEnd?.Invoke();// 대화가 모두 끝났음을 알림
    //        }
    //    }
    //}

    //void ShowCurrentText()
    //{
    //    isWaitingInput = true;
    //    txt_dialogue.text = dialogueSentences[currentTextIndex];
    //}

    //public void SettingUI(bool OnOff)
    //{
    //    go_dialogueBar.SetActive(OnOff);
    //    go_NameBar.SetActive(OnOff);
    //}

    #endregion

    #region 강의 기반 코드

    [SerializeField] GameObject dialogueBar; // 대화창 UI
    [SerializeField] GameObject nameBar; // 이름창 UI

    [SerializeField] private TextMeshProUGUI _dialogueText; // 대화 내용 텍스트
    [SerializeField] string[] _skeletDialogues; // 대화 문장들

    private int currentDialogueIndex = 0; // 문장 번호
    private Coroutine dialogueCoroutine; // 중복 실행 방지를 위한 코루틴 참조

    public System.Action OnDialogueEnd;       // 대화가 모두 끝났을 때 호출될 이벤트 (콜백)

    // 대화 시작 함수: 외부(예: SkeletController)에서 호출
    public void StartDialogue()
    {
        if (_skeletDialogues == null || _skeletDialogues.Length == 0)
        {
            Debug.LogWarning("대화 내용이 없습니다.");
            SettingUI(false);
            return;
        }

        currentDialogueIndex = 0; // 대화 시작 시 인덱스 초기화
        SettingUI(true); // 대화창 UI 활성화


        if (dialogueCoroutine != null) // 이전 코루틴이 실행 중이라면 중지
        {
            StopCoroutine(dialogueCoroutine);
        }

        // 새로운 대화 코루틴 시작
        dialogueCoroutine = StartCoroutine(DisplayDialogue());
    }

    // 다음 대화 줄로 넘어가는 함수 (키 입력)
    public void Nextdialogue()
    {
        // 바로 다음 줄을 시작하지 않고, 플레이어가 다시 입력할 때까지 기다립니다.
        if (dialogueCoroutine != null) // 현재 대화 출력 중이면 강제 종료
        {
            StopCoroutine(dialogueCoroutine); // 현재 타이핑 중단
            dialogueCoroutine = null; // 코루틴 참조 해제
            // 마지막으로 출력되던 대화 완성
            _dialogueText.text = _skeletDialogues[currentDialogueIndex]; 
            return;
        }

        currentDialogueIndex++;

        // 모든 대화를 다 보여줬다면
        if (currentDialogueIndex >= _skeletDialogues.Length) 
        {
            SettingUI(false); // 대화창 UI 비활성화
            _dialogueText.text = ""; // 텍스트 초기화
            Debug.Log("DialogueManager: 대화 종료.");
            OnDialogueEnd?.Invoke(); // 대화 종료 이벤트 호출
            return;
        }
        else
        {
            // 다음 대화 줄 코루틴 시작
            dialogueCoroutine = StartCoroutine(DisplayDialogue());
        }
    }

    private IEnumerator DisplayDialogue() // 대화 한 줄을 타이핑 효과로 보여주는 코루틴
    {
        _dialogueText.gameObject.SetActive(true);

        string targetDialogue = _skeletDialogues[currentDialogueIndex];
        _dialogueText.text = ""; // 텍스트를 비워서 타이핑 효과 시작

        float typeSpeed = 0.05f; // 한 글자 타이핑에 걸리는 시간

        foreach (char letter in targetDialogue.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        dialogueCoroutine = null; // 코루틴 종료 시 참조 해제
    }

    public void SettingUI(bool OnOff) // 대화창 UI 활성화/비활성화
    {
        dialogueBar.SetActive(OnOff);
        nameBar.SetActive(OnOff);
    }

    #endregion
}
