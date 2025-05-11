using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        // Ű���� �Է��� ���� �̵� ���� ��� (��/��/��/��)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D �Ǵ� ��/��
        float vertical = Input.GetAxisRaw("Vertical"); // W/S �Ǵ� ��/��

        // ���� ���� ����ȭ (�밢���� �� �ӵ� ����)
        movementDirection = new Vector2(horizontal, vertical).normalized; // normalized: ���� ũ�⸦ 1�� ����

        if (Mathf.Abs(horizontal) > 0.01f) // ������ ���밪�� 0.01���� ū��? �󸶳� ���� ������
        {
            // �¿� ���⿡ ���� lookDirection ���� �������ش�.
            lookDirection = new Vector2(horizontal, 0).normalized;
        }
    }
}