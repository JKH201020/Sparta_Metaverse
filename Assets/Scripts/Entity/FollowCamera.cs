using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (플레이어)
    public float Speed = 5f; // 카메라 이동 속도
    public Vector2 minBounds; // 카메라가 도달할 수 있는 최소 위치
    public Vector2 maxBounds; // 카메라가 도달할 수 있는 최대 위치
    private Vector3 offset; // 카메라와 플레이어 간의 초기 거리

    void Start()
    {
        // 초기 거리 설정 (보통 z 축만 -10)
        offset = transform.position - target.position;
    }

    // LateUpdate()를 사용하는 이유는 모든 캐릭터 이동이 끝난 후에 카메라가 따라가는 연출을 만들기 위함
    void LateUpdate()
    {
        // 따라가야 할 위치 계산 (z는 유지)
        Vector3 pos = target.position + offset;
        pos.z = transform.position.z;

        // 위치 제한 적용
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

        // 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * Speed);
    }
}
