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
        // 키보드 입력을 통해 이동 방향 계산 (좌/우/상/하)
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D 또는 ←/→
        float vertical = Input.GetAxisRaw("Vertical"); // W/S 또는 ↑/↓

        // 방향 벡터 정규화 (대각선일 때 속도 보정)
        movementDirection = new Vector2(horizontal, vertical).normalized; // normalized: 벡터 크기를 1로 만듦

        if (Mathf.Abs(horizontal) > 0.01f) // 수평의 절대값이 0.01보다 큰가? 얼마나 세게 눌렀나
        {
            // 좌우 방향에 따라 lookDirection 값을 설정해준다.
            lookDirection = new Vector2(horizontal, 0).normalized;
        }
    }
}