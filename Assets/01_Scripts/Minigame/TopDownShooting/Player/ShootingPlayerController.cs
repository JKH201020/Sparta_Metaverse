using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;

public class ShootingPlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _characterRenderer; // SpriteRenderer 컴포넌트를 참조하기 위한 변수
    [SerializeField] private Animator _bowAnim; // 활 애니메이션
    [SerializeField] private Transform _firePoint; // 화살 발사 위치
    [SerializeField] private GameObject _bow;

    private Camera _mainCam;
    private Vector2 _mouseWorldPos; // 마우스 위치
    private Vector2 _fireDir; // 발사 방향
    private float _angle; // 발사 각도
    private float _defalutBulletSpeed = 1.0f;
    private Vector3 _mousePos;
    private Vector3 _worldPos;
    private Vector2 _lookDir; // 보는 방향
    private float _bowAngle; // 활 각도
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

    // 오브젝트 풀링
    private IObjectPool<BulletController> _bulletPool;

    private const string MainSpriteString = "MainSprite";
    private const string BowSpriteString = "BowSprite";

    private void Reset()
    {
        _characterRenderer = transform.Find(MainSpriteString).GetComponent<SpriteRenderer>();
        _bowAnim = transform.Find(BowSpriteString).GetComponent<Animator>();
        _firePoint = transform.Find(BowSpriteString).GetComponent<Transform>();
        _bow = GameObject.Find(BowSpriteString);
    }

    private void Awake()
    {
        _mainCam = Camera.main;

        Rigidbody = GetComponent<Rigidbody2D>();
        // 상태 객체 생성
        PlayingState = new ShootingPlayingState(this);
        DeadState = new ShootingDeadState(this);

        // 풀 초기화
        _bulletPool = new ObjectPool<BulletController>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            maxSize: 20
            );
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

        _bow.SetActive(true);
    }

    private void OnDisable()
    {
        _bow.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled) return;

        // 적이나 피격 판정에 닿으면 사망 처리 (태그 확인 로직 등을 추가해도 됨)
        if (_currentState != DeadState) ChangeState(DeadState);
    }

    private void ChangeState(PlayerBaseState newState) // 컨트롤 변환
    {
        // _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    private void RotateBow() // 활 회전
    {
        _mousePos = new Vector3(MouseScreenPos.x, MouseScreenPos.y, 10f);
        _worldPos = _mainCam.ScreenToWorldPoint(_mousePos);

        _lookDir = (_worldPos - transform.position).normalized;

        _bowAngle = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg;

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

    public void OnShoot() // 애니메이션 이벤트에서 호출할 함수
    {
        if (!enabled) return;

        _mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector2(MouseScreenPos.x, MouseScreenPos.y));
        _fireDir = ((Vector2)_mouseWorldPos - (Vector2)_firePoint.position).normalized;
        _angle = Mathf.Atan2(_fireDir.y, _fireDir.x) * Mathf.Rad2Deg;

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
        BulletController bullet = _bulletPool.Get();
        bullet.transform.SetPositionAndRotation(pos, rot);
        bullet.Init(dir, stats.bulletSpeed, stats.damage);
    }

    #region Bullet 오브젝트 풀링

    private BulletController CreateBullet() // 오브젝트 풀링으로 Bullet 생성
    {
        BulletController bullet = Instantiate(stats.bulletPrefab).GetComponent<BulletController>();
        bullet.SetPool(_bulletPool);
        return bullet;
    }

    private void OnTakeFromPool(BulletController bullet) // 미리 생성한 오브젝트 활성화
    {
        bullet.gameObject.SetActive(true);
        bullet.ResetState();
    }

    private void OnReturnedToPool(BulletController bullet) // 미리 생성한 오브젝트 비활성화
    {
        bullet.gameObject.SetActive(false);
    }

    // 풀(Pool)이 넘치거나, 게임이 종료될 때 메모리를 정리하는 청소부 역할
    private void OnDestroyPoolObject(BulletController bullet)
    {
        Destroy(bullet.gameObject);
    }

    #endregion

}
