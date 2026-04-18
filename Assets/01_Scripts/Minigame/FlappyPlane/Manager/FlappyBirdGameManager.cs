using UnityEngine;

public class FlappyBirdGameManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티 (외부에서 GameManager.Instance로 호출 가능)
    public static FlappyBirdGameManager Instance { get; private set; }

    [Header("플레이어")]
    [SerializeField] private GameObject _playerPos;
    [SerializeField] private Plane _player;

    [Header("메인 카메라"), SerializeField] private MiniFollowCamera _followCamera;

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
    }

    private void Awake()
    {
        if (Instance == null) Instance = this; // GameManager 인스턴스를 gameManager에 할당 (싱글톤 초기화)
    }

    private void Start()
    {
        UIManager.Instance.OnEnableFlappyBirdUI();
        _player.isDead = false; // 생존 중
        _currentScore = 0; // 게임 시작 시 점수를 0으로 초기화하여 UI에 표시
        _bestScore = PlayerPrefs.GetInt(BestScoreKey, 0); // 저장된 최고 점수 불러오기 (없으면 기본값 0)
    }

    /// <summary>
    /// 게임 오버 시 호출되는 함수
    /// </summary>
    public void GameOver()
    {
        GameManager.Instance.ChangeState(GameState.GameOver);

        int mySlot = SaveManager.Instance.CurrentSlot;
        if (mySlot != -1) // 만약 슬롯이 정상적으로 세팅되어 있다면 (-1이 아니라면)
        {
            // 해당 슬롯의 데이터를 불러와서, 최신 점수로 덮어씌우고 다시 저장
            SaveData data = SaveManager.Instance.Load(mySlot);
            // 최고 점수 갱신 로직 (Mathf.Max를 쓰면 둘 중 큰 값을 알아서 넣어줌)
            data.planeScore = Mathf.Max(data.planeScore, CurrentScore);
            SaveManager.Instance.Save(mySlot, data); // 파일로 최종 저장
        }
    }

    /// <summary>
    /// 게임을 재시작하는 함수
    /// </summary>
    public void RestartGame()
    {
        int mySlot = SaveManager.Instance.CurrentSlot;
        SaveData data = SaveManager.Instance.Load(mySlot);

        // 점수 초기화
        _currentScore = 0;
        _bestScore = data.planeScore;
        //OnScoreChanged?.Invoke(CurrentScore, _bestScore);
    }

    /// <summary>
    /// 점수를 추가하는 함수
    /// </summary>
    /// <param name="score">현재 점수</param>
    public void UpdateScore(int score) 
    {
        _currentScore += score; // 주어진 score를 currentScore에 더함

        // 최고 점수 갱신
        if (_currentScore >= _bestScore) _bestScore = _currentScore;
    }
}
