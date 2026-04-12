using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [Header("로비 버튼 UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _soundButton;

    private const string StartButtonString = "StartButton";
    private const string LoadButtonString = "LoadButton";

    private void Reset()
    {
        _startButton = GameObject.Find(StartButtonString).GetComponent<Button>();
        _loadButton = GameObject.Find(LoadButtonString).GetComponent<Button>();
    }

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _loadButton.onClick.AddListener(OnLoadButtonClicked);
    }

    private async void OnStartButtonClicked()
    {
        int targetSlot = 1; // 기본값은 1인 슬롯

        // 모든 슬롯에 데이터 있는지 검사
        bool hasAnyData = SaveManager.Instance.HasData(1) ||
                          SaveManager.Instance.HasData(2) ||
                          SaveManager.Instance.HasData(3);

        if (hasAnyData)
        {
            // 데이터가 있다면, 마지막 플레이 슬롯을 가져옴
            targetSlot = SaveManager.Instance.GetLastPlayedSlot();

            // 예외처리
            // 만약 시스템은 2번이라고 기억하는데, 유저가 방금 2번 슬롯을 삭제했다면?
            // 데이터가 살아있는 다른 슬롯을 찾아서 거기로 연결
            if (SaveManager.Instance.HasData(targetSlot) == false)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (SaveManager.Instance.HasData(i))
                    {
                        targetSlot = i;
                        break;
                    }
                }
                Debug.Log($"[Lobby] 기존 유저: 마지막으로 플레이한 {targetSlot}번 슬롯을 불러옵니다.");
            }
            else
            {
                // 데이터가 하나도 없다면 신규 유저! 무조건 1번 슬롯
                targetSlot = 1;
                Debug.Log($"[Lobby] 신규 유저: 1번 슬롯으로 새 게임을 시작합니다.");
            }
        }

        // 데이터를 로드해 보고, 진짜 없으면 빈 데이터를 새로 만들어서 저장
        SaveData data = SaveManager.Instance.Load(targetSlot);
        if (data == null)
        {
            data = new SaveData();
            SaveManager.Instance.Save(targetSlot, data);
        }

        // 게임 시작 직전에 "나 지금 이 슬롯으로 게임한다"고 시스템에 각인
        SaveManager.Instance.SetCurrentSlot(targetSlot);

        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }

    private void OnLoadButtonClicked()
    {
        UIManager.Instance.OnLoadSlotUI();
    }

    private void OnSoundButtonClicked()
    {

    }
}
