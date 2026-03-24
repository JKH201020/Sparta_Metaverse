using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public GameObject playerObject;

    public void LoadScene(string sceneName)
    {
        PlayerPositionManager.Instance.SetLastPositionAndScene(
            playerObject.transform.position, SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
    }
}
