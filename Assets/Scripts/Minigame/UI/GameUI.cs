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

        // 자식 오브젝트에서 각 컴포넌트 연결
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        bestscoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // GameManager를 싱글톤으로 만들면 어디서든 쉽게 GameManager 인스턴스에 접근하여
        // 점수 데이터를 가져올 수 있다.
        int currentScore = GameManager.Instance.CurrentScore;
        int bestScore = GameManager.Instance.BestScore;

        // 인게임에 점수 출력
        scoreText.text = currentScore.ToString();
        bestscoreText.text = bestScore.ToString();
    }
}
