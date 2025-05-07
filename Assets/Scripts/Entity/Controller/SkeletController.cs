using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletController : MonoBehaviour
{
    public GameObject textCanvas; // ���� UI ������Ʈ (Inspector���� �Ҵ�)
    public DialogueManager dm; // DialogueManager�� Inspector���� �Ҵ�

    public string sceneName; // �̵��� ���� �̸� (Inspector â���� ����)
    bool isInteract = false; // ��ȣ�ۿ� ��������
    bool isDialogueActive = false; // ��ȭ�� Ȱ��ȭ�Ǿ�����

    void Start()
    {
        dm.OnDialogueEnd += AfterTalking; // DialogueManager�� ��ȭ ���� �̺�Ʈ�� �ݹ� �Լ� ���
    }

    void Update()
    {
        // �÷��̾ Ʈ���� ���� �ȿ� �ִ� ���� ������ ��ȣ�ۿ� Ű (F)�� ������
        if (isInteract && !isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            Interactive();
        }

    }

    void OnTriggerEnter2D(Collider2D other) // �÷��̾ Ʈ���ſ� ������ �� ����Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        {
            TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

            textCanvas.SetActive(true); // ��ȣ�ۿ� �ؽ�Ʈ ���
            isInteract = true; // ��ȣ�ۿ� ����
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        {
            textCanvas.SetActive(false); // ��ȣ�ۿ� �ؽ�Ʈ �����
            isInteract = false; // ��ȣ�ۿ� �Ұ�

            // ��ȭ �ʱ�ȭ
            dm.SettingUI(false);
            dm.currentTextIndex = 0;
            dm.isWaitingInput = false;
            isDialogueActive = false;
        }
    }

    void Interactive() // ��ȣ�ۿ�
    {
        isInteract = true;
        isDialogueActive = true; // ��ȭ�� ���۵Ǿ����� ǥ��

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        {
            dm.ShowDialogue(); // ��ȭ ����

            PlayerPositionManager.Instance.SetLastPositionAndScene(
                player.transform.position, SceneManager.GetActiveScene().name); // ���� ��ġ�� �� �̸� ����
        }
    }

    void AfterTalking()
    {
        isDialogueActive = false; // ��ȭ�� �������� ǥ��
        dm.gameObject.SetActive(false); // �� �ε� ���� DialogueManager ������Ʈ�� ��Ȱ��ȭ

        if (isInteract && !isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(sceneName); // ��ȣ�ۿ�(�̴ϰ������� �̵�)
        }
    }
}
