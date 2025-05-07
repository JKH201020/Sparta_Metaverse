using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // �̱��� ������ ���� ����. ���� ������ ������ �ν��Ͻ��� ���

    // �̱��� ���ٿ� ������Ƽ (�ܺο��� GameManager.Instance�� ȣ�� ����)
    public static GameManager Instance { get { return gameManager; } }

    int currentScore = 0; // ���� ������ �����ϴ� ����
    public int CurrentScore { get => currentScore; }

    int bestScore = 0; // �ְ� ���� ���� ����
    public int BestScore { get => bestScore; }

    const string BestScoreKey = "BestScore"; // PlayerPrefs ���� Ű

    void Awake()
    {
        gameManager = this; // GameManager �ν��Ͻ��� gameManager�� �Ҵ� (�̱��� �ʱ�ȭ)
        DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
    }

    void Start()
    {
        Player.isDead = false; // ���� ��
        currentScore = 0; // ���� ���� �� ������ 0���� �ʱ�ȭ�Ͽ� UI�� ǥ��
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0); // ����� �ְ� ���� �ҷ����� (������ �⺻�� 0)

        Time.timeScale = 0; // ���� �� ���� ����
    }

    public void GameOver() // ���� ���� �� ȣ��Ǵ� �Լ�
    {

    }

    public void RestartGame() // ������ ������ϴ� �Լ�
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������ ������ϴ� �Լ�
    }

    public void UpdateScore(int score) // ������ �߰��ϴ� �Լ�
    {
        currentScore += score; // �־��� score�� currentScore�� ����

        if (currentScore >= bestScore)
        {
            bestScore = currentScore; // �ְ� ���� ����

            // PlayerPrefs�� ���� �� ���� ���� �ѵ� ������
            PlayerPrefs.SetInt(BestScoreKey, currentScore);
        }
    }
}
