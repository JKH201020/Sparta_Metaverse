using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerModeManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _mainController;
    [SerializeField] private MonoBehaviour _shootingController;

    private const string MiniGameScene_2String = "MiniGameScene_2";

    private void Reset()
    {
        _mainController = GetComponent<PlayerController>();
        _shootingController = GetComponent<ShootingController>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때마다 유니티가 호출해주는 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdatePlayerMode(scene.name);
    }

    private void UpdatePlayerMode(string sceneName)
    {
        if (sceneName == MiniGameScene_2String)
        {
            _mainController.enabled = false;
            _shootingController.enabled = true;
            Debug.Log("모드 전환: 슈팅 컨트롤러 활성화");
        }
        else
        {
            _mainController.enabled = true;
            _shootingController.enabled = false;
            Debug.Log("모드 전환: 플레이어 컨트롤러 활성화");
        }
    }
}
