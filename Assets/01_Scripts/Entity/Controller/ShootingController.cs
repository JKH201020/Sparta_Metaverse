using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public PlayerStats stats;
    public Rigidbody2D Rigidbody { get; private set; }

    // 상태가 참조할 실시간 입력값들
    public Vector2 MoveInput { get; private set; }
    public Vector2 MouseScreenPos { get; private set; } // 마우스 화면 좌표

    private PlayerBaseState _currentState;
    public ShootingPlayingState PlayingState { get; private set; }
    public ShootingDeadState DeadState { get; private set; }

    
}
