using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    TextMeshProUGUI currentScoreText;
    TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        Init();

        if (GameManager.Instance != null && bestScoreText != null)
        {
            currentScoreText.text = GameManager.Instance.BestScore.ToString();
            bestScoreText.text = GameManager.Instance.BestScore.ToString();
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
        if (GameManager.Instance != null && currentScoreText != null && bestScoreText != null)
        {
            int currentScore = GameManager.Instance.CurrentScore;
            int bestScore = GameManager.Instance.BestScore;

            currentScoreText.text = currentScore.ToString();
            bestScoreText.text = bestScore.ToString();
        }
    }
}
