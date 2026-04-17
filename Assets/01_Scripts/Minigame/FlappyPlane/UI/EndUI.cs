using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [Header("버튼")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private const string CurrentScoreTextString = "ScoreImage/CurrentScoreText";
    private const string BestScoreTextString = "ScoreImage/BestScoreText";
    private const string ReStartButtonString = "ReStartButton";
    private const string ExitButtonString = "ExitButton";

    private void Reset()
    {
        _currentScoreText = transform.Find(CurrentScoreTextString).GetComponent<TextMeshProUGUI>();
        _bestScoreText = transform.Find(BestScoreTextString).GetComponent<TextMeshProUGUI>();
        _restartButton = transform.Find(ReStartButtonString).GetComponent<Button>();
        _exitButton = transform.Find(ExitButtonString).GetComponent<Button>();
    }

    private void Awake()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
        _exitButton.onClick.AddListener(OnClickExitButton);

        ConnectScoreString();
    }

    private void ConnectScoreString() // 점수를 텍스트에 연결
    {
        if (FlappyBirdGameManager.Instance != null)
        {
            _currentScoreText.text = FlappyBirdGameManager.Instance.CurrentScore.ToString();
            _bestScoreText.text = FlappyBirdGameManager.Instance.BestScore.ToString();
        }
    }

    public void OnClickRestartButton()
    {
        
    }

    public async void OnClickExitButton()
    {
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }
}