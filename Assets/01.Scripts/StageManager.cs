using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public Rect minibossMoveArea;
    public List<Rect> minibossLaserArea;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void EnterBoss(Transform enter, GameObject player)
    {
        player.transform.position = enter.position;
    }

    private void OnDrawGizmos()
    {
        MoveAreaGizmos();
        LaserAreaGizmos();
    }

    private void MoveAreaGizmos()
    {
        if (minibossMoveArea == null)
            return;

        Gizmos.color = new Color(1f, 1f, 0f, 0.5f);

        Vector3 center = new Vector3(minibossMoveArea.x + minibossMoveArea.width / 2, minibossMoveArea.y + minibossMoveArea.height / 2);
        Vector3 size = new Vector3(minibossMoveArea.width, minibossMoveArea.height);

        Gizmos.DrawCube(center, size);
    }

    private void LaserAreaGizmos()
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

    
}
