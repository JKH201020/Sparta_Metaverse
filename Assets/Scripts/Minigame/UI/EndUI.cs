using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : BaseUI
{
    TextMeshProUGUI currentScoreText;
    TextMeshProUGUI bestScoreText;

    Button restartButton;
    Button exitButton;

    protected override UIState GetUIState()
    {
        return UIState.End;
    }

    public override void Init(MiniGameUIManager uiManager)
    {
        base.Init(uiManager);

        // "ScoreImage" ������Ʈ ã��
        Transform scoreImageTransform = transform.Find("ScoreImage");

        // �ڽ� ������Ʈ���� �� ������Ʈ ����
        currentScoreText = scoreImageTransform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = scoreImageTransform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        restartButton = transform.Find("ReStartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        // ��ư Ŭ�� �� �̺�Ʈ ����
        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // UI�� ���� ���� ǥ��
    void Update()
    {
        // �ΰ��ӿ� ���� ���
        if (MiniGameManager.Instance != null)
        {
            currentScoreText.text = MiniGameManager.Instance.CurrentScore.ToString();
            bestScoreText.text = MiniGameManager.Instance.BestScore.ToString();
        }
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("MiniGameScene"); // ������ ������ϴ� �Լ�
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}