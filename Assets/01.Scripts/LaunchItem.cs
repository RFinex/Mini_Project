using UnityEngine;

public class LaunchItem : Item
{
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
            collision.GetComponent<PlayerController>().LaunchPlayer(transform.position);
            StartCoroutine(ItemRespawn());
        }
    }
}
