using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    
    private HealthSystem _playerHealth;

    private void OnTriggerEnter2D(Collider2D collision) // 적 무기에 닿았을 경우 플레이어가 대미지 받음
    {
        if (collision.gameObject.CompareTag(Tag.Player))
        {
            _playerHealth = collision.GetComponent<HealthSystem>();

            if (_enemyStats != null) _playerHealth.TakeDamage(_enemyStats.damage);
        }
    }
}
