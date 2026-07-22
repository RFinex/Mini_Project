using UnityEngine;

public class MiniBossEnterTrigger : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.transform.position = target.position;
        }
    }
}
