using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

// 싱글톤 static
// 제네릭하게 쓰는 방법, 옵션을 주는 방법 DontDestroyObject

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LoadSceneManager _sceneManager;
    [SerializeField] private UIManager _uiManager;

    private void Reset()
    {
        _uiManager = GetComponentInChildren<UIManager>();
        _sceneManager = GetComponentInChildren<LoadSceneManager>();
    }

    /// <summary>
    /// 씬 이동
    /// </summary>
    /// <param name="sceneName">이동할 씬 이름</param>
    public async Task ChangeScene(string sceneName)
    {
        await UIManager.Instance.FadeIn(1f);

        UIManager.Instance.OnLoadingPanel(); // 로딩 패널 켜기

        // 비동기 씬 로드 시작 (백그라운드에서 미리 돌리기)
        var loadOp = SceneManager.LoadSceneAsync(sceneName);
        loadOp.allowSceneActivation = false;

        // 로딩 연출(최소 2초 보장)
        float timer = 0f;
        while (timer < 2.0f || loadOp.progress < 0.9f)
        {
            timer += Time.deltaTime;
            await Task.Yield(); // 코루틴의 yield return null; 같음 / 다음 프레임에 돌아옴
        }
         
        loadOp.allowSceneActivation = true; // 씬 활성화

        UIManager.Instance.OffLoadingPanel(); // 로딩 패널 끄기
        await UIManager.Instance.FadeOut(1f);
    }
}
