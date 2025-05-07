using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{
    public static PlayerPositionManager Instance { get; private set; } // 싱글톤

    Vector3 lastPosition; // 메인 씬에서의 현재 위치
    string lastSceneName; // 메인 씬

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 현재 위치와 씬 저장
    public void SetLastPositionAndScene(Vector3 position, string sceneName)
    {
        lastPosition = position; // 플레이어가 마지막으로 위치했던 좌표를 저장
        lastSceneName = sceneName; // 플레이어가 마지막으로 있었던 씬의 이름을 저장
    }

    //저장된 위치와 씬 이름을 반환
    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }

    public string GetLastSceneName()
    {
        return lastSceneName;
    }
}
