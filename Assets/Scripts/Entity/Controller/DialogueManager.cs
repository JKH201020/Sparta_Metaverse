using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_dialogueBar;
    [SerializeField] GameObject go_NameBar;

    [SerializeField] TMP_Text txt_dialogue;
    [SerializeField] TMP_Text txt_name;

    TextMeshProUGUI currentScoreText;
    TextMeshProUGUI bestScoreText;

    string[] dialogueSentences = 
        { "오우오우\nFlappy Plane은 스페이스바와 마우스 클릭으로\n장애물들을 피하는 게임이야" ,
          "시작하고 싶으면 [F]를 눌러줘"}; // 대화 스크립트

    public int currentTextIndex = 0; // 배열 인덱스
    public bool isWaitingInput = false; // 대화 중인지 확인
    public System.Action OnDialogueEnd; // 대화 종료 시 호출될 콜백 함수

    public void ShowDialogue() // 대화창을 보여줌
    {
        txt_name.text = "ㄱ스트"; // 오타 아님
        currentTextIndex = 0;
        ShowCurrentText(); // 대화 출력
        SettingUI(true); // 해당 오브젝트 On
    }

    void Update()
    {
        if (isWaitingInput && Input.GetKeyDown(KeyCode.F))
        {
            //currentTextIndex++; // 다음 인덱스로 넘어가 다음 대화를 호출
            Debug.Log('b');
            Debug.Log(currentTextIndex);
            if (currentTextIndex < dialogueSentences.Length)
            {
                Debug.Log('c');
                Debug.Log(currentTextIndex);
                ShowCurrentText(); // 대화 출력
                currentTextIndex++; // 다음 인덱스로 넘어가 다음 대화를 호출
            }
            else
            {
                SettingUI(false); // 해당 오브젝트 Off
                isWaitingInput = false;

                OnDialogueEnd?.Invoke();// 대화가 모두 끝났음을 알림
            }
        }
    }

    void ShowCurrentText()
    {
        isWaitingInput = true;
        txt_dialogue.text = dialogueSentences[currentTextIndex]; // 해당 인덱스 대화 스크립트
        Debug.Log('a');
        Debug.Log(txt_dialogue.text);
    }

    public void SettingUI(bool OnOff) // 대화창 이미지 OnOff
    {
        go_dialogueBar.SetActive(OnOff);
        go_NameBar.SetActive(OnOff);
    }
}
