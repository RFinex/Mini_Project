using UnityEngine;

public class BossEnterTrigger : MonoBehaviour
{
    [SerializeField] private Transform bossEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            StageManager.instance.EnterBoss(bossEnter);
        }
    }
}
