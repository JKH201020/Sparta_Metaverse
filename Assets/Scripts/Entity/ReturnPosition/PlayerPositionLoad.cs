using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionLoad : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트 (Inspector에서 설정)
    public string returnScene; // 돌아올 메인 씬 이름 (Inspector에서 설정)

    void Start()
    {
        // PlayerPositionManager가 존재하고, 저장된 씬 이름이 현재 씬 이름과 같다면 위치를 설정
        if (PlayerPositionManager.Instance != null &&
                PlayerPositionManager.Instance.GetLastSceneName() == SceneManager.GetActiveScene().name)
        {
            if (player != null)
            {
                player.transform.position = PlayerPositionManager.Instance.GetLastPosition();
            }
        }

        // 위치를 설정했으면 저장된 정보는 초기화 -> 복귀 후 위치 데이터 삭제
        PlayerPositionManager.Instance.SetLastPositionAndScene(Vector3.zero, null);
    }
}
