using UnityEngine;

public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Dead
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private HealthSystem _enemyHealth;

    private float _speed; // 적 이동속도
    private float _attackTimer; // 공격 시간

    private EnemyState _currentState;

    private Transform _target; // 플레이어 위치
    private Vector2 _move; // 적 이동방향

    private void Reset()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _enemyHealth = GetComponentInChildren<HealthSystem>();
    }

    private void Awake()
    {
        _enemyHealth.OnDeath.AddListener(OnEnemyDeadEvent);
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
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
        if (_target == null) _target = GameObject.FindGameObjectWithTag(Tag.Player)?.transform;

        _attackTimer = 0f;
        ChangeState(EnemyState.Chase);
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

    private void Idle()
    {
        //_bowAnim.SetBool(AnimParams.IsIdle, true);
        _move = Vector2.zero;
    }

    private void Chase()
    {
        //_bowAnim.SetBool(AnimParams.IsIdle, false);
        //_bowAnim.SetBool(AnimParams.IsRunning, true);

        if (_target != null) _move = (_target.position - transform.position).normalized;

        if (_spriteRenderer != null) _spriteRenderer.flipX = _target.position.x < transform.position.x;

        // 공격 범위 안에 들어오면 공격 호출
        float sqrDistance = (_target.position - transform.position).sqrMagnitude;
        if (sqrDistance <= _enemyStats.attackRange * _enemyStats.attackRange) ChangeState(EnemyState.Attack);
    }

    private void Attack()
    {
        _move = Vector2.zero;
        //_bowAnim.SetBool(AnimParams.IsRunning, false);
        //_bowAnim.SetBool(AnimParams.IsAttacking, true);

        if (_target == null) return;

        Vector2 offset = _target.position - transform.position;

        // 공격범위 벗어났을 경우
        if (offset.sqrMagnitude > _enemyStats.attackRange * _enemyStats.attackRange)
        {
            //_bowAnim.SetBool(AnimParams.IsAttacking, false);
            ChangeState(EnemyState.Chase);
            return;
        }

        // 공격 쿨타임 계산
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _enemyStats.attackDelay)
        {
            _attackTimer = 0f; // 타이머 초기화
            // 아래 코드도 스포너에서 할당하도록 구현하기
            HealthSystem playerHealth = _target.GetComponent<HealthSystem>();

            // 플레이어에게 대미지 입힘
            if (playerHealth != null) playerHealth.TakeDamage(_enemyStats.damage);
        }
    }

    private void Dead()
    {
        _move = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnEnemyDeadEvent()
    {
        ChangeState(EnemyState.Dead);
    }

    #endregion
}
