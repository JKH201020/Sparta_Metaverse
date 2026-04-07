using UnityEngine;

public enum EnemyState
{
    Chase,
    Attack,
    Dead
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _enemyAnim;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _weaponAnim;

    [Header("스탯")]
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private HealthSystem _enemyHealth;

    private float _speed; // 적 이동속도
    private float sqrDistance;

    private EnemyState _currentState;

    private Transform _target; // 플레이어 위치
    private Vector2 _move; // 적 이동방향
    private Vector2 offset;

    private const string MainSpriteString = "MainSprite";
    private const string WeaponString = "Weapon";

    private void Reset()
    {
        _enemyAnim = transform.Find(MainSpriteString).GetComponentInChildren<Animator>();
        _weaponAnim = transform.Find(WeaponString).GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _enemyHealth = GetComponentInChildren<HealthSystem>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Dead:
                Dead();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (_currentState == EnemyState.Chase)
        {
            _speed = _enemyStats.moveSpeed;
            _rigidbody.velocity = _move * _speed;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnEnable()
    {
        _enemyHealth.OnDeath += OnEnemyDeadEvent;
        _enemyHealth.ResetHp();

        // 이 부분 스포너든지 어떻게든 수정해보기. 계속 활성화할 때마다 부하걸림
        // 그리고 몬스터 체력 복구 시키기
        if (_target == null) _target = GameObject.FindGameObjectWithTag(Tag.Player)?.transform;

        ChangeState(EnemyState.Chase);
    }

    private void OnDisable()
    {
        _enemyHealth.OnDeath -= OnEnemyDeadEvent;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemyStats.attackRange);
    }

    private void ChangeState(EnemyState newState) // 적 상태 변경
    {
        _currentState = newState;
    }

    #region 적 상태

    private void Chase()
    {
        _enemyAnim.SetBool(AnimParams.IsRunning, true);
        _weaponAnim.SetBool(AnimParams.IsAttacking, false);

        if (_target != null) _move = (_target.position - transform.position).normalized;

        if (_spriteRenderer != null) _spriteRenderer.flipX = _target.position.x < transform.position.x;

        // 공격 범위 안에 들어오면 공격 호출
        sqrDistance = (_target.position - transform.position).sqrMagnitude;
        if (sqrDistance <= _enemyStats.attackRange * _enemyStats.attackRange) ChangeState(EnemyState.Attack);
    }

    private void Attack()
    {
        _move = Vector2.zero;
        _enemyAnim.SetBool(AnimParams.IsRunning, false);
        _weaponAnim.SetBool(AnimParams.IsAttacking, true);

        if (_target == null) return;

        offset = _target.position - transform.position;

        // 공격범위 벗어났을 경우
        if (offset.sqrMagnitude > _enemyStats.attackRange * _enemyStats.attackRange)
        {
            _weaponAnim.SetBool(AnimParams.IsAttacking, false);
            ChangeState(EnemyState.Chase);
            return;
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    private void OnEnemyDeadEvent()
    {
        ChangeState(EnemyState.Dead);
    }

    #endregion
}
