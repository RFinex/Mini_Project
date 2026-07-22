using UnityEngine;

public class MiniBossEnterTrigger : MonoBehaviour
{
    [SerializeField] private Transform miniBossEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            StageManager.instance.EnterBoss(miniBossEnter);
        }
    }
}
