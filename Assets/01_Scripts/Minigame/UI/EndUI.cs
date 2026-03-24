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
        // 인게임에 점수 출력
        if (MiniGameManager.Instance != null)
        {
            currentScoreText.text = MiniGameManager.Instance.CurrentScore.ToString();
            bestScoreText.text = MiniGameManager.Instance.BestScore.ToString();
        }
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("MiniGameScene"); // 게임을 재시작하는 함수
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}