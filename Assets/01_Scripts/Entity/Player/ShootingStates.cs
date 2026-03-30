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

        if (controller.MouseScreenPos != Vector2.zero && controller.CharacterRenderer != null)
        {
            // 마우스 방향으로 조준
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(controller.MouseScreenPos);

            // 마우스X가 캐릭터X보다 작으면 -> 마우스가 왼쪽에 있음 -> FlipX = true
            bool shouldFilp = worldMousePos.x < controller.transform.position.x;
            controller.CharacterRenderer.flipX = shouldFilp;
        }
    }

    private void Shoot() // 투사체 발사
    {
        if (controller.stats.bulletPrefab != null)
        {
            // 캐릭터 위치에서 총알 생성 (회전값 없이 기본값 Quaternion.identity 사용)
            GameObject bullet = Object.Instantiate(controller.stats.bulletPrefab, controller.transform.position, Quaternion.identity);

            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(controller.MouseScreenPos);
            Vector2 fireDir = worldMousePos - (Vector2)controller.transform.position.normalized;

            //Bullet bulletScript = bullet.GetComponent<Bullet>();
            //if (bulletScript != null)
            //{
            //    bulletScript.SetDirection(fireDir);
            //}
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