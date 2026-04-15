using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingPlayerController : MonoBehaviour
{
    [Header("플레이어 애니메이션 관련")]
    [SerializeField] private SpriteRenderer _characterRenderer; // SpriteRenderer 컴포넌트를 참조하기 위한 변수
    [SerializeField] private Animator _bowAnim; // 활 애니메이션
    [SerializeField] private Transform _firePoint; // 화살 발사 위치
    [SerializeField] private GameObject _bow;

    [Header("스탯"), SerializeField] private HealthSystem _healthSystem;

    private Camera _mainCam;

    private float _defalutBulletSpeed = 1.0f;
    private float _bowDis = 0.6f; // 활 위치

    // 상태 클래스에서 SpriteRenderer에 접근하기 위한 프로퍼티
    public SpriteRenderer CharacterRenderer => _characterRenderer;
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerStats stats;

    // FSM 상태 객체들이 읽어갈 입력값
    public Vector2 MoveInput { get; private set; } // InputSystem 움직임 입력
    public Vector2 MouseScreenPos { get; private set; } // 마우스 화면 좌표

    // 상태 관리
    private PlayerBaseState _currentState;
    public ShootingPlayingState PlayingState { get; private set; }
    public ShootingDeadState DeadState { get; private set; }

    private const string MainSpriteString = "MainSprite";
    private const string BowSpriteString = "BowSprite";

    private void Reset()
    {
        _characterRenderer = transform.Find(MainSpriteString).GetComponent<SpriteRenderer>();
        _bow = transform.Find(BowSpriteString).gameObject;
        _bowAnim = _bow.GetComponent<Animator>();
        _firePoint = _bow.transform;
        _healthSystem = transform.GetComponent<HealthSystem>();
    }

    private void Awake()
    {
        _mainCam = Camera.main;

        Rigidbody = GetComponent<Rigidbody2D>();
        // 상태 객체 생성
        PlayingState = new ShootingPlayingState(this);
        DeadState = new ShootingDeadState(this);
    }

    private void Start()
    {
        // 스크립트가 켜져 있을 때만 초기 상태 진입
        if (enabled) ChangeState(PlayingState);
    }

    private void Update()
    {
        if (!enabled) return;

        // 공격 딜레이에 따라 애니메이션 속도 변함
        _bowAnim.speed = _defalutBulletSpeed / stats.attackDelay;

        RotateBow();
        _currentState?.Tick();
    }

    private void FixedUpdate()
    {
        if (!enabled) return;

        _currentState?.PhysicsTick();
    }

    private void OnEnable()
    {
        // 스크립트가 켜질 때(미니게임 시작) 첫 상태로 강제 진입
        if (PlayingState != null) ChangeState(PlayingState);

        if (_healthSystem != null) _healthSystem.OnDeath += OnPlayerDead;

        _bow.SetActive(true);
    }

    private void OnDisable()
    {
        if (_healthSystem != null) _healthSystem.OnDeath -= OnPlayerDead;

        _bow.SetActive(false);
    }

    private void ChangeState(PlayerBaseState newState) // 플레이어 상태 변환
    {
        _currentState = newState;
        _currentState?.Enter();
    }

    private void OnPlayerDead() // 플레이어가 죽었을 때 실행 시킬 이벤트 메서드
    {
        if (_currentState != DeadState) ChangeState(DeadState);
    }

    /// <summary>
    /// 플레이어 초기화
    /// </summary>
    public void ResetPlayer()
    {
        if (stats != null) stats.hp = stats.defaultHp;
        GetComponent<HealthSystem>()?.ResetHp();

        ChangeState(PlayingState);
    }

    #region 조작

    private void RotateBow() // 활 회전
    {
        Vector3 _mousePos = new Vector3(MouseScreenPos.x, MouseScreenPos.y, 10f);
        Vector3 _worldPos = _mainCam.ScreenToWorldPoint(_mousePos);

        Vector2 _lookDir = (_worldPos - transform.position).normalized;

        float _bowAngle = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg;

        _bowAnim.transform.position = (Vector2)transform.position + (_lookDir * _bowDis);
        _bowAnim.transform.rotation = Quaternion.Euler(0, 0, _bowAngle);
    }

    public void OnMove(InputAction.CallbackContext context) // 이동 이벤트
    {
        if (!enabled) return;

        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!enabled) return;

        MouseScreenPos = context.ReadValue<Vector2>(); // 마우스 화면 좌표 저장
    }

    #endregion

    #region 투사체 발사

    public void OnShoot() // 애니메이션 이벤트에서 호출할 함수
    {
        if (!enabled) return;

        Vector2 _mouseWorldPos = _mainCam.ScreenToWorldPoint(new Vector2(MouseScreenPos.x, MouseScreenPos.y));
        Vector2 _fireDir = ((Vector2)_mouseWorldPos - (Vector2)_firePoint.position).normalized;
        float _angle = Mathf.Atan2(_fireDir.y, _fireDir.x) * Mathf.Rad2Deg;

        // 오브젝트 이미지가 위를 바라보고 있다면 _angle - 90, x축을 보고 있다면 _angle
        FireBullet(_firePoint.position, Quaternion.Euler(0, 0, _angle - 90), _fireDir);
    }

    /// <summary>
    /// Bullet 발사
    /// </summary>
    /// <param name="pos">발사 위치</param>
    /// <param name="rot">발사 각도</param>
    /// <param name="dir">발사 방향</param>
    public void FireBullet(Vector2 pos, Quaternion rot, Vector2 dir)
    {
        if (GameManager.Instance.CurrentState == GameState.GameOver) return;

        BulletController bullet = BulletManager.Instance.GetBullet();
        bullet.transform.SetPositionAndRotation(pos, rot);
        bullet.Init(dir, stats.bulletSpeed, stats.damage);
    }

    #endregion

}
