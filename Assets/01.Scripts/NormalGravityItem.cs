using UnityEngine;
using System.Collections;

public class NormalGravityItem : Item
{
    private bool isAnti = false;

    private void Awake()
    {
        wait = new WaitForSeconds(delay);
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerController>().AntiGravity(isAnti);
            StartCoroutine(ItemRespawn());
        }
    }

    protected override IEnumerator ItemRespawn()
    {
        col.enabled = false;
        sr.color = new Color(1, 0, 0, 0.2f);

        yield return wait;

        col.enabled = true;
        sr.color = new Color(1, 0, 0, 1);
    }
}
