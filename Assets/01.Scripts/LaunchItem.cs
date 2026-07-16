using UnityEngine;

public class LaunchItem : Item
{
    [SerializeField] private GameObject dirGuide;

    private void Awake()
    {
        wait = new WaitForSeconds(delay);
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        dirGuide = transform.GetChild(0).gameObject;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerController>().LaunchPlayer(transform.position);
            dirGuide.SetActive(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            dirGuide.SetActive(false);
            StartCoroutine(ItemRespawn());
        }
    }
}
