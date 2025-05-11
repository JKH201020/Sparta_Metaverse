using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    Animator animator = null; // 애니메이터와 리지드바디 변수 선언
    Rigidbody2D _rigidbody = null; // 플레이어의 리지드바디 (물리 엔진 적용)

    float flapForce = 6f; // 점프 강도 (플랩)
    float forwardSpeed = 3f; // 앞으로 나가는 속도 (수평 이동)
    public static bool isDead = false; // 플레이어가 죽었는지 확인하는 변수
    bool isFlap = false; // 점프(플랩) 여부 확인하는 변수

    // Start is called before the first frame update
    void Start()
    {
        // 애니메이터와 리지드바디를 컴포넌트에서 찾기
        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) // 생존
        {
            // 점프 (플랩) 입력 처리
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFlap = true; // 점프 시작
            }
        }
    }

    public void FixedUpdate() // 물리 업데이트 (고정된 시간 간격으로 호출됨)
    {
        if (isDead) return; // 죽었으면 물리 연산 하지 않음

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed; // 수평 속도는 일정하게 유지 (앞으로 계속 이동)

        if (isFlap)
        {
            velocity.y += flapForce; // 점프 효과 (수직 속도 증가)
            isFlap = false; // 점프 완료 후 초기화
        }

        _rigidbody.velocity = velocity; // 리지드바디 속도 업데이트

        // 점프 시 각도 조정 (위아래로 기울기)
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90); // y축: -90 ~ 90
        float lerpAngle = Mathf.Lerp(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5f);

        transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
    }

    public void OnCollisionEnter2D(Collision2D collision) // 충돌 시작 시 호출
    {
        if (isDead) return; // 이미 죽었으면 충돌 처리 안 함

        // 죽음 애니메이션 실행
        animator.SetInteger("IsDie", 1);
        isDead = true; // 죽음 상태로 변경
        MiniGameUIManager.Instance.GameOver();
    }
}
