using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentscoreText;
    [SerializeField] private TextMeshProUGUI bestscoreText;

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
    }

    private void Update()
    {
        ScoreUpdate();
    }

    private void ScoreUpdate() // 화면에 표시되는 점수 텍스트와 연결 
    {
        currentscoreText.text = FlappyBirdGameManager.Instance.CurrentScore.ToString();
        bestscoreText.text = FlappyBirdGameManager.Instance.BestScore.ToString();
    }
}
