using Unity.VisualScripting;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    [Header("장애물 루프 설정")]
    [SerializeField] private int _numBgCount = 5; // 배경 개수
    [SerializeField] private Vector3 _startObstaclePosition = new Vector3(5f, 0f, 0f); // 배경 개수


    private Obstacle[] _obstacles; // 장애물 담을 배열
    private int _obstacleCount = 0; // 장애물의 개수
    private Vector3 _obstacleLastPosition = Vector3.zero; // 마지막으로 배치된 장애물의 위치

    private void Awake()
    {
        _obstacles = GameObject.FindObjectsOfType<Obstacle>();
        _obstacleCount = _obstacles.Length;
    }

    private void Start()
    {
        ResetObstacles();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.BackGround)) // 충돌체가 백그라운드 태그를 달고 있으면
        {
            // 충돌한 백그라운드 오브젝트의 BoxCollider2D 컴포넌트에서 가로 길이를 가져옴
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            // 충돌한 백그라운드 오브젝트의 현재 위치를 저장
            Vector3 pos = collision.transform.position;

            // 백그라운드 오브젝트의 가로 길이와 _numBgCount 값을 곱하여 새로운 x 좌표를 계산
            // 백그라운드를 반복적으로 배치하기 위한 위치 조정
            pos.x += widthOfBgObject * _numBgCount;
            // 충돌한 백그라운드 오브젝트의 위치를 새로 계산된 위치로 업데이트
            collision.transform.position = pos;
            return;
        }

        // 충돌한 객체가 Obstacle인지 확인
        Obstacle obstacle = collision.GetComponent<Obstacle>();

        // 장애물이 충돌 시 랜덤 위치로 재배치
        if (obstacle) _obstacleLastPosition = obstacle.SetRandomPlace(_obstacleLastPosition, _obstacleCount);
    }

    /// <summary>
    /// 장애물 배치 초기화 메서드
    /// </summary>
    public void ResetObstacles()
    {
        _obstacleLastPosition = _startObstaclePosition;

        // 장애물 개수만큼 반복하여 각 장애물의 위치를 랜덤하게 설정
        // SetRandomPlace 함수는 각 장애물의 위치를 이전 장애물 위치를 기반으로 설정함
        for (int i = 0; i < _obstacleCount; ++i) _obstacleLastPosition = _obstacles[i].SetRandomPlace(_obstacleLastPosition, _obstacleCount);
    }
}
