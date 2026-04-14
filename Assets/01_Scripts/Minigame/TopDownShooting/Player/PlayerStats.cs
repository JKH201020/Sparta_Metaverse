using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header ("기본 스탯")]
    public float hp = 30.0f; // 체력
    public float defaultHp = 30.0f; // 초기화용 체력
    public float damage = 5.0f; // 대미지
    public float moveSpeed = 1.0f; // 이동 속도
    public float deathCooldown = 1.0f;

    [Header("자동 공격 설정")]
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public float attackDelay = 1.0f; // 공격 빈도
    public float bulletSpeed = 1.0f; // 투사체 속도
}
