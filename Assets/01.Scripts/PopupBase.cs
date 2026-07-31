using DG.Tweening;
using UnityEngine;

public class PopupBase : MonoBehaviour
{
    [Header("Popup Tween Data")]
    [SerializeField] protected PopupTweenDataSO data;

    protected Sequence seq;

    protected void OnEnable()
    {
        transform.localScale = Vector3.one * data.closeSize;
    }
}
