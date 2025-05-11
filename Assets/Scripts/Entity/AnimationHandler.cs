using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator �Ķ���� �̸��� �̸� �ؽ÷� ��ȯ�� ĳ�� (���� ����ȭ)
    static readonly int IsMove = Animator.StringToHash("IsMove");
    static readonly int IsJump = Animator.StringToHash("IsJump");

    protected Animator animator;

    bool isJumping = false; // ���� ���� ������ ���¸� �����ϴ� ����
    float jumpStartTime = 0f;
    float jumpAnimationLength = 0f;

    protected virtual void Awake()
    {
        // �ִϸ����� ������Ʈ�� ������
        animator = GetComponent<Animator>();

        //// �ʱ� �ִϸ��̼� Ŭ�� ���� �������� (��ũ��Ʈ ���� ��)
        //AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        //foreach (AnimationClip clip in clips )
        //{
        //    jumpAnimationLength = clip.length;
        //}
    }

    public void Move(Vector2 obj) // �̵�
    {
        // �̵� ���� ������ ũ�⸦ �̿��� �����̴� ������ �Ǵ�
        animator.SetBool(IsMove, obj.magnitude > .5f);
    }

    public void Jump() // ����
    {
        if (!isJumping && Input.GetKey(KeyCode.Space)) // �����̽� �ٸ� ���� ��
        {
            isJumping = true;
            animator.SetBool(IsJump, true); // ����
            jumpStartTime = Time.time; // ���� ���� �ð� ���
        }

        // ���� ���̰�, ���� ���� �� �ִϸ��̼� ���̸�ŭ �ð��� �������� ���� ���� ����
        if (isJumping && (Time.time - jumpStartTime) >= jumpAnimationLength)
        {
            isJumping = false;
            animator.SetBool(IsJump, false);
        }
    }
}
