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
}