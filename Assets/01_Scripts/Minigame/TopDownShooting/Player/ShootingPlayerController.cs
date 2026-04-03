using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;

public class ShootingPlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _characterRenderer; // SpriteRenderer 컴포넌트를 참조하기 위한 변수

    // 상태 클래스에서 SpriteRenderer에 접근하기 위한 프로퍼티
    public SpriteRenderer CharacterRenderer => _characterRenderer;
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerStats stats;

    // FSM 상태 객체들이 읽어갈 입력값
    public Vector2 MoveInput { get; private set; }
    public Vector2 MouseScreenPos { get; private set; } // 마우스 화면 좌표

    // 상태 관리
    private PlayerBaseState _currentState;
    public ShootingPlayingState PlayingState { get; private set; }
    public ShootingDeadState DeadState { get; private set; }

    // 오브젝트 풀링
    private IObjectPool<Bullet> _bulletPool;

    private const string MainSpriteString = "MainSprite";

    private void Reset()
    {
        _characterRenderer = transform.Find(MainSpriteString).GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        // 상태 객체 생성
        PlayingState = new ShootingPlayingState(this);
        DeadState = new ShootingDeadState(this);

        // 풀 초기화
        _bulletPool = new ObjectPool<Bullet>(
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

 #region Bullet 오브젝트 풀링

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(stats.bulletPrefab).GetComponent<Bullet>();
        bullet.SetPool(_bulletPool);
        return bullet;
    }

    private void OnTakeFromPool(Bullet bullet) // 미리 생성한 오브젝트 활성화
    {
        bullet.gameObject.SetActive(true);
        bullet.ResetState();
    }

    private void OnReturnedToPool(Bullet bullet) // 미리 생성한 오브젝트 비활성화
    {
        bullet.gameObject.SetActive(false);
    }

    // 풀(Pool)이 넘치거나, 게임이 종료될 때 메모리를 정리하는 청소부 역할
    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void FireBullet(Vector2 pos, Quaternion rot, Vector2 dir)
    {
        Bullet bullet = _bulletPool.Get();
        bullet.transform.SetPositionAndRotation(pos, rot);
        bullet.SetDirection(dir, stats.attackSpeed);
    }

    #endregion

}
