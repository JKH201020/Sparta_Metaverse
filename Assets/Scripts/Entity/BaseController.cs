using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트

    [SerializeField] private SpriteRenderer characterRenderer; // 좌우 반전을 위한 렌더러
    [SerializeField] private Transform weaponPivot; // 무기를 회전시킬 기준 위치

    protected Vector2 movementDirection = Vector2.zero; // 현재 이동 방향
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // 현재 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero; // 넉백 방향
    private float knockbackDuration = 0.0f; // 넉백 지속 시간


    // 게임 오브젝트나 컴포넌트 간의 참조를 설정하거나 초기 상태를 설정하는 등의 초기화 작업을 수행하는 데 적합
    protected virtual void Awake() // 이 스크립트의 게임오브젝트가 로드될 때 자동으로 딱 한 번 호출
    {
        // 현재 스크립트가 연결된 게임 오브젝트가 가지고 있는 Rigidbody 2D 컴포넌트를 찾아 반환하는 역할
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);

        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;  // 넉백 시간 감소
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)
    {
        direction = direction * 5; // 이동 속도

        _rigidbody.velocity = direction; // 실제 물리 이동
    }

    public void Rotate(Vector2 direction) // 캐릭터 좌우반전
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f; // 90보다 크면 왼쪽을 바라봄

        characterRenderer.flipX = isLeft;
    }
}