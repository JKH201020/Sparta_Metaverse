using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentscoreText;
    [SerializeField] private TextMeshProUGUI bestscoreText;

    private int currentScore;
    private int bestScore;

    private const string CurrentScoreTextString = "CurrentScoreText";
    private const string BestScoreTextString = "BestScoreText";

    private void Reset()
    {
        currentscoreText = transform.Find(CurrentScoreTextString).GetComponent<TextMeshProUGUI>();
        bestscoreText = transform.Find(BestScoreTextString).GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if (GameManager.Instance != null) GameManager.Instance.ChangeState(GameState.Playing);

        if (FlappyBirdGameManager.Instance != null)
        {
            currentScore = FlappyBirdGameManager.Instance.CurrentScore;
            bestScore = FlappyBirdGameManager.Instance.BestScore;
        }
    }

    private void Update()
    {
        ScoreUpdate();
    }

    private void ScoreUpdate() // 화면에 표시되는 점수 텍스트와 연결 
    {
        currentscoreText.text = currentScore.ToString();
        bestscoreText.text = bestScore.ToString();
    }
}
