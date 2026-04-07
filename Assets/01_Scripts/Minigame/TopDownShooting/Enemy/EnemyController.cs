using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
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
    [SerializeField] private GameObject _weaponSR;
    [SerializeField] private Animator _weaponAnim;
    [SerializeField] private Transform _weaponPos;

    [Header("스탯")]
    [SerializeField] private EnemyStats _stats;
    [SerializeField] private HealthSystem _healthSystem;

    private float _speed; // 적 이동속도
    private float sqrDistance;
    private float _defalutAttackSpeed = 1.0f;
    private float _currentDist;

    private EnemyState _currentState;
    private Transform _target; // 플레이어 위치
    private Vector2 _move; // 적 이동방향
    private AnimatorStateInfo _stateInfo; // 현재 실행 중인 애니메이션

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
        _healthSystem.OnDeath += OnEnemyDeadEvent;
        _healthSystem.ResetHp();

        // 이 부분 스포너든지 어떻게든 수정해보기. 계속 활성화할 때마다 부하걸림
        // 그리고 몬스터 체력 복구 시키기
        if (_target == null) _target = GameObject.FindGameObjectWithTag(Tag.Player)?.transform;

        ChangeState(EnemyState.Chase);
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
        sqrDistance = (_target.position - _weaponPos.position).sqrMagnitude;
        if (sqrDistance <= _stats.attackRange * _stats.attackRange) ChangeState(EnemyState.Attack);
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
        gameObject.SetActive(false);
    }

    private void OnEnemyDeadEvent()
    {
        ChangeState(EnemyState.Dead);
    }

    #endregion
}
