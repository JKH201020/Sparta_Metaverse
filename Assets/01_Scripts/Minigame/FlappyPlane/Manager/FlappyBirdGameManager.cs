using System;
using System.Threading.Tasks;
using UnityEngine;

public class FlappyBirdGameManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티 (외부에서 GameManager.Instance로 호출 가능)
    public static FlappyBirdGameManager Instance { get; private set; }

    [Header("플레이어")]
    [SerializeField] private GameObject _playerPos;
    [SerializeField] private Plane _player;

    [Header("메인 카메라"), SerializeField] private MiniFollowCamera _followCamera;
    [Header("환경 관리"), SerializeField] private BgLooper _bgLooper;

    private int _currentScore = 0; // 현재 점수를 저장하는 변수
    public int CurrentScore { get => _currentScore; }

    private int _bestScore = 0; // 최고 점수 저장 변수
    public int BestScore { get => _bestScore; }

    public const string BestScoreKey = "BestScore"; // PlayerPrefs 저장 키

    private void Reset()
    {
        _playerPos = GameObject.FindWithTag(Tag.Player);
        _player = _playerPos.GetComponent<Plane>();
        _followCamera = GameObject.FindWithTag(Tag.MainCamera).GetComponent<MiniFollowCamera>();
        _bgLooper = FindObjectOfType<BgLooper>();
    }

    private void Awake()
    {
        if (Instance == null) Instance = this; // GameManager 인스턴스를 gameManager에 할당 (싱글톤 초기화)
    }

    private async void Start()
    {
        UIManager.Instance.OnEnableFlappyBirdUI();
        _followCamera.SetTarget(_playerPos.transform);
        await LoadData(); // 데이터 다 읽을 때까지 기다림
        _player.ResetPlane();
    }

    private void OnEnable()
    {
        _player.gameOverEvent += GameOver;
    }

    private void OnDisable()
    {
        _player.gameOverEvent -= GameOver;
    }

    /// <summary>
    /// 게임 오버 시 호출되는 메서드
    /// </summary>
    public async void GameOver()
    {
        GameManager.Instance.ChangeState(GameState.GameOver);

        int mySlot = SaveManager.Instance.CurrentSlot;
        if (mySlot != -1) // 만약 슬롯이 정상적으로 세팅되어 있다면 (-1이 아니라면)
        {
            // 해당 슬롯의 데이터를 불러와서, 최신 점수로 덮어씌우고 다시 저장
            SaveData data = await SaveManager.Instance.Load(mySlot);
            // 최고 점수 갱신 로직 (Mathf.Max를 쓰면 둘 중 큰 값을 알아서 넣어줌)
            data.planeScore = Mathf.Max(data.planeScore, CurrentScore);
            await SaveManager.Instance.Save(mySlot, data); // 파일로 최종 저장
        }

        UIManager.Instance.OnEndUI();
    }

    /// <summary>
    /// 게임 초기화 메서드
    /// </summary>
    public async void PrepareNewGame()
    {
        await LoadData();

        _player.isDead = false; // 생존 중

        // 플레이어, 장애물 위치 초기화
        _playerPos.transform.position = Vector2.zero;
        _player.ResetPlane();
        _bgLooper.ResetObstacles();

        GameManager.Instance.ChangeState(GameState.Playing);
        // 점수 초기화
        _currentScore = 0;
    }

    private async Task LoadData() // 저장 데이터 로드
    {
        int mySlot = SaveManager.Instance.CurrentSlot;
        if (mySlot != -1)
        {
            SaveData data = await SaveManager.Instance.Load(mySlot);
            if (data != null) _bestScore = data.planeScore;
        }
    }

    /// <summary>
    /// 점수를 추가하는 메서드
    /// </summary>
    /// <param name="score">현재 점수</param>
    public void UpdateScore(int score)
    {
        _currentScore += score; // 주어진 score를 currentScore에 더함

        // 최고 점수 갱신
        if (_currentScore >= _bestScore) _bestScore = _currentScore;
    }
}
