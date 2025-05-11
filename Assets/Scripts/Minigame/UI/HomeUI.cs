using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button startButton;
    Button exitButton;

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(MiniGameUIManager uiManager)
    {
        base.Init(uiManager);

        // ���� ������Ʈ���� ��ư���� ã�Ƽ� ����
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        // ��ư Ŭ�� �� �̺�Ʈ ����
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    void OnClickStartButton() // Start ��ư Ŭ�� �� ���� ���� ��û
    {
        uiManager.OnClickStart();
    }

    void OnClickExitButton() // Exit ��ư Ŭ�� �� ���� ���� ��û
    {
        uiManager.OnClickExit();
    }
}