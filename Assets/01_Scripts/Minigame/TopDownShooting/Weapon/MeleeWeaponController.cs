using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private Collider2D _collider;
    
    private HealthSystem _playerHealth;

    private void Reset()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // 적 무기에 닿았을 경우 플레이어가 대미지 받음
    {
        if (collision.gameObject.CompareTag(Tag.Player))
        {
            _playerHealth = collision.GetComponent<HealthSystem>();

            if (_enemyStats != null) _playerHealth.TakeDamage(_enemyStats.damage);
        }
    }

    #region 공격 애니메이션 콜라이더 온오프

    /// <summary>
    /// 공격 애니메이션 마지막에 콜라이더 켜기
    /// (마지막 모션에만 대미지가 들어가게 하기 위함)
    /// </summary>
    public void EnableAttackCollider()
    {
        _collider.enabled = true;
    }

    /// <summary>
    /// 공격 애니메이션 콜라이더 끄기
    /// </summary>
    public void DisableAttackCollider()
    {
        _collider.enabled = false;
    }

    #endregion

}
