using UnityEngine;
using UnityEngine.Pool;

public enum EnemyState
{
    Chase,// 추적
    Attack, // 공격
    Dead // 죽음
}

public class EnemyController : MonoBehaviour
{

    [Header("최상위 오브젝트"), SerializeField] private Rigidbody2D _rigidbody;

    [Header("자식 오브젝트 MainSprite")]
    [SerializeField] private Animator _enemyAnim; // 본채 애니메이션
    [SerializeField] private SpriteRenderer _spriteRenderer; // 본채 이미지

    [Header("자식 오브젝트 WeaponPivot")]
    [SerializeField] private GameObject _weaponSR; // 무기 이미지 있는 오브젝트
    [SerializeField] private Animator _weaponAnim; // 무기 애니메이션
    [SerializeField] private Transform _weaponPos; // 무기 위치

    [Header("스탯")]
    [SerializeField] private EnemyStats _stats; // 적 스탯
    [SerializeField] private HealthSystem _healthSystem; // 

    private float _speed; // 적 이동속도
    private float _sqrDistance;
    private float _defalutAttackSpeed = 1.0f; // 기본 공격 속도
    private float _currentDist; // 공격 감지 범위

    private EnemyState _currentState; // 적 현재 상태
    private Transform _target; // 플레이어 위치
    private Vector2 _move; // 적 이동방향
    private AnimatorStateInfo _stateInfo; // 현재 실행 중인 애니메이션
    private IObjectPool<EnemyController> _pool; // 자기가 속한 풀을 기억할 변수
    private EnemyType _enemyType;

    private const string MainSpriteString = "MainSprite";
    private const string WeaponString = "WeaponPivot/Weapon";
    private const string WeaponPivotString = "WeaponPivot";
    private const string NearAttackString = "NearAttack";

    private void Reset()
    {
        _enemyAnim = transform.Find(MainSpriteString).GetComponentInChildren<Animator>();
        _weaponAnim = transform.Find(WeaponString).GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.Find(MainSpriteString).GetComponentInChildren<SpriteRenderer>();
        _weaponSR = GameObject.Find(WeaponPivotString);
        _weaponPos = transform.Find(WeaponString).GetComponent<Transform>();
        _healthSystem = GetComponentInChildren<HealthSystem>();
    }

    private void Awake()
    {
        if (_target == null) _target = GameObject.FindGameObjectWithTag(Tag.Player)?.transform;
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
            _speed = _stats.moveSpeed;
            _rigidbody.velocity = _move * _speed;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnEnable()
    {
        if (_healthSystem != null) _healthSystem.OnDeath += OnEnemyDeadEvent;
        
        ChangeState(EnemyState.Chase); // 상태 초기화
    }

    private void OnDisable()
    {
        _healthSystem.OnDeath -= OnEnemyDeadEvent;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_weaponPos.position, _stats.attackRange);
    }

    public void SetPool(IObjectPool<EnemyController> pool, EnemyType type)
    {
        _pool = pool;
        _enemyType = type;
    }

    private void ChangeState(EnemyState newState) // 적 상태 변경
    {
        _currentState = newState;
    }

    /// <summary>
    /// 풀에 적 반납
    /// </summary>
    public void ReturnToPool()
    {
        if (!gameObject.activeSelf) return; // 이미 비활성화된 상태에서 중복 반납 방지

        _pool.Release(this); // 스포너에서 SetPool을 통해 받아왔던 그 풀(_pool)에 자신을 반납
    }

    #region 적 상태

    private void Chase()
    {
        _enemyAnim.SetBool(AnimParams.IsRunning, true);
        _weaponAnim.SetBool(AnimParams.IsAttacking, false);

        if (_target != null) _move = (_target.position - transform.position).normalized;

        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = _target.position.x < transform.position.x;
            if (_weaponSR != null && _spriteRenderer.flipX)
            {
                _weaponSR.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (_weaponSR != null && !_spriteRenderer.flipX)
            {
                _weaponSR.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        // 공격 범위 안에 들어오면 공격 호출
        _sqrDistance = (_target.position - _weaponPos.position).sqrMagnitude;
        if (_sqrDistance <= _stats.attackRange * _stats.attackRange) ChangeState(EnemyState.Attack);
    }

    private void Attack()
    {
        _move = Vector2.zero;
        _enemyAnim.SetBool(AnimParams.IsRunning, false);
        _weaponAnim.SetBool(AnimParams.IsAttacking, true);
        _stateInfo = _weaponAnim.GetCurrentAnimatorStateInfo(0);

        if (_target == null) return;

        _currentDist = Vector2.Distance(_weaponPos.position, _target.position);
        // 공격범위 벗어났을 경우
        if (_currentDist > _stats.attackRange
            && (_stateInfo.IsName(NearAttackString) && _stateInfo.normalizedTime == 1.0f))
        {
            _weaponAnim.SetBool(AnimParams.IsAttacking, false);
            ChangeState(EnemyState.Chase);
            return;
        }
        else if (_currentDist < _stats.attackRange)
        {
            // 공격 딜레이 반영 애니메이션 실행 속도
            _weaponAnim.speed = _defalutAttackSpeed / _stats.attackDelay;
        }
    }

    private void Dead()
    {
        if (_pool != null) _pool.Release(this); // 죽으면 풀에 반납
        else Destroy(gameObject); // 풀이 없을 때를 대비한 안전 장치
    }

    private void OnEnemyDeadEvent() // 적이 죽은 후 이벤트
    {
        if (TopDownManager.Instance != null) TopDownManager.Instance.AddKillScore(_enemyType);

        ChangeState(EnemyState.Dead);
    }

    #endregion

}
