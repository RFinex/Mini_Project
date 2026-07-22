using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    // 나중에 자동으로 그리게 리팩토링 해볼 예정
    public Rect minibossMoveArea;
    public List<Rect> minibossLaserArea;

    [SerializeField] private Transform exitBoss;

    private GameObject player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.Find(ConstString.Player);
    }

    public void EnterBoss(Transform enter)
    {
        player.transform.position = enter.position;
    }

    public void ExitBoss()
    {
        player.transform.position = exitBoss.position;
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
