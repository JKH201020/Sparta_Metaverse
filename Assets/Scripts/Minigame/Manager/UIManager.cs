using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

// ������ UI ���¸� �����ϴ� ������
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

        // �ڽ� ������Ʈ���� ������ UI�� ã�� �ʱ�ȭ
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);

        // �ʱ� ���¸� Ȩ ȭ������ ����
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
        ChangeState(UIState.Game); // UI�� ���� ȭ������ ��ȯ
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    //public void Start()
    //{
    //    restartText.gameObject.SetActive(false); // ó������ "�ٽ� ����" �ؽ�Ʈ�� ���ܵ�
    //}

    //public void SetRestart() // ���� ���� ���¿��� �ٽ� ���� �޽����� ���̰� ��
    //{
    //    restartText.gameObject.SetActive(true);
    //}
}
