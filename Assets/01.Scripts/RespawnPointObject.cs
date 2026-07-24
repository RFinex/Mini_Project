using UnityEngine;

public class RespawnPointObject : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DataManager.instance.SetCheckPos(transform.position);
            GameManager.instance.SaveGame();
            UIManager.instance.SaveTextOn(transform.position);
        }
    }
}
