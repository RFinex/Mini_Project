using UnityEngine;

public class PlayerBullet : Projectile
{
    private float dir;

    protected void Awake()
    {
        dir = 1f;
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 3f;
        wait = new WaitForSeconds(lifeTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        lifeTime = 3f;
        speed = 15f;
    }

    protected void FixedUpdate()
    {
        rb.linearVelocity = Vector3.right * speed * dir;
    }
    

    //private void OnBecameInvisible()
    //{     
    //    Debug.Log($"{this.gameObject.name}/visible");
    //    ReturnPool();
    //}

    public void SetDirection(float dir)
    {
        this.dir = dir;
    }

    public override void ReturnPool()
    {        
        ObjectPoolManager.instance.ReturnObject("playerBullet", this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ReturnPool();
        }
    }
}
