using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    UIManager uiManager; // UI를 제어할 UIManager 참조 변수
    static GameManager gameManager; // 싱글톤 패턴을 위한 변수. 게임 내에서 유일한 인스턴스를 사용

    // 싱글톤 접근용 프로퍼티 (외부에서 GameManager.Instance로 호출 가능)
    public static GameManager Instance { get { return gameManager; } }

    int currentScore = 0; // 현재 점수를 저장하는 변수

    public UIManager UIManager { get { return uiManager; } } // 외부에서 접근할 수 있도록 프로퍼티 제공

    void Awake()
    {
        gameManager = this; // GameManager 인스턴스를 gameManager에 할당 (싱글톤 초기화)
        uiManager = FindObjectOfType<UIManager>(); // Scene에서 UIManager를 찾아 참조함
    }

    public void Start()
    {
        uiManager.UpdateScore(0); // 게임 시작 시 점수를 0으로 초기화하여 UI에 표시
    }

    public void GameOver() // 게임 오버 시 호출되는 함수
    {
        uiManager.SetRestart(); // UI에 "다시 시작" 텍스트를 활성화
    }

    public void RestartGame() // 게임을 재시작하는 함수
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 게임을 재시작하는 함수
    }

    public void AddScore(int score) // 점수를 추가하는 함수
    {
        currentScore += score; // 주어진 score를 currentScore에 더함
        uiManager.UpdateScore(currentScore); // UI 업데이트
    }
}
