using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private const string StartButtonString = "StartButton";
    private const string ExitButtonString = "ExitButton";

    private void Reset()
    {
        _startButton = transform.Find(StartButtonString).GetComponent<Button>();
        _exitButton = transform.Find(ExitButtonString).GetComponent<Button>();
    }

    private void Awake()
    {
        _startButton.onClick.AddListener(OnClickStartButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnEnable()
    {
        GameManager.Instance.ChangeState(GameState.Paused);
    }

    public void OnClickStartButton() // Start 버튼 클릭 시 게임 시작 요청
    {
        UIManager.Instance.OffHomeUI();
        UIManager.Instance.OnGameUI();
    }

    public async void OnClickExitButton() // Exit 버튼 클릭 시 게임 종료 요청
    {
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }
}