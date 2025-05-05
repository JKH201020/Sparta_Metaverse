using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_dialogueBar;
    [SerializeField] GameObject go_NameBar;

    [SerializeField] TMP_Text txt_dialogue;
    [SerializeField] TMP_Text txt_name;

    public void ShowDialogue()
    {
        txt_dialogue.text = "�������\nFlappy Plane�� �����̽��ٿ� ���콺 Ŭ������\n��ֹ����� ���ϴ� �����̾�";
        txt_name.text = "����Ʈ";

        SettingUI(true);
    }

    public void SettingUI(bool p_flag)
    {
        go_dialogueBar.SetActive(p_flag);
        go_NameBar.SetActive(p_flag);
    }
}
