using System.Collections;
using UnityEngine;

public class JumpItem : MonoBehaviour
{
    private float delay;
    private WaitForSeconds wait;
    private Collider2D col;
    private SpriteRenderer sr;

    private void Awake()
    {
        delay = 5f;
        wait = new WaitForSeconds(delay);
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerController>().AddJump();
            StartCoroutine(ItemRespawn());
        }
    }

    private IEnumerator ItemRespawn()
    {
        col.enabled = false;
        sr.color = new Color(1, 1, 1, 0.2f);

        yield return wait;

        col.enabled = true;
        sr.color = new Color(1, 1, 1, 1);
    }
}
