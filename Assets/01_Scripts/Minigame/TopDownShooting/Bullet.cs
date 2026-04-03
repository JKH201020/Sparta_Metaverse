using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _direction; // 방향
    private float _speed; // 속도
    private bool _isReleased; // 중복 반납 방지

    private IObjectPool<Bullet> _managedPool;

    private void Reset()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _speed * _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) // collision이 충돌했을 경우
    {
        if (collision.CompareTag(Tag.Wall) || collision.CompareTag(Tag.Enemy))
        {
            _isReleased = true;
            _managedPool.Release(this);
        }
    }

    private void OnBecameInvisible() // 화면 밖으로 나갔을 경우 (최적화)
    {
        if (_isReleased || !gameObject.activeSelf) return;

        _isReleased = true;
        _managedPool.Release(this);
    }

    public void ResetState()
    {
        _isReleased = false;
    }

    /// <summary>
    /// Bullet 나가는 방향 설정
    /// </summary>
    /// <param name="dir">방향</param>
    /// <param name="speed">투사체 속도</param>
    public void SetDirection(Vector2 dir, float attackSpeed)
    {
        _direction = dir.normalized;
        _speed = attackSpeed;

        // 다시 활성화될 때 물리 속도를 초기화해줘야 함
        if (_rb != null) _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _direction * _speed;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        // 오브젝트 이미지가 위를 바라보고 있다면 angle - 90, x축을 보고 있다면 angle
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _managedPool = pool;
    }
}
