using UnityEngine;

public class BossBullet : Projectile
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wait = new WaitForSeconds(lifeTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        speed = baseSpeed;
        transform.rotation = Quaternion.identity;
    }

    //public void SetDirection(Quaternion rotate)
    //{
    //    transform.rotation = rotate;
    //}

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed;
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("bossBullet", this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.GetComponent<PlayerController>().TakeDamage();
        }
    }
}