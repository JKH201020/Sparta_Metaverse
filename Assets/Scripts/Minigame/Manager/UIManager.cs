using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

// 게임의 UI 상태를 정의하는 열거형
public enum UIState
{
    Home,
    Game
}

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    UIState currentState = UIState.Home;

    HomeUI homeUI = null;
    GameUI gameUI = null;
    GameManager gameManager = null;

    private void Awake()
    {
        instance = this;
        gameManager = FindObjectOfType<GameManager>();

        // 자식 오브젝트에서 각각의 UI를 찾아 초기화
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);

        // 초기 상태를 홈 화면으로 설정
        ChangeState(UIState.Home);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.Game); // UI를 게임 화면으로 전환
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    //public void Start()
    //{
    //    restartText.gameObject.SetActive(false); // 처음에는 "다시 시작" 텍스트를 숨겨둠
    //}

    //public void SetRestart() // 게임 오버 상태에서 다시 시작 메시지를 보이게 함
    //{
    //    restartText.gameObject.SetActive(true);
    //}
}
