using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    // �̱��� ������ ���� ����. ���� ������ ������ �ν��Ͻ��� ���
    public static MiniGameManager gameManager;

    // �̱��� ���ٿ� ������Ƽ (�ܺο��� GameManager.Instance�� ȣ�� ����)
    public static MiniGameManager Instance { get { return gameManager; } }
    [SerializeField] private GameObject _player;
    [SerializeField] private MiniFollowCamera _followCamera;

    int currentScore = 0; // ���� ������ �����ϴ� ����
    public int CurrentScore { get => currentScore; }

    int bestScore = 0; // �ְ� ���� ���� ����
    public int BestScore { get => bestScore; }

    public const string BestScoreKey = "BestScore"; // PlayerPrefs ���� Ű

    void Awake()
    {
        gameManager = this; // GameManager �ν��Ͻ��� gameManager�� �Ҵ� (�̱��� �ʱ�ȭ)
        DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
    }

    void Start()
    {
        Plane.isDead = false; // ���� ��
        currentScore = 0; // ���� ���� �� ������ 0���� �ʱ�ȭ�Ͽ� UI�� ǥ��
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0); // ����� �ְ� ���� �ҷ����� (������ �⺻�� 0)
        Spawn();
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
            PlayerPrefs.SetFloat(BestScoreKey, currentScore);
            PlayerPrefs.Save();
        }
    }

    public void Spawn() // ������������ �ִ� ����� ��ȯ
    {
        GameObject go = Instantiate(_player, Vector2.zero, Quaternion.identity);
        _followCamera.SetTarget(go.transform);
    }
}
