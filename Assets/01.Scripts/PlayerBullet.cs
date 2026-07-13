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
        base.OnEnable();
    }

    void Start()
    {
        
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("playerBullet", this.gameObject);
    }
}
