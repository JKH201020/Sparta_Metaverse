using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private PlayerStats _playerStats;

    private float _currentHealth;
    private float _MaxHealth;

    public event Action OnDeath;

    private void OnEnable()
    {
        if (CompareTag(Tag.Enemy) && _enemyStats != null)
        {
            _MaxHealth = _enemyStats.hp;
            _currentHealth = _enemyStats.hp;
        }
        else if (CompareTag(Tag.Player) && _playerStats != null)
        {
            _MaxHealth = _playerStats.hp;
            _currentHealth = _playerStats.hp;
        }
    }

    /// <summary>
    /// 대미지 받기
    /// </summary>
    /// <param name="damage">받을 대미지</param>
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log($"{gameObject.name}가 {damage}의 피해를 받음");

        if (_currentHealth <= 0) Die();
    }

    /// <summary>
    /// 체력 초기화
    /// </summary>
    public void ResetHp()
    {
        _currentHealth = _MaxHealth;
    }

    private void Die() // 죽음
    {
        _currentHealth = 0;
        OnDeath?.Invoke();
    }
}
