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

        // 하위 오브젝트에서 버튼들을 찾아서 연결
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        // 버튼 클릭 시 이벤트 연결
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    void OnClickStartButton() // Start 버튼 클릭 시 게임 시작 요청
    {
        uiManager.OnClickStart();
    }

    void OnClickExitButton() // Exit 버튼 클릭 시 게임 종료 요청
    {
        uiManager.OnClickExit();
    }
}