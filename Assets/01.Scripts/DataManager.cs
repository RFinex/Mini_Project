using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private Dictionary<int, Trophy> trophys = new Dictionary<int, Trophy>();

    [SerializeField] private TrophyData trophyData;

    public Vector3 checkPos;
    public float playTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        LoadTrophyData();
    }

    private void LoadTrophyData()
    {
        for (int i = 0; i < trophyData.trophy.Count; i++)
        {
            Trophy trophy = trophyData.trophy[i].Clone();

            trophys[trophy.id] = trophy;
        }
    }

    public Trophy GetTrophyData(int id)
    {
        return trophys.GetValueOrDefault(id);
    }

    // 트로피 정보 외부 참조용
    public List<Trophy> GetTrophyInfo()
    {
        return new List<Trophy>(trophys.Values);
    }
}
