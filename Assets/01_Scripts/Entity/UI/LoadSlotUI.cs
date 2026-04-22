using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSlotUI : MonoBehaviour
{
    [Header("슬롯 1")]
    [SerializeField] private Button _startButton_1;
    [SerializeField] private Button _deleteButton_1;
    [SerializeField] private GameObject _emptySlot_1;
    [SerializeField] private GameObject _score_1;
    [SerializeField] private TextMeshProUGUI _tdScore_1;
    [SerializeField] private TextMeshProUGUI _planeScore_1;

    [Header("슬롯 2")]
    [SerializeField] private Button _startButton_2;
    [SerializeField] private Button _deleteButton_2;
    [SerializeField] private GameObject _emptySlot_2;
    [SerializeField] private GameObject _score_2;
    [SerializeField] private TextMeshProUGUI _tdScore_2;
    [SerializeField] private TextMeshProUGUI _planeScore_2;

    [Header("슬롯 3")]
    [SerializeField] private Button _startButton_3;
    [SerializeField] private Button _deleteButton_3;
    [SerializeField] private GameObject _emptySlot_3;
    [SerializeField] private GameObject _score_3;
    [SerializeField] private TextMeshProUGUI _tdScore_3;
    [SerializeField] private TextMeshProUGUI _planeScore_3;

    [Header("뒤로가기")]
    [SerializeField] private Button _closeButton;

    private const string StartButton_1PathString = "Slot1/StartButton_1";
    private const string DeleteButton_1PathString = "Slot1/DeleteButton_1";
    private const string StartButton_2PathString = "Slot2/StartButton_2";
    private const string DeleteButton_2PathString = "Slot2/DeleteButton_2";
    private const string StartButton_3PathString = "Slot3/StartButton_3";
    private const string DeleteButton_3PathString = "Slot3/DeleteButton_3";
    private const string CloseButtonPathString = "CloseButton";
    private const string EmptySlot_1PathString = "Slot1/Text - EmptySlot_1";
    private const string Score_1PathString = "Slot1/Text - Score_1";
    private const string EmptySlot_2PathString = "Slot2/Text - EmptySlot_2";
    private const string Score_2PathString = "Slot2/Text - Score_2";
    private const string EmptySlot_3PathString = "Slot3/Text - EmptySlot_3";
    private const string Score_3PathString = "Slot3/Text - Score_3";
    private const string TDScore_1PathString = "Slot1/Text - Score_1/Text - TDScore";
    private const string PlaneScore_1PathString = "Slot1/Text - Score_1/Text - PlaneScore";
    private const string TDScore_2PathString = "Slot2/Text - Score_2/Text - TDScore";
    private const string PlaneScore_2PathString = "Slot2/Text - Score_2/Text - PlaneScore";
    private const string TDScore_3PathString = "Slot3/Text - Score_3/Text - TDScore";
    private const string PlaneScore_3PathString = "Slot3/Text - Score_3/Text - PlaneScore";

    private void Reset()
    {
        _startButton_1 = transform.Find(StartButton_1PathString).GetComponent<Button>();
        _deleteButton_1 = transform.Find(DeleteButton_1PathString).GetComponent<Button>();
        _startButton_2 = transform.Find(StartButton_2PathString).GetComponent<Button>();
        _deleteButton_2 = transform.Find(DeleteButton_2PathString).GetComponent<Button>();
        _startButton_3 = transform.Find(StartButton_3PathString).GetComponent<Button>();
        _deleteButton_3 = transform.Find(DeleteButton_3PathString).GetComponent<Button>();
        _closeButton = transform.Find(CloseButtonPathString).GetComponent<Button>();
        _emptySlot_1 = transform.Find(EmptySlot_1PathString).gameObject;
        _score_1 = transform.Find(Score_1PathString).gameObject;
        _emptySlot_2 = transform.Find(EmptySlot_2PathString).gameObject;
        _score_2 = transform.Find(Score_2PathString).gameObject;
        _emptySlot_3 = transform.Find(EmptySlot_3PathString).gameObject;
        _score_3 = transform.Find(Score_3PathString).gameObject;
        _tdScore_1 = transform.Find(TDScore_1PathString).GetComponent<TextMeshProUGUI>();
        _planeScore_1 = transform.Find(PlaneScore_1PathString).GetComponent<TextMeshProUGUI>();
        _tdScore_2 = transform.Find(TDScore_2PathString).GetComponent<TextMeshProUGUI>();
        _planeScore_2 = transform.Find(PlaneScore_2PathString).GetComponent<TextMeshProUGUI>();
        _tdScore_3 = transform.Find(TDScore_3PathString).GetComponent<TextMeshProUGUI>();
        _planeScore_3 = transform.Find(PlaneScore_3PathString).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _startButton_1.onClick.AddListener(() => OnStartButtonClicked(1));
        _deleteButton_1.onClick.AddListener(() => OnDeleteButtonClicked(1, _emptySlot_1, _score_1));
        _startButton_2.onClick.AddListener(() => OnStartButtonClicked(2));
        _deleteButton_2.onClick.AddListener(() => OnDeleteButtonClicked(2, _emptySlot_2, _score_2));
        _startButton_3.onClick.AddListener(() => OnStartButtonClicked(3));
        _deleteButton_3.onClick.AddListener(() => OnDeleteButtonClicked(3, _emptySlot_3, _score_3));
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }
    private void OnEnable()
    {
        RefreshAllSlots();
    }

    // 모든 슬롯의 UI를 한 번에 갱신
    private void RefreshAllSlots()
    {
        UpdateSingleSlotUI(1, _emptySlot_1, _score_1, _tdScore_1, _planeScore_1);
        UpdateSingleSlotUI(2, _emptySlot_2, _score_2, _tdScore_2, _planeScore_2);
        UpdateSingleSlotUI(3, _emptySlot_3, _score_3, _tdScore_3, _planeScore_3);
    }

    // 데이터 유뮤 확인 + 점수 연결
    private async void UpdateSingleSlotUI(int slot, GameObject emptyObj, GameObject scoreObj, TextMeshProUGUI tdScore, TextMeshProUGUI planeScore)
    {
        if (SaveManager.Instance.HasData(slot))
        {
            emptyObj.SetActive(false);
            scoreObj.SetActive(true);

            SaveData data = await SaveManager.Instance.Load(slot);

            tdScore.text = data.tdScore.ToString();
            planeScore.text = data.planeScore.ToString();
        }
        else
        {
            emptyObj.SetActive(true);
            scoreObj.SetActive(false);
        }
    }

    #region 버튼 이벤트

    private async void OnStartButtonClicked(int slot) // 해당 슬롯 시작 버튼 이벤트
    {
        SaveData data;
        if (SaveManager.Instance.HasData(slot))
        {
            data = await SaveManager.Instance.Load(slot);
        }
        else
        {
            data = new SaveData();
            await SaveManager.Instance.Save(slot, data);
        }

        // 씬을 넘어가기 전에, SaveManager에게 현재 슬롯 번호를 각인
        SaveManager.Instance.SetCurrentSlot(slot);

        UIManager.Instance.OffLoadSlotUI();
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }

    private void OnDeleteButtonClicked(int slot, GameObject emptyObj, GameObject scoreObj) // 해당 슬롯 삭제 버튼 이벤트
    {
        SaveManager.Instance.Delete(slot);

        emptyObj.SetActive(true);
        scoreObj.SetActive(false);

        RefreshAllSlots();
    }

    private void OnCloseButtonClicked() // 닫기 버튼 이벤트
    {
        UIManager.Instance.OffLoadSlotUI();
    }

    #endregion

}
