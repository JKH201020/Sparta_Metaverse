using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5; // 배경 개수
    public int obstacleCount = 0; // 장애물의 개수
    public Vector3 obstacleLastPosition = Vector3.zero; // 마지막으로 배치된 장애물의 위치

    // Start is called before the first frame update
    void Start()
    {
        // Scene에 존재하는 모든 Obstacle 객체를 배열로 가져오기
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        // 첫 번째 장애물의 위치를 obstacleLastPosition에 저장
        obstacleLastPosition = obstacles[0].transform.position;
        // 장애물의 개수를 계산하여 저장
        obstacleCount = obstacles.Length;

        // 장애물 개수만큼 반복하여 각 장애물의 위치를 랜덤하게 설정
        for (int i = 0; i < obstacleCount; i++)
        {
            // SetRandomPlace 함수는 각 장애물의 위치를 이전 장애물 위치를 기반으로 설정함
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround")) // 충돌체가 백그라운드 태그를 달고 있으면
        {
            // 충돌한 백그라운드 오브젝트의 BoxCollider2D 컴포넌트에서 가로 길이를 가져옴
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            // 충돌한 백그라운드 오브젝트의 현재 위치를 저장
            Vector3 pos = collision.transform.position;

            // 백그라운드 오브젝트의 가로 길이와 numBgCount 값을 곱하여 새로운 x 좌표를 계산
            // 백그라운드를 반복적으로 배치하기 위한 위치 조정
            pos.x += widthOfBgObject * numBgCount;
            // 충돌한 백그라운드 오브젝트의 위치를 새로 계산된 위치로 업데이트
            collision.transform.position = pos;
            return;
        }

        // 충돌한 객체가 Obstacle인지 확인
        Obstacle obstacle = collision.GetComponent<Obstacle>();

        if(obstacle)
        {
            // 장애물이 충돌 시 랜덤 위치로 재배치
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
