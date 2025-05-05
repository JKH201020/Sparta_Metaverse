using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

// ������ UI ���¸� �����ϴ� ������
public enum UIState
{
    Home,
    Game,
    End
}

public class UIManager : MonoBehaviour
{
    public string sceneName; // �̵��� ���� �̸� (Inspector â���� ����)

    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    UIState currentState = UIState.Home;

    HomeUI homeUI = null;
    GameUI gameUI = null;
    EndUI endUI = null;

    private void Awake()
    {
        instance = this;

        // �ڽ� ������Ʈ���� ������ UI�� ã�� �ʱ�ȭ
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);
        endUI = GetComponentInChildren<EndUI>(true);
        endUI?.Init(this);

        // �ʱ� ���¸� Ȩ ȭ������ ����
        ChangeState(UIState.Home);
    }

    public void ChangeState(UIState state) // UI ��ȯ
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        endUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.Game); // UI�� ���� ȭ������ ��ȯ
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene(sceneName); // ���� ������ ����
    }

    public void GameOver()
    {
        ChangeState(UIState.End); // UI�� ���� ����ȭ������ ��ȯ
    }
}
