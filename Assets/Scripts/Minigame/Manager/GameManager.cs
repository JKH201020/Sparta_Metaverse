using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager; // 싱글톤 패턴을 위한 변수. 게임 내에서 유일한 인스턴스를 사용

    // 싱글톤 접근용 프로퍼티 (외부에서 GameManager.Instance로 호출 가능)
    public static GameManager Instance { get { return gameManager; } }

    int currentScore = 0; // 현재 점수를 저장하는 변수
    public int CurrentScore { get => currentScore; }

    int bestScore = 0; // 최고 점수 저장 변수
    public int BestScore { get => bestScore; }

    const string BestScoreKey = "BestScore"; // PlayerPrefs 저장 키

    void Awake()
    {
        //if(gameManager == null)
        //{
        //    gameManager = this;
        //    DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        //}
        //else if (gameManager != this)
        //{
        //    Destroy(gameObject); // 씬에 이미 GameManager가 있다면 중복 생성을 방지하기 위해 파괴
        //}
            gameManager = this; // GameManager 인스턴스를 gameManager에 할당 (싱글톤 초기화)
    }

    public void Start()
    {
        Player.isDead = false; // 생존 중
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0); // 저장된 최고 점수 불러오기 (없으면 기본값 0)
        currentScore = 0; // 게임 시작 시 점수를 0으로 초기화하여 UI에 표시
        Time.timeScale = 0; // 시작 전 정지 상태
    }

    public void GameOver() // 게임 오버 시 호출되는 함수
    {

    }

    public void RestartGame() // 게임을 재시작하는 함수
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 게임을 재시작하는 함수
    }

    public void UpdateScore(int score) // 점수를 추가하는 함수
    {
        currentScore += score; // 주어진 score를 currentScore에 더함

        if (currentScore >= bestScore)
        {
            bestScore = currentScore; // 최고 점수 갱신

            // PlayerPrefs에 저장 → 앱을 껐다 켜도 유지됨
            PlayerPrefs.SetInt(BestScoreKey, currentScore);
        }
    }
}
