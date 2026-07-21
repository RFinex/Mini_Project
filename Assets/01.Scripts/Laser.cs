using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour, IPoolable
{
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(LaserDelay());
    }

    private IEnumerator LaserDelay()
    {
        yield return new WaitForSeconds(1f + (20f / 60f));

        col.enabled = true;

        yield return new WaitForSeconds((50f / 60f));

        col.enabled = false;
    }

    public void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("laser", this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.GetComponent<PlayerController>().CollisionObject();
        }
    }
}
