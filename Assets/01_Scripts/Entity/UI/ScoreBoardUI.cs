using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardUI : MonoBehaviour
{
    [Header("Score")]
    TextMeshProUGUI currentScoreText;
    TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        Init();

        if (MiniGameManager.Instance != null && bestScoreText != null)
        {
            currentScoreText.text = MiniGameManager.Instance.BestScore.ToString();
            bestScoreText.text = MiniGameManager.Instance.BestScore.ToString();
        }
    }

    private void Init()
    {
        currentScoreText = transform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MiniGameManager.Instance != null && currentScoreText != null && bestScoreText != null)
        {
            int currentScore = MiniGameManager.Instance.CurrentScore;
            int bestScore = MiniGameManager.Instance.BestScore;

            currentScoreText.text = currentScore.ToString();
            bestScoreText.text = bestScore.ToString();
        }
    }
}
