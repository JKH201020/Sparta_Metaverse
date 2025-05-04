using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    UIManager uiManager; // UI�� ������ UIManager ���� ����
    static GameManager gameManager; // �̱��� ������ ���� ����. ���� ������ ������ �ν��Ͻ��� ���

    // �̱��� ���ٿ� ������Ƽ (�ܺο��� GameManager.Instance�� ȣ�� ����)
    public static GameManager Instance { get { return gameManager; } }

    int currentScore = 0; // ���� ������ �����ϴ� ����

    public UIManager UIManager { get { return uiManager; } } // �ܺο��� ������ �� �ֵ��� ������Ƽ ����

    void Awake()
    {
        gameManager = this; // GameManager �ν��Ͻ��� gameManager�� �Ҵ� (�̱��� �ʱ�ȭ)
        uiManager = FindObjectOfType<UIManager>(); // Scene���� UIManager�� ã�� ������
    }

    public void Start()
    {
        uiManager.UpdateScore(0); // ���� ���� �� ������ 0���� �ʱ�ȭ�Ͽ� UI�� ǥ��
    }

    public void GameOver() // ���� ���� �� ȣ��Ǵ� �Լ�
    {
        uiManager.SetRestart(); // UI�� "�ٽ� ����" �ؽ�Ʈ�� Ȱ��ȭ
    }

    public void RestartGame() // ������ ������ϴ� �Լ�
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������ ������ϴ� �Լ�
    }

    public void AddScore(int score) // ������ �߰��ϴ� �Լ�
    {
        currentScore += score; // �־��� score�� currentScore�� ����
        uiManager.UpdateScore(currentScore); // UI ������Ʈ
    }
}
