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
        _startButton_1.onClick.AddListener(OnStartButton1Clicked);
        _deleteButton_1.onClick.AddListener(OnDeleteButton1Clicked);
        _startButton_2.onClick.AddListener(OnStartButton2Clicked);
        _deleteButton_2.onClick.AddListener(OnDeleteButton2Clicked);
        _startButton_3.onClick.AddListener(OnStartButton3Clicked);
        _deleteButton_3.onClick.AddListener(OnDeleteButton3Clicked);
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private async void OnStartButton1Clicked()
    {
        UIManager.Instance.OffLoadSlotUI();
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }

    private void OnDeleteButton1Clicked()
    {

    }

    private async void OnStartButton2Clicked()
    {
        UIManager.Instance.OffLoadSlotUI();
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }

    private void OnDeleteButton2Clicked()
    {

    }

    private async void OnStartButton3Clicked()
    {
        UIManager.Instance.OffLoadSlotUI();
        await GameManager.Instance.ChangeScene(SceneNames.MainScene);
    }

    private void OnDeleteButton3Clicked()
    {

    }

    private void OnCloseButtonClicked()
    {
        UIManager.Instance.OffLoadSlotUI();
    }
}
