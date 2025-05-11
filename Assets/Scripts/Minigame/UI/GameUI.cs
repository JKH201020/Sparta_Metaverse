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

        // 자식 오브젝트에서 각 컴포넌트 연결
        currentscoreText = transform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestscoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        // GameManager를 싱글톤으로 만들면 어디서든 쉽게 GameManager 인스턴스에 접근하여
        // 점수 데이터를 가져올 수 있다.
        int currentScore = MiniGameManager.Instance.CurrentScore;
        int bestScore = MiniGameManager.Instance.BestScore;

        // 인게임에 점수 출력
        currentscoreText.text = currentScore.ToString();
        bestscoreText.text = bestScore.ToString();
    }
}
