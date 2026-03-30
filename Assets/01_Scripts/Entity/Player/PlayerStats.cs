using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header ("기본 스탯")]
    public float health = 30f; // 체력
    public float damage = 5f; // 대미지
    public float speed = 1.0f; // 이동 속도

    [Header("자동 공격 설정")]
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public float attackRate = 0.5f; // 공격 속도
}
