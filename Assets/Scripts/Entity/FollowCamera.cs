using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상

    // Update is called once per frame
    void Update()
    {
        if (target == null) return; // target이 null이라면 카메라가 아무것도 따라가지 않음

        Vector3 pos = transform.position; // 현재 카메라 위치를 저장
        pos.x = target.position.x; // 카메라의 X 위치를 대상의 X 위치
        pos.y = target.position.y; // 카메라의 Y 위치를 대상의 Y 위치
        transform.position = pos; // 계산된 새로운 위치로 카메라 이동
    }
}
