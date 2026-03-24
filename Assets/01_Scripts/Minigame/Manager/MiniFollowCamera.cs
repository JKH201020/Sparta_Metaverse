using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFollowCamera : MonoBehaviour
{
    private Transform target; // 카메라가 따라갈 대상
    float offsetX; // 카메라와 대상 사이의 X축 오프셋

    // Update is called once per frame
    void Update()
    {
        // target이 null이라면 카메라가 아무것도 따라가지 않음
        if (target == null) return;

        Vector3 pos = transform.position; // 현재 카메라 위치를 저장

        pos.x = target.position.x + offsetX; // 카메라의 X 위치를 대상의 X 위치에 오프셋을 더해 설정
        transform.position = pos; // 계산된 새로운 위치로 카메라 이동
    }

    // 에셋 폴더에서 프리팹으로 불러올 때
    public void SetTarget(Transform _target) // 카메라 타겟 설정
    {
        target = _target;

        // 카메라의 현재 위치와 대상의 X 위치 차이를 오프셋으로 저장
        offsetX = transform.position.x - target.position.x;
    }
}
