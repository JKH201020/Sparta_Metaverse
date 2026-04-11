using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [Header("로비 버튼 UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _soundButton;

    private const string StartButtonString = "StartButton";
    private const string LoadButtonString = "LoadButton";

    private void Reset()
    {
        _startButton = GameObject.Find(StartButtonString).GetComponent<Button>();
        _loadButton = GameObject.Find(LoadButtonString).GetComponent<Button>();
    }

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _loadButton.onClick.AddListener(OnLoadButtonClicked);
    }

    private async void OnStartButtonClicked()
    {
        // 나중에 코드 수정하기
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    } 

    private void OnLoadButtonClicked()
    {
        UIManager.Instance.OnLoadSlotUI();
    }

    private void OnSoundButtonClicked()
    {

    }
}
