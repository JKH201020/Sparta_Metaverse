using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletController : MonoBehaviour
{
    #region ���� �ڵ�

    //public GameObject textCanvas; // ���� UI ������Ʈ (Inspector���� �Ҵ�)
    //public DialogueManager dm; // DialogueManager�� Inspector���� �Ҵ�

    //bool isInteract = false; // ��ȣ�ۿ� ��������
    //bool isDialogueActive = false; // ��ȭ�� Ȱ��ȭ�Ǿ�����

    //void Start()
    //{
    //    // dm.OnDialogueEnd += AfterTalking; // DialogueManager�� ��ȭ ���� �̺�Ʈ�� �ݹ� �Լ� ���
    //}

    //void Update()
    //{
    //    // �÷��̾ Ʈ���� ���� �ȿ� �ִ� ���� ������ ��ȣ�ۿ� Ű (F)�� ������
    //    if (isInteract && !isDialogueActive && Input.GetKeyDown(KeyCode.F))
    //    {
    //        Interactive();
    //    }

    //}

    //void OnTriggerEnter2D(Collider2D other) // �÷��̾ Ʈ���ſ� ������ �� ����Ǵ� �Լ�
    //{
    //    if (other.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
    //    {
    //        TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

    //        textCanvas.SetActive(true); // ��ȣ�ۿ� �ؽ�Ʈ ���
    //        isInteract = true; // ��ȣ�ۿ� ����
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
    //    {
    //        textCanvas.SetActive(false); // ��ȣ�ۿ� �ؽ�Ʈ �����
    //        isInteract = false; // ��ȣ�ۿ� �Ұ�

    //        // ��ȭ �ʱ�ȭ
    //        //dm.SettingUI(false);
    //        //dm.currentTextIndex = 0;
    //        //dm.isWaitingInput = false;
    //        isDialogueActive = false;
    //    }
    //}

    //void Interactive() // ��ȣ�ۿ�
    //{
    //    isInteract = true;
    //    isDialogueActive = true; // ��ȭ�� ���۵Ǿ����� ǥ��

    //    GameObject player = GameObject.FindGameObjectWithTag("Player");

    //    if (player.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
    //    {
    //        //dm.ShowDialogue(); // ��ȭ ����

    //        //PlayerPositionManager.Instance.SetLastPositionAndScene(
    //        //    player.transform.position, SceneManager.GetActiveScene().name); // ���� ��ġ�� �� �̸� ����
    //    }
    //}

    //void AfterTalking()
    //{
    //    isDialogueActive = false; // ��ȭ�� �������� ǥ��
    //    dm.gameObject.SetActive(false); // �� �ε� ���� DialogueManager ������Ʈ�� ��Ȱ��ȭ

    //    if (isInteract && !isDialogueActive && Input.GetKeyDown(KeyCode.F))
    //    {
    //        // MainGameManager���� LoadScene�� �ҷ��� (�̴ϰ������� �̵�)
    //        MainGameManager.Instance.LoadScene("MiniGameScene");
    //    }
    //}

    #endregion

    #region ���� ��� �ڵ�

    public GameObject textCanvas; // ���� UI ������Ʈ (Inspector���� �Ҵ�)
    public DialogueManager dialogueManager; // DialogueManager�� Inspector���� �Ҵ�

    bool isInteract = false; // ��ȣ�ۿ� ��������
    bool isDialogueActive = false; // ��ȭ�� Ȱ��ȭ�Ǿ�����

    void Start()
    {
        // DialogueManager�� ��ȭ ���� �̺�Ʈ�� AfterTalking �Լ� ���
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueEnd += AfterTalking;
        }
        else
        {
            Debug.LogError("SkeletController: DialogueManager�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    void Update()
    {
        // �÷��̾ Ʈ���� ���� �ȿ� �ִ� ���� ������ ��ȣ�ۿ� Ű (F)�� ������
        if (isInteract && Input.GetKeyDown(KeyCode.F))
        {
            if (!isDialogueActive) // ��ȭ�� Ȱ��ȭ���� �ʾҴٸ� (ó�� ��ȭ ����)
            {
                Interactive(); // ��ȭ ����
            }
            else // ��ȭ�� Ȱ��ȭ ���̶�� (���� ��ȭ ���� �Ǵ� ��ȭ ���� ��ŵ)
            {
                dialogueManager.Nextdialogue();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) // �÷��̾ Ʈ���ſ� ������ �� ����Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        {
            //TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

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

            if (isDialogueActive)
            {
                isDialogueActive = false; // ��ȭ ����

                if (dialogueManager != null) // ��ȭ �ڷ�ƾ�� ���� ���̶�� ���� ����
                {
                    StopAllCoroutines(); // ���� ��ũ��Ʈ�� �ڷ�ƾ ����
                    dialogueManager.SettingUI(false); // ��ȭâ UI ��Ȱ��ȭ
                    dialogueManager.StopAllCoroutines(); // DialogueManager�� �ڷ�ƾ�� ��� ����
                }
            }
        }
    }

    void Interactive() // ��ȣ�ۿ�
    {
        //isInteract = true;
        isDialogueActive = true; // ��ȭ�� ���۵Ǿ����� ǥ��
        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue();

    }

    void AfterTalking()
    {
        isDialogueActive = false; // ��ȭ�� �������� ǥ��
        dialogueManager.gameObject.SetActive(false); // �� �ε� ���� DialogueManager ������Ʈ�� ��Ȱ��ȭ
        HandleSceneTransition();
    }

    void HandleSceneTransition()
    {
        // MainGameManager���� LoadScene�� �ҷ��� (�̴ϰ������� �̵�)
        if (MainGameManager.Instance != null)
        {
            MainGameManager.Instance.LoadScene("MiniGameScene");
            Debug.Log("SkeletController: MiniGameScene���� �̵��մϴ�.");
        }
        else
        {
            Debug.LogError("SkeletController: MainGameManager.Instance�� null�Դϴ�. �� ��ȯ ����.");
        }
    }

    #endregion
}
