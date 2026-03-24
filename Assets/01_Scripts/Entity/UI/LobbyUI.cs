using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [Header("로비 버튼 UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _SoundButton;

    private const string Start_ButtonString = "Start_Button";

    private void Reset()
    {
        _startButton = GameObject.Find(Start_ButtonString).GetComponent<Button>();
    }

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        GameManager.Instance.LoadScene(SceneNames.MainScene);
    }

    private void OnLoadButtonClicked()
    {

    }

    private void OnSoundButtonClicked()
    {

    }
}
