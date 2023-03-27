using UnityEngine;

public class Skeleton : Enemy
{
    /// <summary>
    /// Use for any Initialization
    /// </summary>
    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public override void Movement()
    {
        base.Movement();
    }

    public override void Damage(int damageAmount)
    {
        base.Damage(damageAmount);
    }

    protected override void FaceAttacker()
    {
        base.FaceAttacker();
    }
}
