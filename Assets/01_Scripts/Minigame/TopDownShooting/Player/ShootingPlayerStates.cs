using UnityEngine;

public abstract class PlayerBaseState
{
    protected ShootingPlayerController controller;
    public PlayerBaseState(ShootingPlayerController controller)
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

    public ShootingPlayingState(ShootingPlayerController controller) : base(controller) { }

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
        // 플레이어 이동 속도
        controller.Rigidbody.velocity = controller.MoveInput * controller.stats.moveSpeed;

        if (controller.MouseScreenPos != Vector2.zero)
        {
            // 마우스 방향으로 조준
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(controller.MouseScreenPos);

            // 마우스X가 캐릭터X보다 작으면 -> 마우스가 왼쪽에 있음 -> FlipX = true
            bool isLeft = worldMousePos.x < controller.transform.position.x;
            controller.CharacterRenderer.flipX = isLeft;
        }
    }

    private void Shoot() // 투사체 발사
    {
        if (controller.stats.bulletPrefab != null)
        {
            // 마우스의 월드 좌표 계산
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(controller.MouseScreenPos);
            // 발사 방향 계산
            Vector2 fireDir = (worldMousePos - (Vector2)controller.transform.position).normalized;

            // 직접 생성(Instantiate)하지 않고, 컨트롤러의 풀에서 빌려옴 / 위치, 회전(기본), 방향을 인자로 전달
            controller.FireBullet(controller.transform.position, Quaternion.identity, fireDir);
        }
    }
}

public class ShootingDeadState : PlayerBaseState
{
    private float _cooldownTimer;

    public ShootingDeadState(ShootingPlayerController controller) : base(controller) { }

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