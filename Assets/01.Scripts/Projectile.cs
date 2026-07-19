using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public abstract class Projectile : MonoBehaviour, IPoolable
{
    protected float speed;
    [SerializeField] protected float lifeTime;

    protected int damage;


    protected Rigidbody2D rb;

    protected WaitForSeconds wait;

    protected virtual void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(LifeDelay());
        SceneManager.sceneLoaded += BulletReturn;
    }

    protected void OnDisable()
    {
        SceneManager.sceneLoaded -= BulletReturn;
    }

    protected virtual void BulletReturn(Scene scene, LoadSceneMode mode)
    {
        ReturnPool();
    }

    protected IEnumerator LifeDelay()
    {
        yield return wait;

        ReturnPool();
    }

    public virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public abstract void ReturnPool();
}
