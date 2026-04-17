using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentscoreText;
    [SerializeField] private TextMeshProUGUI bestscoreText;

    private int currentScore;
    private int bestScore;

    private void Reset()
    {
        currentscoreText = transform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
        bestscoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if(GameManager.Instance != null) GameManager.Instance.ChangeState(GameState.Playing);
    }

    private void OnEnable()
    {
        currentScore = FlappyBirdGameManager.Instance.CurrentScore;
        bestScore = FlappyBirdGameManager.Instance.BestScore;
    }

    void Update()
    {
        // 인게임에 점수 출력
        currentscoreText.text = currentScore.ToString();
        bestscoreText.text = bestScore.ToString();
    }
}
