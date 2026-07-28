using System.Collections.Generic;
using UnityEngine;

public class RectArea : MonoBehaviour
{
    public static RectArea instance;

    [Header("Rect Area")]
    public Rect minibossMoveArea;
    public List<Rect> bossMoveArea;
    public List<Rect> minibossLaserArea;
    public List<Rect> bossLaserArea;

    [Header("Gizmos Color")]
    [SerializeField] private Color moveAreaColor = new Color(1f, 1f, 0f, 0.5f);
    [SerializeField] private Color laserAreaColor = new Color(1f, 0f, 0f, 0.5f);

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = moveAreaColor;
        DrawRectGizmos(minibossMoveArea);
        DrawRectListGizmos(bossMoveArea);

        Gizmos.color = laserAreaColor;
        DrawRectListGizmos(minibossLaserArea);
        DrawRectListGizmos(bossLaserArea);
    }

    private void DrawRectGizmos(Rect rect)
    {
        Vector3 center = new Vector3(rect.x + rect.width / 2, rect.y + rect.height / 2);
        Vector3 size = new Vector3(rect.width, rect.height);
        Gizmos.DrawCube(center, size);
    }

    private void DrawRectListGizmos(List<Rect> rects)
    {
        foreach (var area in rects)
        {
            DrawRectGizmos(area);
        }
    }
}
