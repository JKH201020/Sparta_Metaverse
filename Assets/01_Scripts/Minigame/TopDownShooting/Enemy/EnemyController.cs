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
    [SerializeField] private HealthSystem _healthSystem;

    private float _speed; // 적 이동속도
    private float _sqrDistance;
    private float _attackTimer = 0f; // 공격 쿨타임
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

        ChangeState(EnemyState.Chase); // 초기 상태
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) Idle();

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
        if (GameManager.Instance.CurrentState != GameState.Playing)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

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

        if (_weaponAnim != null)
        {
            _weaponAnim.Rebind(); // 애니메이션 기본 상태로 되돌림
            _weaponAnim.Update(0f); // 0프레임으로 업데이트
        }

        if (_enemyAnim != null)
        {
            _enemyAnim.Rebind(); // 애니메이션 기본 상태로 되돌림
            _enemyAnim.Update(0f); // 0프레임으로 업데이트
        }

        ChangeState(EnemyState.Chase); // 상태 초기화
        _attackTimer = _stats.attackDelay; // 첫 공격은 딜레이 없이 바로 할 수 있게 초기화
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

    private void Idle()
    {
        _enemyAnim.SetBool(AnimParams.IsRunning, false);
        _weaponAnim.SetBool(AnimParams.IsAttacking, false);
    }

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
        _move = Vector2.zero; // 공격 중에는 무조건 제자리 정지
        _enemyAnim.SetBool(AnimParams.IsRunning, false);

        if (_target == null) return;

        _stateInfo = _weaponAnim.GetCurrentAnimatorStateInfo(0);

        // 애니메이션 재생이 100% 완료되었는지 확인
        // 애니메이션 끝나는 것보다 스크립트가 늦게 실행되기 때문에 1f로 하면 애니메이션은 이미 끝난 상태로 코드가 실행되 2번 때리는 문제가 있음. 
        if (_stateInfo.IsName(NearAttackString) && _stateInfo.normalizedTime >= 0.8f) _weaponAnim.SetBool(AnimParams.IsAttacking, false);

        bool isAttackingNow = _weaponAnim.GetBool(AnimParams.IsAttacking) ||
                            (_stateInfo.IsName(NearAttackString) && _stateInfo.normalizedTime < 1.0f);
        if (isAttackingNow) return; // 공격 모션 재생중이면 아래 로직 실행 안 함

        _currentDist = Vector2.Distance(_weaponPos.position, _target.position);

        // 공격 사거리 벗어난 경우 재추적
        if (_currentDist > _stats.attackRange)
        {
            ChangeState(EnemyState.Chase);
            return;
        }

        _attackTimer += Time.deltaTime;

        // 쿨타임 다 돌면 다시 공격 애니메이션 시작
        if (_attackTimer >= _stats.attackDelay)
        {
            _weaponAnim.SetBool(AnimParams.IsAttacking, true);
            _attackTimer = 0f; // 타이머 초기화
        }
    }

    private void Dead()
    {
        _enemyAnim.SetBool(AnimParams.IsRunning, false);
        _weaponAnim.SetBool(AnimParams.IsAttacking, false);

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
