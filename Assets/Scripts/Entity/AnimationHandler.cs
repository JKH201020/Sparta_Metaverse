using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Animator �Ķ���� �̸��� �̸� �ؽ÷� ��ȯ�� ĳ�� (���� ����ȭ)
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsJump = Animator.StringToHash("IsJump");

    protected Animator animator;

    protected virtual void Awake()
    {
        // �ִϸ����� ������Ʈ�� ������
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 obj) // �̵�
    {
        // �̵� ���� ������ ũ�⸦ �̿��� �����̴� ������ �Ǵ�
        animator.SetBool(IsMove, obj.magnitude > .5f);
    }

    public void Jump() // ����
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
