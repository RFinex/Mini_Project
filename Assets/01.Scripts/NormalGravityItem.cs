using UnityEngine;

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
}
