using UnityEngine;

public abstract class PlayerBaseState
{
    protected ShootingPlayerController controller;
    public PlayerBaseState(ShootingPlayerController controller)
    {
        this.controller = controller;
    }

    public virtual void Enter() { } // 플레이어 상태 변화될 때 한 번만 호출
    public virtual void Tick() { } // Update에 사용될 메서드
    public virtual void PhysicsTick() { } // FixedUpdate에 사용될 메서드
}

public class ShootingPlayingState : PlayerBaseState
{
    public ShootingPlayingState(ShootingPlayerController controller) : base(controller) { }

    private bool _isLeft;

    public override void Enter()
    {
       
    }

    public override void Tick()
    {

    }

    public override void PhysicsTick()
    {
        // 플레이어 이동 속도
        controller.Rigidbody.velocity = controller.MoveInput * controller.stats.moveSpeed;

        if (controller.MouseScreenPos != Vector2.zero)
        {
            // 마우스 방향으로 조준
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(controller.MouseScreenPos);

            // 마우스X가 캐릭터X보다 작으면 -> 마우스가 왼쪽에 있음 -> FlipX = true
            _isLeft = worldMousePos.x < controller.transform.position.x;
            controller.CharacterRenderer.flipX = _isLeft;
        }
    }
}

public class ShootingDeadState : PlayerBaseState
{
    public ShootingDeadState(ShootingPlayerController controller) : base(controller) { }

    /// <summary>
    /// 사망상태 진입
    /// </summary>
    public override void Enter()
    {
        TopDownManager.Instance.GameOver();
    }

    public override void Tick()
    {
        
    }
}