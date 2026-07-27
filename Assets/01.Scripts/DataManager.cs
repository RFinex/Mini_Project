using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private Dictionary<int, Trophy> trophys = new Dictionary<int, Trophy>();

    [SerializeField] private TrophyData trophyData;

    private Vector3 checkPos;

    public Vector3 CheckPos
    {
        get
        {
            return checkPos;
        }
        private set
        {
            checkPos = value;
        }
    }

    private float playTime;

    public float PlayTime
    {
        get
        {
            return playTime;
        }
        private set
        {
            playTime = value;
        }
    }

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

    public void SetCollectTrophy(List<int> trophyID)
    {
        if (trophyID == null)
            return;

        foreach (int id in trophyID)
        {
            if (trophys.ContainsKey(id))
            {
                trophys[id].isCollect = true;
            }
        }
    }

    public void SetCheckPos(Vector3 position)
    {
        checkPos = position;
    }

    public void SetPlayTime(float time)
    {
        playTime = time;
    }

    public void UpdatePlayTime(float time)
    {
        playTime += time;
    }
    
    // 트로피 데이터를 제외한 모든 데이터를 기본값으로 초기화
    public void ResetDataKeepTrophy()
    {
        CheckPos = Vector3.zero;
        PlayTime = 0f;
    }

    // 트로피 획득 시 불러옴
    public void GetTrophy(int id)
    {
        if (trophys.ContainsKey(id))
        {
            if (!trophys[id].isCollect)
            {
                trophys[id].isCollect = true;
            }
        }
    }
}
