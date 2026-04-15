using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    [Header("투사체 설정"), SerializeField] private GameObject _bulletPrefab;

    private IObjectPool<BulletController> _bulletPool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // 풀 초기화
        _bulletPool = new ObjectPool<BulletController>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            maxSize: 50);
    }

    #region 투사체 소환 반납

    /// <summary>
    /// 외부(플레이어, 적)에서 총알을 빌려갈 때 쓰는 함수
    /// </summary>
    /// <returns></returns>
    public BulletController GetBullet()
    {
        return _bulletPool.Get();
    }

    /// <summary>
    /// 다 쓴 총알을 창고로 다시 반납할 때 쓰는 함수
    /// </summary>
    /// <param name="bullet">투사체</param>
    public void ReleaseBullet(BulletController bullet)
    {
        _bulletPool.Release(bullet);
    }

    /// <summary>
    /// 화면에 있는 투사체 초기화
    /// </summary>
    public void ClearAllBullets()
    {
        BulletController[] activeBullets = GetComponentsInChildren<BulletController>(false);
        foreach (BulletController bullet in activeBullets) ReleaseBullet(bullet);
    }

    #endregion

    #region 오브젝트 풀링

    private BulletController CreateBullet() // 투사체 생성
    {
        BulletController bullet = Instantiate(_bulletPrefab, transform).GetComponent<BulletController>();
        bullet.SetPool(_bulletPool); // 여기로 돌아오라고 알려줌
        return bullet;
    }

    private void OnTakeFromPool(BulletController bullet) // 풀에서 꺼내감
    {
        if (bullet != null) bullet.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(BulletController bullet) // 사용 후 풀에 반납
    {
        if (bullet != null) bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(BulletController bullet)
    {
        Destroy(bullet.gameObject);
    }

    #endregion

}
