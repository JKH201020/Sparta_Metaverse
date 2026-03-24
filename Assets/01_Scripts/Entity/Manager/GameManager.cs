using UnityEngine;

// 싱글톤 static
// 제네릭하게 쓰는 방법, 옵션을 주는 방법 DontDestroyObject

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LoadSceneManager _sceneManager;
    [SerializeField] private UIManager _uiManager;

    #region MainUIManager

    public void UpdateNoticeText(string text)
    {
        _uiManager.UpdateNoticeText(text); // UIManager.cs에서 UpdateNoticeText를 불러옴
    }

    #endregion

    #region LoadSceneManager

    public void LoadScene(string sceneName) // 씬을 불러옴
    {
        _sceneManager.LoadScene(sceneName); //LoadSceneManager에서 LoadScene을 불러옴
    }

    #endregion
}
