using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    #region ���� �ڵ�

    //[SerializeField] GameObject go_dialogueBar;
    //[SerializeField] GameObject go_NameBar;

    //[SerializeField] TMP_Text txt_dialogue;
    //[SerializeField] TMP_Text txt_name;

    //string[] dialogueSentences = 
    //    { "�������\nFlappy Plane�� �����̽� �ٸ� ������\n��ֹ����� ���ϴ� �����̾�" ,
    //      "�����ϰ� ������ [F]�� ������"};

    //public int currentTextIndex = 0;
    //public bool isWaitingInput = false; // ��ȭ ������ Ȯ��
    //public System.Action OnDialogueEnd; // ��ȭ ���� �� ȣ��� �ݹ� �Լ�

    //public void ShowDialogue()
    //{
    //    txt_name.text = "����Ʈ"; // ��Ÿ �ƴ�
    //    currentTextIndex = 0;
    //    ShowCurrentText();
    //    SettingUI(true);
    //}

    //void Update()
    //{
    //    if (isWaitingInput && Input.GetKeyDown(KeyCode.F))
    //    {
    //        //currentTextIndex++; // ���� �ε����� �Ѿ ���� ��ȭ�� ȣ��
    //        if (currentTextIndex < dialogueSentences.Length)
    //        {
    //            ShowCurrentText();
    //            currentTextIndex++; // ���� �ε����� �Ѿ ���� ��ȭ�� ȣ��
    //        }
    //        else
    //        {
    //            SettingUI(false);
    //            isWaitingInput = false;

    //            OnDialogueEnd?.Invoke();// ��ȭ�� ��� �������� �˸�
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

    #region ���� ��� �ڵ�

    [SerializeField] GameObject dialogueBar; // ��ȭâ UI
    [SerializeField] GameObject nameBar; // �̸�â UI

    [SerializeField] private TextMeshProUGUI _dialogueText; // ��ȭ ���� �ؽ�Ʈ
    [SerializeField] string[] _skeletDialogues; // ��ȭ �����

    private int currentDialogueIndex = 0; // ���� ��ȣ
    private Coroutine dialogueCoroutine; // �ߺ� ���� ������ ���� �ڷ�ƾ ����

    public System.Action OnDialogueEnd;       // ��ȭ�� ��� ������ �� ȣ��� �̺�Ʈ (�ݹ�)

    // ��ȭ ���� �Լ�: �ܺ�(��: SkeletController)���� ȣ��
    public void StartDialogue()
    {
        if (_skeletDialogues == null || _skeletDialogues.Length == 0)
        {
            Debug.LogWarning("��ȭ ������ �����ϴ�.");
            SettingUI(false);
            return;
        }

        currentDialogueIndex = 0; // ��ȭ ���� �� �ε��� �ʱ�ȭ
        SettingUI(true); // ��ȭâ UI Ȱ��ȭ


        if (dialogueCoroutine != null) // ���� �ڷ�ƾ�� ���� ���̶�� ����
        {
            StopCoroutine(dialogueCoroutine);
        }

        // ���ο� ��ȭ �ڷ�ƾ ����
        dialogueCoroutine = StartCoroutine(DisplayDialogue());
    }

    // ���� ��ȭ �ٷ� �Ѿ�� �Լ� (Ű �Է�)
    public void Nextdialogue()
    {
        // �ٷ� ���� ���� �������� �ʰ�, �÷��̾ �ٽ� �Է��� ������ ��ٸ��ϴ�.
        if (dialogueCoroutine != null) // ���� ��ȭ ��� ���̸� ���� ����
        {
            StopCoroutine(dialogueCoroutine); // ���� Ÿ���� �ߴ�
            dialogueCoroutine = null; // �ڷ�ƾ ���� ����
            // ���������� ��µǴ� ��ȭ �ϼ�
            _dialogueText.text = _skeletDialogues[currentDialogueIndex]; 
            return;
        }

        currentDialogueIndex++;

        // ��� ��ȭ�� �� ������ٸ�
        if (currentDialogueIndex >= _skeletDialogues.Length) 
        {
            SettingUI(false); // ��ȭâ UI ��Ȱ��ȭ
            _dialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
            Debug.Log("DialogueManager: ��ȭ ����.");
            OnDialogueEnd?.Invoke(); // ��ȭ ���� �̺�Ʈ ȣ��
            return;
        }
        else
        {
            // ���� ��ȭ �� �ڷ�ƾ ����
            dialogueCoroutine = StartCoroutine(DisplayDialogue());
        }
    }

    private IEnumerator DisplayDialogue() // ��ȭ �� ���� Ÿ���� ȿ���� �����ִ� �ڷ�ƾ
    {
        _dialogueText.gameObject.SetActive(true);

        string targetDialogue = _skeletDialogues[currentDialogueIndex];
        _dialogueText.text = ""; // �ؽ�Ʈ�� ����� Ÿ���� ȿ�� ����

        float typeSpeed = 0.05f; // �� ���� Ÿ���ο� �ɸ��� �ð�

        foreach (char letter in targetDialogue.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        dialogueCoroutine = null; // �ڷ�ƾ ���� �� ���� ����
    }

    public void SettingUI(bool OnOff) // ��ȭâ UI Ȱ��ȭ/��Ȱ��ȭ
    {
        dialogueBar.SetActive(OnOff);
        nameBar.SetActive(OnOff);
    }

    #endregion
}
