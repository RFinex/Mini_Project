using UnityEngine;

public class PlayerBullet : Projectile
{

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 1f;
    }

    protected override void OnEnable()
    {
        lifeTime = 1f;
        speed = 10f;
        base.OnEnable();
    }

    protected void FixedUpdate()
    {
        rb.linearVelocity = Vector3.right * speed * dir;
    }

    public override void SetDamage(int damage)
    {
        base.SetDamage(damage);
    }

    public override void SetDirection(float dir)
    {
        base.SetDirection(dir);
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("playerBullet", this.gameObject);
    }
}
