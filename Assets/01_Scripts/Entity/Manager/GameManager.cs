using UnityEngine;

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

    #region MainUIManager

    public void UpdateNoticeText(string text)
    {
        _uiManager.UpdateNoticeText(text); // UIManager.cs에서 UpdateNoticeText를 불러옴
    }

    #endregion

    #region LoadSceneManager

    /// <summary>
    /// 씬을 불러옴
    /// </summary>
    /// <param name="sceneName">이동할 씬 이름</param>
    public void LoadScene(string sceneName)
    {
        _sceneManager.LoadScene(sceneName); //LoadSceneManager에서 LoadScene을 불러옴
    }

    #endregion
}
