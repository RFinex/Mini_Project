using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour
{
    protected float delay = 5f;
    protected WaitForSeconds wait;
    protected Collider2D col;
    protected SpriteRenderer sr;

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected IEnumerator ItemRespawn()
    {
        col.enabled = false;
        sr.color = new Color(1, 1, 1, 0.2f);

        yield return wait;

        col.enabled = true;
        sr.color = new Color(1, 1, 1, 1);
    }
}
