using UnityEngine;

public class EnemyBullet : Projectile
{
    private Vector2 dir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 5f;
        wait = new WaitForSeconds(lifeTime);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public override void ReturnPool()
    {
        
    }
}
