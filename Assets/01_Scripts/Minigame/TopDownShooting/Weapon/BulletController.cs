using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _direction; // 방향
    private float _speed; // 속도
    private bool _isReleased; // 중복 반납 방지
    private float _damage; // 대미지

    private IObjectPool<BulletController> _managedPool;

    private void Reset()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _speed * _direction;
    }

    private void OnEnable()
    {
        ResetState();
    }

    private void OnTriggerEnter2D(Collider2D collision) // collision이 충돌했을 경우
    {
        if (collision.CompareTag(Tag.Wall) || collision.CompareTag(Tag.Enemy))
        {
            HealthSystem enemyHealth = collision.GetComponent<HealthSystem>();

            if (enemyHealth != null) enemyHealth.TakeDamage(_damage);

            Release();
        }
    }

    private void OnBecameInvisible() // 화면 밖으로 나갔을 경우 (최적화)
    {
        Release();
    }

    private void Release()
    {
        if (_isReleased || !gameObject.activeSelf) return;

        _isReleased = true;
        _managedPool.Release(this);
    }

    /// <summary>
    /// 투사체 상태 초기화
    /// </summary>
    public void ResetState()
    {
        _isReleased = false;
    }

/// <summary>
/// Bullet 초기설정 / 적이나 플레이어가 사용할 때 각자 스탯을 적용하기 위함
/// </summary>
/// <param name="dir">발사 방향</param>
/// <param name="bulletSpeed">투사체 속도</param>
/// <param name="damage">대미지</param>
    public void Init(Vector2 dir, float bulletSpeed, float damage)
    {
        ResetState();

        _direction = dir.normalized;
        _speed = bulletSpeed;
        _damage = damage;
    }

    /// <summary>
    /// 투사체를 사용한 후 반납할 풀 알려주기
    /// </summary>
    /// <param name="pool"></param>
    public void SetPool(IObjectPool<BulletController> pool)
    {
        _managedPool = pool;
    }
}
