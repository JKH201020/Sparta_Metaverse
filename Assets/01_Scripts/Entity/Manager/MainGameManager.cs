using UnityEngine;

// 싱글톤 static
// 제네릭하게 쓰는 방법, 옵션을 주는 방법 DontDestroyObject

public class MainGameManager : MonoBehaviour
{
    // 기능 및 관리 (싱글톤 - 어디서든 불러와서 사용할 수 있다.)
    public static MainGameManager Instance { get; private set; }

    [SerializeField] private LoadSceneManager _sceneManager;
    [SerializeField] private MainUIManager _uiManager;

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this; // 자기 자신을 할당해서 null이 되지 않도록 함
        DontDestroyOnLoad(this);
    }

    #region MainUIManager

    public void UpdateNoticeText(string text)
    {
        _uiManager.UpdateNoticeText(text); // MainUIManager.cs에서 UpdateNoticeText를 불러옴
    }

    #endregion

    #region LoadSceneManager

    public void LoadScene(string sceneName) // 씬을 불러옴
    {
        _sceneManager.LoadScene(sceneName); //LoadSceneManager에서 LoadScene을 불러옴
    }

    #endregion
}
