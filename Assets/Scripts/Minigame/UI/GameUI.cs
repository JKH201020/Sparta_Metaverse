using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : BaseUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestscoreText;

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // �ڽ� ������Ʈ���� �� ������Ʈ ����
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        bestscoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // GameManager�� �̱������� ����� ��𼭵� ���� GameManager �ν��Ͻ��� �����Ͽ�
        // ���� �����͸� ������ �� �ִ�.
        int currentScore = GameManager.Instance.CurrentScore;
        int bestScore = GameManager.Instance.BestScore;

        // �ΰ��ӿ� ���� ���
        scoreText.text = currentScore.ToString();
        bestscoreText.text = bestScore.ToString();
    }
}
