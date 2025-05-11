using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText; // �ν����Ϳ��� �Ҵ� ����
    [SerializeField] string[] _zombieDialogue; // �ν����Ϳ� ���� ������ �߰�

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO : �浹ü�� �� �÷��̾� ������ ������ ��
        RandomDialogue(); // ���� ������ ���
        MainGameManager.Instance.UpdateNoticeText("��ȣ�ۿ� ���� �Դϴ�.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // TODO : �浹ü�� �� �÷��̾� ������ ������� ��
        _dialogueText.gameObject.SetActive(false); // �ؽ�Ʈ ȭ�� �����
        MainGameManager.Instance.UpdateNoticeText("��ȣ�ۿ� ������ ������ϴ�.");
    }

    private void RandomDialogue()
    {
        int randomIndex = Random.Range(0, _zombieDialogue.Length); // 0���� �迭 ���� ��ŭ �������� ���� ����

        _dialogueText.text = _zombieDialogue[randomIndex];  // ���ڿ� �迭���� �������� ���� ���
        _dialogueText.gameObject.SetActive(true); // �ؽ�Ʈ ȭ�鿡 ���
    }
}
