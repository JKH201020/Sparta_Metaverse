using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header ("로딩 UI")]
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _fadePanel;
    [SerializeField] private Image _fadePanelImage;

    private const String LoadingPanelPathString = "Canvas/LoadingPanel";
    private const String FadePanelPathString = "Canvas/FadePanel";

    private void Reset()
    {
        _loadingPanel = transform.Find(LoadingPanelPathString).gameObject;
        _fadePanel = transform.Find(FadePanelPathString).gameObject;
        _fadePanelImage = _fadePanel.GetComponent<Image>();
    }

    protected override void Awake()
    {

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

    #region 페이드 온오프

    /// <summary>
    /// 화면에 검은색 페이드 효과 활성화
    /// </summary>
    public async Task FadeIn(float time)
    {
        _fadePanel.SetActive(true);
        // 페이드를 시작하고 그 작업이 끝날 때까지 '비동기적으로' 기다린다는(await) 뜻
        await _fadePanelImage.DOFade(1f, time).AsyncWaitForCompletion();
    }

    /// <summary>
    /// 화면에 검은색 페이드 효과 비활성화
    /// </summary>
    public async Task FadeOut(float time)
    {
        await _fadePanelImage.DOFade(0f, time).AsyncWaitForCompletion();
        _fadePanel.SetActive(false);
    }

    #endregion

}