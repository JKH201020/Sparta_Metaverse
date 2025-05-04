using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI startText;

    public void Start()
    {
        restartText.gameObject.SetActive(false); // ó������ "�ٽ� ����" �ؽ�Ʈ�� ���ܵ�
    }

    public void SetRestart() // ���� ���� ���¿��� �ٽ� ���� �޽����� ���̰� ��
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score) // ���� ������ UI�� ������Ʈ
    {
        scoreText.text = score.ToString(); // ������ ���ڿ��� ��ȯ�Ͽ� ���
    }
}
