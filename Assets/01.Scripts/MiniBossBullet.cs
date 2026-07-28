using System.Collections;
using UnityEngine;

public class MiniBossBullet : Projectile
{
    private Vector2 dir;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wait = new WaitForSeconds(lifeTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        speed = baseSpeed;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dir * speed;
    }

    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject(ConstString.minibossBullet, this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.GetComponent<PlayerController>().TakeDamage();
            ReturnPool();
        }
    }

}
