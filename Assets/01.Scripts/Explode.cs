using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Explode : MonoBehaviour, IPoolable
{
    private float delay;
    private WaitForSeconds wait;

    private void Awake()
    {
        delay = (24f / 60f);
        wait = new WaitForSeconds(delay);
    }
    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(ReturnDelay());
    }

    private IEnumerator ReturnDelay()
    {
        yield return wait;

        ReturnPool();
    }

    public void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("explode", this.gameObject);
    }


}
