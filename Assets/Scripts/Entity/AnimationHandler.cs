using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator 파라미터 이름을 미리 해시로 변환해 캐싱 (성능 최적화)
    static readonly int IsMove = Animator.StringToHash("IsMove");
    static readonly int IsJump = Animator.StringToHash("IsJump");

    protected Animator animator;

    bool isJumping = false; // 현재 점프 중인지 상태를 저장하는 변수
    float jumpStartTime = 0f;
    float jumpAnimationLength = 0f;

    protected virtual void Awake()
    {
        // 애니메이터 컴포넌트를 가져옴
        animator = GetComponent<Animator>();

        //// 초기 애니메이션 클립 정보 가져오기 (스크립트 시작 시)
        //AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        //foreach (AnimationClip clip in clips )
        //{
        //    jumpAnimationLength = clip.length;
        //}
    }

    public void Move(Vector2 obj) // 이동
    {
        // 이동 방향 벡터의 크기를 이용해 움직이는 중인지 판단
        animator.SetBool(IsMove, obj.magnitude > .5f);
    }

    public void Jump() // 점프
    {
        if (!isJumping && Input.GetKey(KeyCode.Space)) // 스페이스 바를 누를 때
        {
            isJumping = true;
            animator.SetBool(IsJump, true); // 점프
            jumpStartTime = Time.time; // 점프 시작 시간 기록
        }

        // 점프 중이고, 점프 시작 후 애니메이션 길이만큼 시간이 지났으면 점프 상태 종료
        if (isJumping && (Time.time - jumpStartTime) >= jumpAnimationLength)
        {
            isJumping = false;
            animator.SetBool(IsJump, false);
        }
    }
}
