using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Header ("적 스탯")]
    public float hp = 10f; // 체력
    public float damage = 3f; // 대미지
    public float moveSpeed = 3f; // 이동속도
    public float attackDelay = 2f; // 공격속도 (attackDelay초 마다 1번 공격)
    public float attackRange = 1f; // 공격범위
}
