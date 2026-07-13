using UnityEngine;

public class PlayerBullet : Projectile
{

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("playerBullet", this.gameObject)
    }
}
