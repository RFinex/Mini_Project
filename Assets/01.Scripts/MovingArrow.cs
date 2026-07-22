using UnityEngine;
using DG.Tweening;

public class MovingArrow : MonoBehaviour
{
    [SerializeField] private Vector3 dir;

    void Start()
    {
        transform.DOMove(dir, 1f).SetRelative()
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject)
            .SetEase(Ease.InOutCubic);
    }
}
