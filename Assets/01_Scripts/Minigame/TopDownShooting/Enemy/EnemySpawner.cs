using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum EnemyType // 적 타입
{
    Normal,
    Fast,
    Tank
}

[System.Serializable]
public struct EnemySpawnData // 인스펙터 노출용 구조체
{
    public EnemyType enemyType;
    public GameObject prefab;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 설정"), SerializeField] private List<EnemySpawnData> _enemyDataList;

    [Header("스폰 위치")]
    [SerializeField] private float _mapMinX = -16f;
    [SerializeField] private float _mapMaxX = 16f;
    [SerializeField] private float _mapMinY = -9f;
    [SerializeField] private float _mapMaxY = 9f;
    [SerializeField] private float _minSpawnDistance = 5f; // 적 소환 최소 거리

    [Header("시스템 설정")]
    [SerializeField] private Transform _playerTransform; // 플레이어 위치
    [SerializeField] private float _spawnInterval = 5f; // 스폰 간격
    [SerializeField] private int _maxEnemyCount = 10; // 최대 적 생성 수

    // 다중 풀 관리를 위한 딕셔너리
    private Dictionary<EnemyType, IObjectPool<EnemyController>> _enemyPools;
    private Coroutine _coroutine;

    private int _currentActiveEnemies = 0; // 현재 생성된 적 수

    private void Reset()
    {
        _playerTransform = GameObject.FindWithTag(Tag.Player)?.transform;
    }

    private void Awake()
    {
        _enemyPools = new Dictionary<EnemyType, IObjectPool<EnemyController>>();

        foreach (var data in _enemyDataList)
        {
            IObjectPool<EnemyController> _enemyPool = new ObjectPool<EnemyController>(
            () => CreateEnemy(data.prefab, data.enemyType),
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            maxSize: 20
            );
            _enemyPools.Add(data.enemyType, _enemyPool);
        }
    }

    private void Start()
    {
        if (_coroutine == null) _coroutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable() // 안전하게 코루틴 종료
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void OnDrawGizmos()
    {
        // 맵 전체 구역
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((_mapMaxX + _mapMinX) / 2, (_mapMaxY + _mapMinY) / 2, 0);
        Vector3 size = new Vector3(_mapMaxX - _mapMinX, _mapMaxY - _mapMinY, 1);
        Gizmos.DrawWireCube(center, size);

        // 스폰 금지 구역 표시
        if (_playerTransform != null) 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_playerTransform.position, _minSpawnDistance);
        }
    }

    #region 소환

    private IEnumerator SpawnRoutine() // 소환 코루틴
    {
        while (true)
        {
            // 현재 맵에 활성화된 적의 수가 최대치보다 적을 때만 스폰
            if (_currentActiveEnemies < _maxEnemyCount) Spawn();

            yield return new WaitForSeconds(_spawnInterval); // 지정된 시간만큼 대기
        }
    }

    private void Spawn() // 소환
    {
        EnemyType randomType = (EnemyType)Random.Range(0, _enemyDataList.Count); // 랜덤 타입 결정
        EnemyController _enemy = _enemyPools[randomType].Get(); // 해당 풀에서 꺼내기
        _enemy.transform.position = CalculateDonutPosition(); // 도넛 형태의 랜덤 위치 계산
        _enemy.GetComponent<HealthSystem>()?.ResetHp(); // 체력 초기화
    }

    private Vector2 CalculateDonutPosition() // 소환 거리 계산
    {
        Vector2 spawnPos = Vector2.zero;
        bool isValidPosition = false;
        int attemptCount = 0; // 무한 루프 방지용

        // 소환 적정 위치를 찾을 때까지 반복
        while (!isValidPosition && attemptCount < 10)
        {
            attemptCount++;

            // 맵 전체 범위 안에서 랜덤 좌표 생성
            float x = Random.Range(_mapMinX, _mapMaxX);
            float y = Random.Range(_mapMinY, _mapMaxY);
            spawnPos = new Vector2(x, y);

            // 플레이어와의 거리 계산
            float distance = Vector2.Distance(spawnPos, _playerTransform.position);

            if (distance >= _minSpawnDistance) isValidPosition = true;
        }

        return spawnPos;
    }

    #endregion

    #region 오브젝트 풀링

    private EnemyController CreateEnemy(GameObject prefab, EnemyType type) // 적 생성
    {
        // Instantiate 할 때 'this.transform'을 넣어 현재 스포너의 자식 오브젝트로 생성
        EnemyController _enemy = Instantiate(prefab, this.transform).GetComponent<EnemyController>();
        _enemy.SetPool(_enemyPools[type], type);
        return _enemy;
    }

    private void OnTakeFromPool(EnemyController enemy) // 생성시 활성화
    {
        enemy.gameObject.SetActive(true);
        _currentActiveEnemies++;
    }

    private void OnReturnedToPool(EnemyController enemy) // 죽으면 비활성화
    {
        enemy.gameObject.SetActive(false);
        _currentActiveEnemies--;
    }

    private void OnDestroyPoolObject(EnemyController enemy) // 풀에 자리 없으면 제거
    {
        Destroy(enemy.gameObject);
    }

    #endregion

}
