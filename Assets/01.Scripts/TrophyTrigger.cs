using UnityEngine;

public class TrophyTrigger : MonoBehaviour
{
    [SerializeField] private int trophyId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            DataManager.instance.GetTrophy(trophyId);
            enabled = false;
        }
    }
}
