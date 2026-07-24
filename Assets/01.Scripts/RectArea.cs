using System.Collections.Generic;
using UnityEngine;

public class RectArea : MonoBehaviour
{
    public static RectArea instance;

    public Rect minibossMoveArea;
    public List<Rect> bossMoveArea;
    public List<Rect> minibossLaserArea;
    public List<Rect> bossLaserArea;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        MiniBossMoveAreaGizmos();
        BossMoveAreaGizmos();
        MiniBossLaserAreaGizmos();
        BossLaserAreaGizmos();
    }

    private void MiniBossMoveAreaGizmos()
    {
        if (minibossMoveArea == null)
            return;

        Gizmos.color = new Color(1f, 1f, 0f, 0.5f);

        Vector3 center = new Vector3(minibossMoveArea.x + minibossMoveArea.width / 2, minibossMoveArea.y + minibossMoveArea.height / 2);
        Vector3 size = new Vector3(minibossMoveArea.width, minibossMoveArea.height);

        Gizmos.DrawCube(center, size);
    }

    private void BossMoveAreaGizmos()
    {
        if (bossMoveArea == null)
            return;

        Gizmos.color = new Color(1f, 1f, 0f, 0.5f);

        foreach (var area in bossMoveArea)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    private void MiniBossLaserAreaGizmos()
    {
        if (minibossLaserArea == null)
            return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        foreach (var area in minibossLaserArea)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }
    private void BossLaserAreaGizmos()
    {
        if (bossLaserArea == null)
            return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        foreach (var area in bossLaserArea)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }
}
