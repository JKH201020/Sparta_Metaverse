using TMPro;
using UnityEngine;

public class TopDownUI : MonoBehaviour
{
    [Header("점수 UI")]
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _highScore;

    private const string ScorePathString = "Score/Text - Score";
    private const string HighScorePathString = "Score/Text - HighScore";

    private void Reset()
    {
        _score = transform.Find(ScorePathString).GetComponent<TextMeshProUGUI>();
        _highScore = transform.Find(HighScorePathString).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (TopDownManager.Instance != null)
        {
            TopDownManager.Instance.OnScoreChanged += UpdateScoreUI;

            UpdateScoreUI(TopDownManager.Instance.CurrentScore);
        }
    }

    private void OnDisable()
    {
        if (TopDownManager.Instance != null) TopDownManager.Instance.OnScoreChanged -= UpdateScoreUI;
    }

    /// <summary>
    /// 점수UI 업데이트
    /// </summary>
    /// <param name="currentScore">현재 점수</param>
    public void UpdateScoreUI(int currentScore)
    {
        _score.text = TopDownManager.Instance.CurrentScore.ToString();
    }
}
