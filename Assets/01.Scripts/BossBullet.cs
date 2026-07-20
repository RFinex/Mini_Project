using UnityEngine;

public class BossBullet : Projectile
{
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
        speed = 8f;
    }

    public void SetDirection(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
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