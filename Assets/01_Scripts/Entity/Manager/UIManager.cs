using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("로딩 UI")]
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _fadePanel;
    [SerializeField] private Image _fadePanelImage;

    [Header("대화 UI")]
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private DialogueUI _dialogueUIScript;

    [Header("불러오기 UI"), SerializeField] private GameObject _loadSlotUI;

    [Header("탑다운 미니게임 UI")]
    [SerializeField] private GameObject _topDownUI;
    [SerializeField] private GameObject _gameOverUI;

    [Header("플래피 플레인 UI")]
    [SerializeField] private GameObject _homeUI;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _endUI;

    private const string LoadingPanelPathString = "Canvas/LoadingPanel";
    private const string FadePanelPathString = "Canvas/FadePanel";
    private const string DialogueUIString = "Canvas/DialogueUI";
    private const string LoadSlotUIString = "Canvas/LoadSlotUI";
    private const string TopDownGameUIString = "Canvas/TopDownGameUI";
    private const string GameOverUIString = "Canvas/TopDownGameUI/GameOverUI";
    private const string HomeUIString = "Canvas/FlappyPlaneUI/HomeUI";
    private const string GameUIString = "Canvas/FlappyPlaneUI/GameUI";
    private const string EndUIString = "Canvas/FlappyPlaneUI/EndUI";

    private void Reset()
    {
        _loadingPanel = transform.Find(LoadingPanelPathString).gameObject;
        _fadePanel = transform.Find(FadePanelPathString).gameObject;
        _fadePanelImage = _fadePanel.GetComponent<Image>();
        _dialogueUI = transform.Find(DialogueUIString).gameObject;
        _dialogueUIScript = _dialogueUI.GetComponent<DialogueUI>();
        _loadSlotUI = transform.Find(LoadSlotUIString).gameObject;
        _topDownUI = transform.Find(TopDownGameUIString).gameObject;
        _gameOverUI = transform.Find(GameOverUIString).gameObject;
        _homeUI = transform.Find(HomeUIString).gameObject;
        _gameUI = transform.Find(GameUIString).gameObject;
        _endUI = transform.Find(EndUIString).gameObject;
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

    #region 대화UI 온오프

    public void ShowDialogueUI(string npcName, Action onEnd = null)
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.ChangeState(GameState.Talking);
        _dialogueUI.SetActive(true);
        _dialogueUIScript.StartDialogue(npcName, onEnd);
    }

    public void CloseDialogueUI()
    {
        if (GameManager.Instance == null) return;

        _dialogueUI.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
    }

    #endregion

    #region 로드UI 온오프

    public void OnLoadSlotUI()
    {
        _loadSlotUI.SetActive(true);
    }

    public void OffLoadSlotUI()
    {
        _loadSlotUI.SetActive(false);
    }

    #endregion

    #region 탑다운 미니게임UI 온오프

    /// <summary>
    /// 탑다운게임UI 활성화
    /// </summary>
    public void OnTopDownGameUI()
    {
        _topDownUI.SetActive(true);
    }

    /// <summary>
    /// 탑다운게임UI 비활성화
    /// </summary>
    public void OffTopDownGameUI()
    {
        _topDownUI.SetActive(false);
    }

    /// <summary>
    /// 게임오버UI 활성화
    /// </summary>
    public void OnGameOverUI()
    {
        _gameOverUI.SetActive(true);
    }

    /// <summary>
    /// 게임오버UI 비활성화
    /// </summary>
    public void OffGameOverUI()
    {
        _gameOverUI.SetActive(false);
    }

    #endregion

    #region 플래피 버드UI 온오프

    /// <summary>
    /// 시작 UI 활성화
    /// </summary>
    public void OnHomeUI()
    {
        _homeUI.SetActive(true);
    }

    /// <summary>
    /// 시작 UI 비활성화
    /// </summary>
    public void OffHomeUI()
    {
        _homeUI.SetActive(false);
    }

    /// <summary>
    /// 인게임 UI 활성화
    /// </summary>
    public void OnGameUI()
    {
        _gameUI.SetActive(true);
    }

    /// <summary>
    /// 인게임 UI 비활성화
    /// </summary>
    public void OffGameUI()
    {
        _gameUI.SetActive(false);
    }

    /// <summary>
    /// 게임 오버 UI 활성화
    /// </summary>
    public void OnEndUI()
    {
        _endUI.SetActive(true);
    }

    /// <summary>
    /// 게임 오버 UI 비활성화
    /// </summary>
    public void OffEndUI()
    {
        _endUI.SetActive(false);
    }

    #endregion

}