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

        // "ScoreImage" 오브젝트 찾기
        Transform scoreImageTransform = transform.Find("ScoreImage");

        // 자식 오브젝트에서 각 컴포넌트 연결
        currentScoreText = scoreImageTransform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = scoreImageTransform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        restartButton = transform.Find("ReStartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        // 버튼 클릭 시 이벤트 연결
        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // UI에 점수 정보 표시
    void Update()
    {
        // GameManager를 싱글톤으로 만들면 어디서든 쉽게 GameManager 인스턴스에 접근하여
        // 점수 데이터를 가져올 수 있다.
        int currentScore = GameManager.Instance.CurrentScore;
        int bestScore = GameManager.Instance.BestScore;

        // 인게임에 점수 출력
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