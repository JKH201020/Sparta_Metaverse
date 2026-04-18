using UnityEngine;
using UnityEngine.InputSystem;

public class Plane : MonoBehaviour
{
    [SerializeField] private Animator _animator; // 애니메이터와 리지드바디 변수 선언
    [SerializeField] private Rigidbody2D _rigidbody; // 플레이어의 리지드바디 (물리 엔진 적용)

    private float _flapForce = 6f; // 점프 강도 (플랩)
    private float _forwardSpeed = 3f; // 앞으로 나가는 속도 (수평 이동)
    public bool isDead = false; // 플레이어가 죽었는지 확인하는 변수
    private bool _isFlap = false; // 점프(플랩) 여부 확인하는 변수

    private void Reset()
    {
        // 애니메이터와 리지드바디를 컴포넌트에서 찾기
        _animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() // 물리 업데이트 (고정된 시간 간격으로 호출됨)
    {
        Jump();
    }

    public void OnCollisionEnter2D(Collision2D collision) // 충돌 시작 시 호출
    {
        if (isDead) return; // 이미 죽었으면 충돌 처리 안 함

        // 죽음 애니메이션 실행
        _animator.SetInteger(AnimParams.IsDie, 1);
        isDead = true; // 죽음 상태로 변경
    }

    private void Jump() // 점프 로직
    {
        if (isDead) return; // 죽었으면 물리 연산 하지 않음

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = _forwardSpeed; // 수평 속도는 일정하게 유지 (앞으로 계속 이동)

        if (_isFlap) // 점프 신호가 들어왔을 때
        {
            velocity.y = _flapForce; // 점프 효과 (수직 속도 증가)
            _isFlap = false; // 점프 완료 후 초기화
        }

        _rigidbody.velocity = velocity; // 리지드바디 속도 업데이트

        // 점프 시 각도 조정 (위아래로 기울기)
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90); // y축: -90 ~ 90

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle),Time.fixedDeltaTime * 5f);
    }

    public void OnJump(InputAction.CallbackContext context) /// 점프 인풋 감지
    {
        if (!enabled || isDead) return;

        if (context.started) _isFlap = true; // 점프 시작
    }
}
