using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header ("기본 스탯")]
    public float health = 30f; // 체력
    public float damage = 5f; // 대미지
    public float moveSpeed = 1.0f; // 이동 속도
    public float deathCooldown = 1f;

    [Header("자동 공격 설정")]
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public float attackRate = 1f; // 공격 빈도
    public float attackSpeed = 1f; // 투사체 속도
}
