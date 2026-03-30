using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerModeManager : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _mainController;
    [SerializeField] private MonoBehaviour _shootingController;

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
        if (sceneName == SceneNames.MiniGameScene_2)
        {
            _mainController.enabled = false;
            _shootingController.enabled = true;
        }
        else
        {
            _mainController.enabled = true;
            _shootingController.enabled = false;
        }
    }
}
