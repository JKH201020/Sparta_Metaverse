using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트
    [SerializeField] private SpriteRenderer _characterRenderer; // SpriteRenderer 컴포넌트를 참조하기 위한 변수

    private Vector2 _moveInput = Vector2.zero; // 현재 이동 방향
    private Vector2 _lookDirection = Vector2.zero; // 현재 바라보는 방향

    private const string MainSpriteString = "MainSprite";

    private void Reset()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterRenderer = transform.Find(MainSpriteString).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        // 대화가 아닐 경우에만 방향 전환 가능
        if (GameManager.Instance.CurrentState == GameState.Talking) return;

        Rotate(_lookDirection);
    }

    private void FixedUpdate()
    {
        // 대화 중일 경우
        if (GameManager.Instance.CurrentState == GameState.Talking)
        {
            _rigidbody.velocity = Vector2.zero; // 정지
            return;
        }

        Movement(_moveInput);
    }

    public void OnMove(InputAction.CallbackContext context) // 이동 이벤트
    {
        _moveInput = context.ReadValue<Vector2>();

        if (_moveInput != Vector2.zero) _lookDirection = _moveInput;
    }

    private void Movement(Vector2 direction) // 이동
    {
        _rigidbody.velocity = direction * 5f;
    }

    private void Rotate(Vector2 direction) // 바라보는 방향
    {
        if (direction == Vector2.zero) return;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        _characterRenderer.flipX = isLeft;
    }
}