//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class DialogueManager : MonoBehaviour
//{
//    [SerializeField] GameObject go_dialogueBar;
//    [SerializeField] GameObject go_NameBar;

//    [SerializeField] TMP_Text txt_dialogue;
//    [SerializeField] TMP_Text txt_name;

//    TextMeshProUGUI currentScoreText;
//    TextMeshProUGUI bestScoreText;

//    //string[] dialogueSentences = { "�������\nFlappy Plane�� �����̽��ٿ� ���콺 Ŭ������\n��ֹ����� ���ϴ� �����̾�" ,
//    //"�����ϰ� ������ [F]�� ������"};
//    //int currentTextIndex = 0;
//    //bool isWaitingInput = false;

//    public void ShowDialogue()
//    {
//        ////txt_dialogue.text = "�������\nFlappy Plane�� �����̽��ٿ� ���콺 Ŭ������\n��ֹ����� ���ϴ� �����̾�";
//        ////txt_dialogue.text = "�����ϰ� ������ [F]�� ������";
//        //txt_name.text = "����Ʈ";
//        //currentTextIndex = 0;
//        //ShowCurrentText();
//        //SettingUI(true);
//    }

//    private void Start()
//    {
//        Transform scoreTextTransform = transform.Find("ScoreBoard");

//        currentScoreText= scoreTextTransform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
//        bestScoreText = scoreTextTransform.Find("BestScoreText").GetComponent<TextMeshProUGUI> ();
//    }

//    void Update()
//    {
//        ScoreBoard();

//        //if (isWaitingInput && Input.GetKeyDown(KeyCode.F))
//        //{
//        //    currentTextIndex++;

//        //    if (currentTextIndex < dialogueSentences.Length)
//        //    {
//        //        ShowCurrentText();
//        //    }
//        //    else
//        //    {
//        //        SettingUI(false);
//        //        isWaitingInput = false;
//        //    }
//        //}
//    }

//    //void ShowCurrentText()
//    //{
//    //    txt_dialogue.text = dialogueSentences[currentTextIndex];
//    //    isWaitingInput = true;
//    //}

//    public void SettingUI(bool p_flag)
//    {
//        go_dialogueBar.SetActive(p_flag);
//        go_NameBar.SetActive(p_flag);
//    }

//    void ScoreBoard()
//    {
//        int currentScore = GameManager.Instance.CurrentScore;
//        int bestScore = GameManager.Instance.BestScore;

//        currentScoreText.text = currentScore.ToString();
//        bestScoreText.text = bestScore.ToString();
//    }
//}
