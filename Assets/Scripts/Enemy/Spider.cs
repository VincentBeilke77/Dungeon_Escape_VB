using UnityEngine;

public class Spider : Enemy
{
    public GameObject acidEffectPrefab;

    /// <summary>
    /// Use for any Initialization
    /// </summary>
    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public override void Update()
    {

    }

    public override void Movement()
    {

    }

    public void Attack()
    {
        // instantiate acid effect
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
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
