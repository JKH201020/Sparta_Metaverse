using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̱��� static
// ���׸��ϰ� ���� ���, �ɼ��� �ִ� ��� DontDestroyObject

public class MainGameManager : MonoBehaviour
{
    // ��� �� ���� (�̱��� - ��𼭵� �ҷ��ͼ� ����� �� �ִ�.)
    public static MainGameManager Instance;
    [SerializeField] private LoadSceneManager _sceneManager;
    [SerializeField] private MainUIManager _uiManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this; // �ڱ� �ڽ��� �Ҵ��ؼ� null�� ���� �ʵ��� ��
        }
    }

    #region MainUIManager

    public void UpdateNoticeText(string text)
    {
        _uiManager.UpdateNoticeText(text); // MainUIManager.cs���� UpdateNoticeText�� �ҷ���
    }

    #endregion

    #region LoadSceneManager

    public void LoadScene(string sceneName) // ���� �ҷ���
    {
        _sceneManager.LoadScene(sceneName); //LoadSceneManager���� LoadScene�� �ҷ���
    }

    #endregion
}
