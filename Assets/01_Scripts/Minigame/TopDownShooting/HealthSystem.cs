using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private PlayerStats _playerStats;

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public event Action OnDeath;
    public event Action<float> OnTakeDamage;

    private void OnEnable()
    {
        if (CompareTag(Tag.Enemy) && _enemyStats != null)
        {
            MaxHealth = _enemyStats.hp;
            CurrentHealth = _enemyStats.hp;
        }
        else if (CompareTag(Tag.Player) && _playerStats != null)
        {
            MaxHealth = _playerStats.hp;
            CurrentHealth = _playerStats.hp;
        }
    }

    /// <summary>
    /// 대미지 받기
    /// </summary>
    /// <param name="damage">받을 대미지</param>
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        OnTakeDamage?.Invoke(CurrentHealth);
    }

    /// <summary>
    /// 체력 초기화
    /// </summary>
    public void ResetHp()
    {
        CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// 죽음
    /// </summary>
    public void Die()
    {
        CurrentHealth = 0;
        OnDeath?.Invoke();
    }
}
