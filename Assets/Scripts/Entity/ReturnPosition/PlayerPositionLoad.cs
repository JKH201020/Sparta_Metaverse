using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionLoad : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ (Inspector���� ����)
    public string returnScene; // ���ƿ� ���� �� �̸� (Inspector���� ����)

    void Start()
    {
        // PlayerPositionManager�� �����ϰ�, ����� �� �̸��� ���� �� �̸��� ���ٸ� ��ġ�� ����
        if (PlayerPositionManager.Instance != null &&
                PlayerPositionManager.Instance.GetLastSceneName() == SceneManager.GetActiveScene().name)
        {
            if (player != null)
            {
                player.transform.position = PlayerPositionManager.Instance.GetLastPosition();
            }
        }

        // ��ġ�� ���������� ����� ������ �ʱ�ȭ -> ���� �� ��ġ ������ ����
        PlayerPositionManager.Instance.SetLastPositionAndScene(Vector3.zero, null);
    }
}
