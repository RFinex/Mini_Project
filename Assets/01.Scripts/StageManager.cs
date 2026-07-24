using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] private Transform exitMiniBoss;

    private GameObject player;
    public Transform PlayerPos
    {
        get
        {
            return player != null ? player.transform : null;
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

    public void Init()
    {
        player = GameObject.Find(ConstString.Player);
        exitMiniBoss = GameObject.Find("MiniBossExitTarget").transform;
    }

    public void EnterBoss(Transform enter)
    {
        player.transform.position = enter.position;
    }

    public void ExitBoss()
    {
        player.transform.position = exitMiniBoss.position;
    }
}
