using UnityEngine;

public class PlayerBullet : Projectile
{
    [SerializeField] private float dir;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wait = new WaitForSeconds(lifeTime);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        speed = baseSpeed;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Ground))
        {
            ReturnPool();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            SoundManager.instance.PlaySFX(SFXType.EnemyHit);
            collision.GetComponent<EnemyController>().TakeDamage();
            ReturnPool();
        }
    }
}
