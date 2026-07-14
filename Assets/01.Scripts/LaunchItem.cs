using UnityEngine;

public class LaunchItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerController>().LaunchPlayer(transform.position);
        }
    }
}
