using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : Singleton<DamageTextPool>
{
    [Header("설정")]
    [SerializeField] private GameObject _damagePrefab;
    [SerializeField] private RectTransform _container;
    [SerializeField] private int _initialPoolSize = 20;

    private Queue<DamageTextUI> _pool = new Queue<DamageTextUI>();

    private void Reset()
    {
        _container = GetComponent<RectTransform>(); 
    }

    protected override void Awake()
    {
        for (int i = 0; i < _initialPoolSize; i++) CreateNewInstance();
        if (_damagePrefab == null) _damagePrefab = GameObject.FindWithTag(Tag.UI).gameObject;
    }

    private void CreateNewInstance() // 대미지 UI 생성
    {
        GameObject obj = Instantiate(_damagePrefab, _container);
        DamageTextUI dt = obj.GetComponent<DamageTextUI>();
        obj.SetActive(false); // 비활성화 상태로 보관
        _pool.Enqueue(dt);
    }

    /// <summary>
    /// 풀에서 꺼내기
    /// </summary>
    /// <param name="worldPosition">적 월드 좌표</param>
    /// <returns></returns>
    public DamageTextUI Get(Vector3 worldPosition)
    {
        if (_pool.Count == 0) CreateNewInstance();

        DamageTextUI dt = _pool.Dequeue();

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        dt.transform.position = screenPos;
        dt.gameObject.SetActive(true);
        return dt;
    }

    /// <summary>
    /// 사용 후 풀에 반납
    /// </summary>
    /// <param name="dt"></param>
    public void ReturnToPool(DamageTextUI dt)
    {
        dt.gameObject.SetActive(false);
        _pool.Enqueue(dt);
    }
}
