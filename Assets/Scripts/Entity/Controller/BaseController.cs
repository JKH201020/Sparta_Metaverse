using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트
    protected SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트를 참조하기 위한 변수
    protected AnimationHandler animationHandler;

    [SerializeField] private SpriteRenderer characterRenderer; // 좌우 반전을 위한 렌더러

    protected Vector2 movementDirection = Vector2.zero; // 현재 이동 방향
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // 현재 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }

    // 게임 오브젝트나 컴포넌트 간의 참조를 설정하거나 초기 상태를 설정하는 등의 초기화 작업을 수행하는 데 적합
    protected virtual void Awake() // 이 스크립트의 게임오브젝트가 로드될 때 자동으로 딱 한 번 호출
    {
        // 현재 스크립트가 연결된 게임 오브젝트가 가지고 있는 Rigidbody 2D 컴포넌트를 찾아 반환하는 역할
        _rigidbody = GetComponent<Rigidbody2D>();
        // 현재 게임 오브젝트에서 SpriteRenderer 컴포넌트를 자식에서 찾아 spriteRenderer 변수에 할당
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // 현재 스크립트가 연결된 게임 오브젝트의 자식이 가지고 있는 AnimationHandler 컴포넌트를 찾아 반환하는 역할
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }

    protected virtual void Start()
    {
        Time.timeScale = 1.0f; // Scene 전환 후 멈춰있는 상황 방지ㅁㅁ
    }

    protected virtual void Update()
    {
        HandleAction();
        Flip();
        Jump();
    }

    protected virtual void FixedUpdate() // 물리 업데이트 (고정된 시간 간격으로 호출됨)
    {
        Movement(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    void Movement(Vector2 direction) // 이동
    {
        direction = direction * 5; // 이동 속도

        _rigidbody.velocity = direction; // 실제 물리 이동
        animationHandler.Move(direction); // 이동 애니메이션 처리
    }

    void Flip() // 캐릭터 이미지 좌우반전
    {
        // Rigidbody2D 컴포넌트가 존재한다면 속도 기반으로 좌우 반전을 결정
        if (_rigidbody.velocity.x > 0.1f)
        {
            // Rigidbody2D의 x축 속도가 양수이고 어느 정도 크다면 (오른쪽으로 이동 중)
            spriteRenderer.flipX = false;
        }
        else if (_rigidbody.velocity.x < -0.1f)
        {
            // Rigidbody2D의 x축 속도가 음수이고 어느 정도 작다면 (왼쪽으로 이동 중)
            spriteRenderer.flipX = true; // 좌우반전
        }
    }

    void Jump() // 점프
    {
        animationHandler.Jump();
    }
}