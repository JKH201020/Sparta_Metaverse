using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

// 게임의 UI 상태를 정의하는 열거형
public enum UIState
{
    Home,
    Game,
    End
}

public class UIManager : MonoBehaviour
{
    public string sceneName; // 이동할 씬의 이름 (Inspector 창에서 설정)

    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    UIState currentState = UIState.Home;

    HomeUI homeUI = null;
    GameUI gameUI = null;
    EndUI endUI = null;

    private void Awake()
    {
        instance = this;

        // 자식 오브젝트에서 각각의 UI를 찾아 초기화
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);
        endUI = GetComponentInChildren<EndUI>(true);
        endUI?.Init(this);

        // 초기 상태를 홈 화면으로 설정
        ChangeState(UIState.Home);
    }

    public void ChangeState(UIState state) // UI 전환
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        endUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.Game); // UI를 게임 화면으로 전환
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene(sceneName); // 메인 씬으로 복귀
    }

    public void GameOver()
    {
        ChangeState(UIState.End); // UI를 게임 오버화면으로 전환
    }
}
