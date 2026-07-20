using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected int maxHp;
    protected int nowHp;
    protected float speed;

    protected SpriteRenderer sr;
    protected Transform target;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        CheckFlip();
    }

    protected virtual void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
    }
}
