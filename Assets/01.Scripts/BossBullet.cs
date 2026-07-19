using UnityEngine;

public class BossBullet : Projectile
{
    private Vector2 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 5f;
        wait = new WaitForSeconds(lifeTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        lifeTime = 5f;
        speed = 5f;
    }

    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed;
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("bossBullet", this.gameObject);
    }
}