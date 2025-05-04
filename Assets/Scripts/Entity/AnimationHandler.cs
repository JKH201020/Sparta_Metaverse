using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator 파라미터 이름을 미리 해시로 변환해 캐싱 (성능 최적화)
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsJump = Animator.StringToHash("IsJump");

    protected Animator animator;

    protected virtual void Awake()
    {
        // 애니메이터 컴포넌트를 가져옴
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 obj) // 이동
    {
        // 이동 방향 벡터의 크기를 이용해 움직이는 중인지 판단
        animator.SetBool(IsMove, obj.magnitude > .5f);
    }

    public void Jump() // 점프
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool(IsJump, true);
        }
        else
        {
            animator.SetBool(IsJump, false);
        }
    }
}
