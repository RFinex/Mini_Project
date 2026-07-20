using System.Collections;
using UnityEngine;

public class MiniBossBullet : Projectile
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
        speed = 12f;
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
        ObjectPoolManager.instance.ReturnObject("minibossBullet", this.gameObject);
    }

    
}
