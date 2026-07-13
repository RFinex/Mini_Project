using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour, IPoolable
{
    protected float speed;
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float timer;

    protected int damage;

    protected float dir;

    protected Rigidbody2D rb;

    protected void Awake()
    {
        lifeTime = 3f;
        timer = 0f;
    }

    protected void OnEnable()
    {
        lifeTime = 3f;
        timer = 0f;
        StartCoroutine(LifeDelay());
    }

    void Start()
    {
        StartCoroutine(LifeDelay());
    }

    protected IEnumerator LifeDelay()
    {
        while (true)
        {
            if (timer >= lifeTime)
            {
                ReturnPool();
            }
            yield return null;
        }
    }

    protected void Update()
    {
        timer += Time.deltaTime;
    }

    public abstract void ReturnPool();
}
