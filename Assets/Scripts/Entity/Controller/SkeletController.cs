using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletController : MonoBehaviour
{
    // ĳ���� �������� �ٶ󺸰� �ϱ�

    public GameObject textCanvas; // ���� UI ������Ʈ (Inspector���� �Ҵ�)
    public string sceneName; // �̵��� ���� �̸� (Inspector â���� ����)


    private bool Interact = false; // ��ȣ�ۿ� ��������

    void OnTriggerEnter2D(Collider2D other) // �÷��̾ Ʈ���ſ� ������ �� ����Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        {
            // ���ϴ� �̺�Ʈ ���� (��: �̴ϰ��� ����, ��ȭ â ���� ��)
            if (textCanvas != null)
            {
                TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

                textCanvas.SetActive(true); // ��ȣ�ۿ� �ؽ�Ʈ ���

                Interact = true; // ��ȣ�ۿ� ����
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (textCanvas != null)
        {
            textCanvas.SetActive(false); // ��ȣ�ۿ� �ؽ�Ʈ �����

            Interact = false; // ��ȣ�ۿ� �Ұ�
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // �÷��̾ Ʈ���� ���� �ȿ� �ִ� ���� ������ ��ȣ�ۿ� Ű (F)�� ������
        if (Interact && Input.GetKeyDown(KeyCode.F))
        {
            PlayerPositionManager.Instance.SetLastPositionAndScene(
                player.transform.position, SceneManager.GetActiveScene().name); // ���� ��ġ�� �� �̸� ����

            SceneManager.LoadScene(sceneName); // ��ȣ�ۿ�(�̴ϰ������� �̵�)
        }
    }
}
