using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("로비 버튼 UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _SoundButton;

    [Header("Notice")]
    public TextMeshProUGUI NoticeText;
    private Coroutine _noticeTimerCoroutine;

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        GameManager.Instance.LoadScene(SceneNames.MainScene);
    }

    private void OnLoadButtonClicked()
    {

    }

    private void OnSoundButtonClicked()
    {

    }

    #region NoticeText

    public void UpdateNoticeText(string text) // 텍스트 업데이트
    {
        NoticeText.text = text; // 화면 상단에 뜨는 빨간 텍스트
        StartNoticeTimer(NoticeText.gameObject); // 텍스트 타이머 작동
    }

    private void StartNoticeTimer(GameObject activeObject) // 텍스트 타이머 작동
    {
        // 1.5초 안에 상호작용 범위를 나갔을 경우 - 아직 코루틴이 돌고 있음
        if (_noticeTimerCoroutine != null)
        {
            StopCoroutine(_noticeTimerCoroutine); // 코루틴 정지
            _noticeTimerCoroutine = null; // 타이머 초기화
        }

        // 코루틴 작동
        _noticeTimerCoroutine = StartCoroutine(routine: CountingCoroutine(activeObject));
    }

    // 일정 시간 뒤에 UpdateNoticeText를 지움
    IEnumerator CountingCoroutine(GameObject activeObject)
    {
        activeObject.gameObject.SetActive(true); // NoticeText.text를 활성화
        yield return new WaitForSeconds(1.5f); // 1.5초 대기
        activeObject.gameObject.SetActive(false); // NoticeText.text를 비활성화
    }

    #endregion
}