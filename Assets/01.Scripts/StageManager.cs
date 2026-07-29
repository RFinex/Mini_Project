using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("Trigger")]
    [SerializeField] private Transform exitMiniBoss;
    [SerializeField] private Transform trophyRoomPos;

    public Transform PlayerPos
    {
        get
        {
            return GameManager.instance != null ? GameManager.instance.PlayerPos : null;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }

    public void Init(Transform miniExit, Transform trophyRoom)
    {
        exitMiniBoss = miniExit;
        trophyRoomPos = trophyRoom;
    }

    public void EnterBoss(Transform enter)
    {
        PlayerPos.position = enter.position;
    }

    public void ExitBoss()
    {
        PlayerPos.position = exitMiniBoss.position;
    }

    public void EnterTrophyRoom()
    {
        PlayerPos.position = trophyRoomPos.position;
    }

    public void ExitTrophyRoom()
    {
        UIManager.instance.OnClearUI();
    }
}
