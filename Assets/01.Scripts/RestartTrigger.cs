using UnityEngine;

public class RestartTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            GameManager.instance.GameClear();
        }
    }
}
