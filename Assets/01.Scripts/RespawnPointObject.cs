using UnityEngine;

public class RespawnPointObject : MonoBehaviour
{
    private GameObject respawnPoint;

    private void Awake()
    {
        respawnPoint = GameObject.Find("RespawnPoint");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            respawnPoint.transform.position = transform.position;
            
        }
    }
}
