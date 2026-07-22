using UnityEngine;
using System.Collections;

public class AntiGravityItem : Item
{
    private bool isAnti = true;

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
        }
    }
}
