using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 UI의 기본 동작을 정의하는 추상 클래스
public abstract class BaseUI : MonoBehaviour
{
    protected MiniGameUIManager uiManager;

    // 현재 UI 상태(UIState) 정의 (자식 클래스에서 구현해야 함)
    protected abstract UIState GetUIState();

    public virtual void Init(MiniGameUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    // 전달된 상태와 현재 UI의 상태가 일치하면 활성화, 아니면 비활성화
    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
