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
    private Vector2 _mousePos;

    public ShootingPlayingState(ShootingController controller) : base(controller) { }

    public override void Tick()
    {
        
    }

    public override void PhysicsTick()
    {
        
    }
}

public class ShootingDeadState : PlayerBaseState
{
    private float _timer;

    public ShootingDeadState(ShootingController controller) : base(controller) { }

    public override void Enter() { }

    public override void Tick() { }
}