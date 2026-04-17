using UnityEngine;

// 게임의 UI 상태를 정의하는 열거형
public enum UIState
{
    Home,
    Game,
    End
}

public class MiniGameUIManager : MonoBehaviour
{
    static MiniGameUIManager instance;
    public static MiniGameUIManager Instance { get { return instance; } }

    UIState currentState = UIState.Home;

    [SerializeField] private HomeUI _homeUI;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private EndUI _endUI;

    private void Awake()
    {
        instance = this;
        ChangeState(UIState.Home);
    }

    private void Reset()
    {
        _homeUI = GetComponentInChildren<HomeUI>(true);
        _gameUI = GetComponentInChildren<GameUI>(true);
        _endUI = GetComponentInChildren<EndUI>(true);
    }

    public void ChangeState(UIState state) // UI 전환
    {
        currentState = state;
        _homeUI?.SetActive(currentState);
        _gameUI?.SetActive(currentState);
        _endUI?.SetActive(currentState);
    }

    #region UI 이벤트

    public void OnClickStart()
    {
        ChangeState(UIState.Game); // UI를 게임 화면으로 전환
    }

    public async void OnClickExit()
    {
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }

    public void GameOver()
    {
        ChangeState(UIState.End); // UI를 게임 오버화면으로 전환
    }

    #endregion

}
