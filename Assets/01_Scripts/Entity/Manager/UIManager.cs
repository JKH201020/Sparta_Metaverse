using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Image _fadePanel;

    private const String LoadingPanelString = "Canvas/LoadingPanel";
    private const String FadePanelString = "Canvas/FadePanel";

    private void Reset()
    {
        _loadingPanel = transform.Find(LoadingPanelString).gameObject;
        _fadePanel = transform.Find(FadePanelString).GetComponent<Image>();
    }

    #region 패널 온오프

    /// <summary>
    /// 로딩 패널 활성화
    /// </summary>
    public void OnLoadingPanel() 
    {
        _loadingPanel.SetActive(true);
    }

    /// <summary>
    /// 로딩 패널 비활성화
    /// </summary>
    public void OffLoadingPanel() 
    {
        _loadingPanel.SetActive(false);
    }

    #endregion

    protected override void Awake()
    {

    }

    private async Task OnLoading() // 로딩화면
    {
        
    }
}