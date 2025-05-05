using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{
    public static PlayerPositionManager Instance { get; private set; } // �̱���

    Vector3 lastPosition; // ���� �������� ���� ��ġ
    string lastSceneName; // ���� ��

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �̵� �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� ��ġ�� �� ����
    public void SetLastPositionAndScene(Vector3 position, string sceneName)
    {
        lastPosition = position; // �÷��̾ ���������� ��ġ�ߴ� ��ǥ�� ����
        lastSceneName = sceneName; // �÷��̾ ���������� �־��� ���� �̸��� ����
    }

    //����� ��ġ�� �� �̸��� ��ȯ
    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }

    public string GetLastSceneName()
    {
        return lastSceneName;
    }
}
