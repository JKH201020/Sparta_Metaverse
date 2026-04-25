using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopDownUI : MonoBehaviour
{
    [Header("체력 UI")]
    [SerializeField] private Slider _hpBar;

    [Header("점수 UI")]
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _highScore;

    [Header("게임 오버UI")]
    [SerializeField] private TextMeshProUGUI _gameOverScore;
    [SerializeField] private TextMeshProUGUI _gameOverHighScore;
    [SerializeField] private Button _retryButton; // 다시하기 버튼
    [SerializeField] private Button _homeButton; // 홈으로 버튼

    [SerializeField] private HealthSystem _healthSystem;

    private const string ScorePathString = "Score/Text - Score";
    private const string HighScorePathString = "Score/Text - HighScore";
    private const string GOScorePathString = "GameOverUI/Image - BG/Text - Score";
    private const string GOHighScorePathString = "GameOverUI/Image - BG/Text - HighScore";
    private const string RetryButtonPathString = "GameOverUI/Button/Button - Retry";
    private const string HomeButtonPathString = "GameOverUI/Button/Button - Home";
    private const string HpBarPathString = "HpBar";

    private void Reset()
    {
        _score = transform.Find(ScorePathString).GetComponent<TextMeshProUGUI>();
        _highScore = transform.Find(HighScorePathString).GetComponent<TextMeshProUGUI>();
        _gameOverScore = transform.Find(GOScorePathString).GetComponent<TextMeshProUGUI>();
        _gameOverHighScore = transform.Find(GOHighScorePathString).GetComponent<TextMeshProUGUI>();
        _retryButton = transform.Find(RetryButtonPathString).GetComponent<Button>();
        _homeButton = transform.Find(HomeButtonPathString).GetComponent<Button>();
        _hpBar = transform.Find(HpBarPathString).GetComponent<Slider>();
    }

    private void Awake()
    {
        _healthSystem = GameObject.FindWithTag(Tag.Player).GetComponent<HealthSystem>();
    }

    private void Start()
    {
        _retryButton.onClick.AddListener(RetryButtonClicked);
        _homeButton.onClick.AddListener(HomeButtonClicked);
    }

    private void OnEnable()
    {
        if (TopDownManager.Instance != null)
        {
            TopDownManager.Instance.OnScoreChanged += UpdateScoreUI;
            TopDownManager.Instance.OnRestart += ResetUI;

            UpdateScoreUI(TopDownManager.Instance.CurrentScore, TopDownManager.Instance.HighScore);
        }

        if (_healthSystem != null)
        {
            StopAllCoroutines();

            _hpBar.maxValue = _healthSystem.MaxHealth;
            _hpBar.value = _healthSystem.CurrentHealth;

            _healthSystem.OnTakeDamage += UpdateHealthUI;
        }
    }

    private void OnDisable()
    {
        if (TopDownManager.Instance != null)
        {
            TopDownManager.Instance.OnScoreChanged -= UpdateScoreUI;
            TopDownManager.Instance.OnRestart -= ResetUI;
        }

        if (_healthSystem != null) _healthSystem.OnTakeDamage -= UpdateHealthUI;
    }

    #region 버튼 이벤트

    public void RetryButtonClicked() // 재시도 버튼 이벤트
    {
        if (UIManager.Instance != null) UIManager.Instance.OffGameOverUI();
        TopDownManager.Instance.Restart();
    }

    public void HomeButtonClicked() // 홈으로 버튼 이벤트
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.OffGameOverUI();
            UIManager.Instance.OffTopDownGameUI();
        }

        if (GameManager.Instance != null) _ = GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }

    #endregion

    #region 점수 UI

    /// <summary>
    /// 점수UI 업데이트
    /// </summary>
    /// <param name="currentScore">현재 점수</param>
    public void UpdateScoreUI(int currentScore, int highScore)
    {
        _score.text = currentScore.ToString();
        _gameOverScore.text = currentScore.ToString();
        _highScore.text = highScore.ToString();
        _gameOverHighScore.text = highScore.ToString();
    }

    #endregion

    #region 체력 UI

    /// <summary>
    /// 체력 UI 업데이트
    /// </summary>
    /// <param name="targetHp">현재 체력</param>
    public void UpdateHealthUI(float targetHp)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateRoutine(targetHp));
    }

    IEnumerator UpdateRoutine(float targetHp)
    {
        float startHp = _hpBar.value;
        float elapsed = 0f;
        float duration = 0.2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _hpBar.value = Mathf.Lerp(startHp, targetHp, elapsed / duration);
            yield return null;
        }

        _hpBar.value = targetHp;

        if (_healthSystem.CurrentHealth <= 0) _healthSystem.Die();
    }

    #endregion

    private void ResetUI()
    {
        StopAllCoroutines();

        if(_healthSystem != null)
        {
            _hpBar.maxValue = _healthSystem.MaxHealth;
            _hpBar.value = _healthSystem.MaxHealth;
        }
    }
}
