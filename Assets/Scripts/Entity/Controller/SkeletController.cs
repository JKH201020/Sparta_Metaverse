using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletController : MonoBehaviour
{
    public GameObject textCanvas; // ���� UI ������Ʈ (Inspector���� �Ҵ�)
    //DialogueManager dm;

    public string sceneName; // �̵��� ���� �̸� (Inspector â���� ����)
    bool Interact = false; // ��ȣ�ۿ� ��������

    void Start()
    {
        //dm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (Interact && Input.GetKeyDown(KeyCode.F))
        {
            Interactive();
        }
    }

    void OnTriggerEnter2D(Collider2D other) // �÷��̾ Ʈ���ſ� ������ �� ����Ǵ� �Լ�
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        {
            // ���ϴ� �̺�Ʈ ���� (��: �̴ϰ��� ����, ��ȭ â ���� ��)
            TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

            textCanvas.SetActive(true); // ��ȣ�ۿ� �ؽ�Ʈ ���
            Interact = true; // ��ȣ�ۿ� ����
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        {
            textCanvas.SetActive(false); // ��ȣ�ۿ� �ؽ�Ʈ �����
            Interact = false; // ��ȣ�ۿ� �Ұ�

            //dm.SettingUI(Interact);
        }
    }

    void Interactive()
    {
        Interact = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && player.CompareTag("Player")) // Ʈ���ſ� ���� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        {
            // �÷��̾ Ʈ���� ���� �ȿ� �ִ� ���� ������ ��ȣ�ۿ� Ű (F)�� ������

            PlayerPositionManager.Instance.SetLastPositionAndScene(
                player.transform.position, SceneManager.GetActiveScene().name); // ���� ��ġ�� �� �̸� ����

            SceneManager.LoadScene(sceneName); // ��ȣ�ۿ�(�̴ϰ������� �̵�)

            //dm.ShowDialogue();
        }

        //StartCoroutine(NextScene());
    }

    //IEnumerator NextScene()
    //{
    //    yield return new WaitForSeconds(2f);

    //    if (Interact && Input.GetKeyDown(KeyCode.F))
    //    {
    //        SceneManager.LoadScene(sceneName); // ��ȣ�ۿ�(�̴ϰ������� �̵�)
    //    }
    //}
}
