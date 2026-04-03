using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Header ("적 스탯")]
    public float hp = 10f; // 체력
    public float moveSpeed = 3f; // 이동속도
    public float attackRate = 2f; // 공격속도
}
