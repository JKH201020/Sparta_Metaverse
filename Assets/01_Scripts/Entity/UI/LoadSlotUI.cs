using UnityEngine;
using UnityEngine.UI;

public class LoadSlotUI : MonoBehaviour
{
    [Header("슬롯 1")]
    [SerializeField] private Button _startButton_1;
    [SerializeField] private Button _deleteButton_1;

    [Header("슬롯 2")]
    [SerializeField] private Button _startButton_2;
    [SerializeField] private Button _deleteButton_2;

    [Header("슬롯 3")]
    [SerializeField] private Button _startButton_3;
    [SerializeField] private Button _deleteButton_3;

    [Header("뒤로가기")]
    [SerializeField] private Button _closeButton;

    private const string StartButton_1String = "Slot1/StartButton_1";
    private const string DeleteButton_1String = "Slot1/DeleteButton_1";
    private const string StartButton_2String = "Slot2/StartButton_2";
    private const string DeleteButton_2String = "Slot2/DeleteButton_2";
    private const string StartButton_3String = "Slot3/StartButton_3";
    private const string DeleteButton_3String = "Slot3/DeleteButton_3";
    private const string CloseButtonString = "CloseButton";

    private void Reset()
    {
        _startButton_1 = transform.Find(StartButton_1String).GetComponent<Button>();
        _deleteButton_1 = transform.Find(DeleteButton_1String).GetComponent<Button>();
        _startButton_2 = transform.Find(StartButton_2String).GetComponent<Button>();
        _deleteButton_2 = transform.Find(DeleteButton_2String).GetComponent<Button>();
        _startButton_3 = transform.Find(StartButton_3String).GetComponent<Button>();
        _deleteButton_3 = transform.Find(DeleteButton_3String).GetComponent<Button>();
        _closeButton = transform.Find(CloseButtonString).GetComponent<Button>();
    }

    private void Start()
    {
        _startButton_1.onClick.AddListener(() => OnStartButtonClicked(1));
        _deleteButton_1.onClick.AddListener(() => OnDeleteButtonClicked(1));
        _startButton_2.onClick.AddListener(() => OnStartButtonClicked(2));
        _deleteButton_2.onClick.AddListener(() => OnDeleteButtonClicked(2));
        _startButton_3.onClick.AddListener(() => OnStartButtonClicked(3));
        _deleteButton_3.onClick.AddListener(() => OnDeleteButtonClicked(3));
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private async void OnStartButtonClicked(int slot) // 해당 슬롯 시작 버튼 이벤트
    {
        SaveData data;
        if (SaveManager.Instance.HasData(slot))
        {
            data = SaveManager.Instance.Load(slot);
        }
        else
        {
            data = new SaveData();
            SaveManager.Instance.Save(slot, data);
            Debug.Log($"{slot}번 슬롯 신규 생성");
        }

        // 씬을 넘어가기 전에, SaveManager에게 현재 슬롯 번호를 각인
        SaveManager.Instance.SetCurrentSlot(slot);

        UIManager.Instance.OffLoadSlotUI();
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }

    private void OnDeleteButtonClicked(int slot) // 해당 슬롯 삭제 버튼 이벤트
    {
        SaveManager.Instance.Delete(slot);
    }

    private void OnCloseButtonClicked() // 닫기 버튼 이벤트
    {
        UIManager.Instance.OffLoadSlotUI();
    }
}
