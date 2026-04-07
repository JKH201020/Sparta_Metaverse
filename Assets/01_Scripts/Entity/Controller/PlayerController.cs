using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트
    [SerializeField] private SpriteRenderer _characterRenderer; // SpriteRenderer 컴포넌트를 참조하기 위한 변수
    [SerializeField] private Animator _animator;

    private Vector2 _moveInput = Vector2.zero; // 현재 이동 방향
    private Vector2 _lookDirection = Vector2.zero; // 현재 바라보는 방향
    private float _rotZ;
    private bool _isLeft;
    private float _fixedSpeed = 5f; // 고정된 이동속도
    private IInteractable _interactable;

    private const string MainSpriteString = "MainSprite";

    private void Reset()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterRenderer = transform.Find(MainSpriteString).GetComponent<SpriteRenderer>();
        _animator = transform.Find(MainSpriteString).GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameState.Talking
            || GameManager.Instance.CurrentState == GameState.Loading) return;

        Rotate(_lookDirection);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState == GameState.Talking
            || GameManager.Instance.CurrentState == GameState.Loading)
        {
            _rigidbody.velocity = Vector2.zero; // 정지
            return;
        }

        Movement(_moveInput);
    }

    private void OnDrawGizmos() // 상호작용 범위 기즈모
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    private void Movement(Vector2 direction) // 이동
    {
        _rigidbody.velocity = direction * _fixedSpeed;
    }

    private void Rotate(Vector2 direction) // 바라보는 방향
    {
        if (direction == Vector2.zero) return;

        _rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       _isLeft = Mathf.Abs(_rotZ) > 90f;

        _characterRenderer.flipX = _isLeft;
    }

    public void OnMove(InputAction.CallbackContext context) // 이동 이벤트
    {
        _moveInput = context.ReadValue<Vector2>();

        if (_moveInput != Vector2.zero)
        {
            _lookDirection = _moveInput;
            _animator.SetBool(AnimParams.IsRunning, true);
        }
        else
        {
            _animator.SetBool(AnimParams.IsRunning, false);
        }
    }

    public void OnInteract(InputAction.CallbackContext context) // 상호작용 이벤트
    {
        if (context.performed && GameManager.Instance.CurrentState != GameState.Talking)
        {
            // 주변 1 유닛 반경 내의 콜라이더를 찾음
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);

            foreach (var hit in hitColliders)
            {
                // 상대방이 IInteractable 인터페이스를 가지고 있는지 확인
                _interactable = hit.GetComponent<IInteractable>();

                if (_interactable != null)
                {
                    _interactable.Interact(); // 상속받은 NPC의 Interact()가 실행됨
                    break;
                }
            }
        }
    }
}