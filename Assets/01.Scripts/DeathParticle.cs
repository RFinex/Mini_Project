using System.Collections;
using UnityEngine;

public class DeathParticle : MonoBehaviour, IPoolable
{
    [SerializeField] private float lifeDelay;
    private WaitForSeconds wait;

    private void Awake()
    {
        wait = new WaitForSeconds(lifeDelay);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(LifeDelay());
    }

    

    IEnumerator LifeDelay()
    {
        yield return wait;

        ReturnPool();
    }

    public void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("deathParticle", this.gameObject);
    }
}
