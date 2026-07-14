using System.Collections;
using UnityEngine;

public class DeathParticle : MonoBehaviour, IPoolable
{
    float delay;
    WaitForSeconds wait;

    private void Awake()
    {
        delay = 3f;
        wait = new WaitForSeconds(delay);
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
