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
        txt_dialogue.text = "오우오우\nFlappy Plane은 스페이스바와 마우스 클릭으로\n장애물들을 피하는 게임이야";
        txt_name.text = "ㄱ스트";

        SettingUI(true);
    }

    public void SettingUI(bool p_flag)
    {
        go_dialogueBar.SetActive(p_flag);
        go_NameBar.SetActive(p_flag);
    }
}
