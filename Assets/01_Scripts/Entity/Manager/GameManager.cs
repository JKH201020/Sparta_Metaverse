using UnityEngine;
using UnityEngine.SceneManagement;

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
    /// 씬을 불러옴
    /// </summary>
    /// <param name="sceneName">이동할 씬 이름</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
