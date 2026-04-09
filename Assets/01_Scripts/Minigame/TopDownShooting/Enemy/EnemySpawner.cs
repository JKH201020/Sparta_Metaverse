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

    [Header("스폰 거리")]
    [SerializeField] private float _minSpawnDistance = 8f; // 플레이어와의 최소 거리
    [SerializeField] private float _maxSpawnDistance = 15f; // 플레이어와의 최대 거리

    [Header("시스템 설정")]
    [SerializeField] private Transform _playerTransform; // 플레이어 위치
    [SerializeField] private float _spawnInterval = 5f; // 스폰 간격
    [SerializeField] private int _maxEnemyCount = 10; // 최대 적 생성 수

    // 다중 풀 관리를 위한 딕셔너리
    private Dictionary<EnemyType, IObjectPool<EnemyController>> _enemyPools;
    private IObjectPool<EnemyController> _enemyPool;
    private EnemyController _enemy;
    private Coroutine _coroutine;

    private float randomDist; // 적과 플레이어 사이의 랜덤 거리
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
            _enemyPool = new ObjectPool<EnemyController>(
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
        _enemy = _enemyPools[randomType].Get(); // 해당 풀에서 꺼내기
        _enemy.transform.position = CalculateDonutPosition(); // 도넛 형태의 랜덤 위치 계산
        _enemy.GetComponent<HealthSystem>()?.ResetHp(); // 체력 초기화
    }

    private Vector2 CalculateDonutPosition() // 소환 거리 계산
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized; // 랜덤 방향 (길이 1인 벡터)
        float randomDist = Random.Range(_minSpawnDistance, _maxSpawnDistance); // 최소 ~ 최대 사이의 랜덤 거리
        return (Vector2)_playerTransform.position + (randomDir * randomDist); // 플레이어 위치 기준으로 거리 합산
    }

    #endregion

    #region 오브젝트 풀링

    private EnemyController CreateEnemy(GameObject prefab, EnemyType type) // 적 생성
    {
        _enemy = Instantiate(prefab).GetComponent<EnemyController>();
        _enemy.SetPool(_enemyPools[type]);
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
