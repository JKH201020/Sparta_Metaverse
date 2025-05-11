using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : BaseUI
{
    public TextMeshProUGUI currentscoreText;
    public TextMeshProUGUI bestscoreText;

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public override void Init(MiniGameUIManager uiManager)
    {
        base.Init(uiManager);

        // �ڽ� ������Ʈ���� �� ������Ʈ ����
        currentscoreText = transform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestscoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        // GameManager�� �̱������� ����� ��𼭵� ���� GameManager �ν��Ͻ��� �����Ͽ�
        // ���� �����͸� ������ �� �ִ�.
        int currentScore = MiniGameManager.Instance.CurrentScore;
        int bestScore = MiniGameManager.Instance.BestScore;

        // �ΰ��ӿ� ���� ���
        currentscoreText.text = currentScore.ToString();
        bestscoreText.text = bestScore.ToString();
    }
}
