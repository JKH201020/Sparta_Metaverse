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
        { "�������\nFlappy Plane�� �����̽��ٿ� ���콺 Ŭ������\n��ֹ����� ���ϴ� �����̾�" ,
          "�����ϰ� ������ [F]�� ������"};

    public int currentTextIndex = 0;
    public bool isWaitingInput = false; // ��ȭ ������ Ȯ��
    public System.Action OnDialogueEnd; // ��ȭ ���� �� ȣ��� �ݹ� �Լ�

    public void ShowDialogue()
    {
        txt_name.text = "����Ʈ"; // ��Ÿ �ƴ�
        currentTextIndex = 0;
        ShowCurrentText();
        SettingUI(true);
    }

    void Update()
    {
        if (isWaitingInput && Input.GetKeyDown(KeyCode.F))
        {
            //currentTextIndex++; // ���� �ε����� �Ѿ ���� ��ȭ�� ȣ��
            Debug.Log('b');
            Debug.Log(currentTextIndex);
            if (currentTextIndex < dialogueSentences.Length)
            {
                Debug.Log('c');
                Debug.Log(currentTextIndex);
                ShowCurrentText();
                currentTextIndex++; // ���� �ε����� �Ѿ ���� ��ȭ�� ȣ��
            }
            else
            {
                SettingUI(false);
                isWaitingInput = false;

                OnDialogueEnd?.Invoke();// ��ȭ�� ��� �������� �˸�
            }
        }
    }

    void ShowCurrentText()
    {
        isWaitingInput = true;
        txt_dialogue.text = dialogueSentences[currentTextIndex];
        Debug.Log('a');
        Debug.Log(txt_dialogue.text);
    }

    public void SettingUI(bool OnOff)
    {
        go_dialogueBar.SetActive(OnOff);
        go_NameBar.SetActive(OnOff);
    }
}
