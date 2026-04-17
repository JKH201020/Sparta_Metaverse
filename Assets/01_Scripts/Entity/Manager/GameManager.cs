using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public enum GameState
{
    Loading,
    Playing,
    Talking,
    Paused,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState { get; private set; }

    private void Start() // 특정 씬 구현할 때 조작하기 위한 용도. 구현 끝나면 지우기
    {
        ChangeState(GameState.Playing);
    }

    #region 게임 상태

    public void ChangeState(GameState state)
    {
        CurrentState = state;
        GameObject playerObj = GameObject.FindWithTag(Tag.Player);
        PlayerController playerController = playerObj != null ? playerObj.GetComponent<PlayerController>() : null;

        switch (CurrentState)
        {
            case GameState.Playing:
                Time.timeScale = 1.0f; // 게임 시간 흐름
                playerController?.PlayerInputActivate();
                break;
            case GameState.Loading:
                Time.timeScale = 1.0f;
                playerController?.PlayerInputDeactivate();
                break;
            case GameState.Paused:
                Time.timeScale = 0.0f; // 게임 시간 정지
                break;
            case GameState.Talking:
                break;
            case GameState.GameOver:
                Time.timeScale = 0.0f;
                break;
        }
    }

    #endregion

    #region 씬 이동

    /// <summary>
    /// 씬 이동
    /// </summary>
    /// <param name="sceneName">이동할 씬 이름</param>
    public async Task ChangeScene(string sceneName)
    {
        ChangeState(GameState.Loading);

        await UIManager.Instance.FadeIn(0.5f);

        UIManager.Instance.OnLoadingPanel(); // 로딩 패널 켜기

        // 비동기 씬 로드 시작 (백그라운드에서 미리 돌리기)
        var loadOp = SceneManager.LoadSceneAsync(sceneName);
        loadOp.allowSceneActivation = false;

        // 로딩 연출(최소 2초 보장)
        float timer = 0f;
        while (timer < 2.0f || loadOp.progress < 0.9f)
        {
            timer += Time.unscaledDeltaTime; // unscaledDeltaTime: 현실 시간 기준으로 작동
            await Task.Yield(); // 코루틴의 yield return null; 같음 / 다음 프레임에 돌아옴
        }

        loadOp.allowSceneActivation = true; // 씬 활성화

        UIManager.Instance.OffLoadingPanel(); // 로딩 패널 끄기
        await UIManager.Instance.FadeOut(0.5f);

        ChangeState(GameState.Playing);
    }

    #endregion
}