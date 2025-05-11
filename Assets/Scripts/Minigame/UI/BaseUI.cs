using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� UI�� �⺻ ������ �����ϴ� �߻� Ŭ����
public abstract class BaseUI : MonoBehaviour
{
    protected MiniGameUIManager uiManager;

    // ���� UI ����(UIState) ���� (�ڽ� Ŭ�������� �����ؾ� ��)
    protected abstract UIState GetUIState();

    public virtual void Init(MiniGameUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    // ���޵� ���¿� ���� UI�� ���°� ��ġ�ϸ� Ȱ��ȭ, �ƴϸ� ��Ȱ��ȭ
    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
