using UnityEngine;

public abstract class PlayerBaseState
{
    protected ShootingController controller;
    public PlayerBaseState(ShootingController controller)
    {
        this.controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Tick() { }
    public virtual void PhysicsTick() { }
}

public class ShootingPlayingState : PlayerBaseState
{
    private float _attackTimer;

    public ShootingPlayingState(ShootingController controller) : base(controller) { }

    public override void Enter()
    {
        _attackTimer = 0f;
    }

    public override void Tick()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= controller.stats.attackRate)
        {

            Shoot();
            _attackTimer = 0f;
        }
    }

    public override void PhysicsTick()
    {
        // 이동 속도
        controller.Rigidbody.velocity = controller.MoveInput * controller.stats.speed;
        // 마우스 방향으로 조준
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(controller.MouseScreenPos);
        Vector2 lookDir = worldMousePos - (Vector2)controller.transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        controller.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Shoot() // 투사체 발사
    {
        if (controller.stats.bulletPrefab != null)
        {
            Object.Instantiate(controller.stats.bulletPrefab, controller.transform.position, controller.transform.rotation);
        }
    }
}

public class ShootingDeadState : PlayerBaseState
{
    private float _cooldownTimer;

    public ShootingDeadState(ShootingController controller) : base(controller) { }

    public override void Enter()
    {
        controller.Rigidbody.velocity = Vector2.zero;
        _cooldownTimer = controller.stats.deathCooldown;
    }

    public override void Tick()
    {
        if (_cooldownTimer <= 0)
        {
            // TODO: 씬 재시작 등 게임 오버 처리
        }
        else
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }
}