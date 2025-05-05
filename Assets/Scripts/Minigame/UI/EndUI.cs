using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public override void Init(UIManager uiManager)
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
        // GameManager�� �̱������� ����� ��𼭵� ���� GameManager �ν��Ͻ��� �����Ͽ�
        // ���� �����͸� ������ �� �ִ�.
        int currentScore = GameManager.Instance.CurrentScore;
        int bestScore = GameManager.Instance.BestScore;

        // �ΰ��ӿ� ���� ���
        currentScoreText.text = currentScore.ToString();
        bestScoreText.text = bestScore.ToString();
    }

    void OnClickRestartButton()
    {
        uiManager.OnClickStart();
    }

    void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}