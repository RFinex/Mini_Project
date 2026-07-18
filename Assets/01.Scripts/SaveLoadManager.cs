using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;
    [SerializeField] private GameData data;

    private string fileName;
    private string savePath;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        data = new GameData();
        fileName = "gameData.json";
        savePath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void Save()
    {
        data.CheckPointX = DataManager.instance.checkPos.x;
        data.CheckPointY = DataManager.instance.checkPos.y;
        data.CheckPointZ = DataManager.instance.checkPos.z;
        data.elapsedTime = DataManager.instance.playTime;


        foreach (Trophy trophy in DataManager.instance.GetTrophyInfo())
        {
            if (trophy.isCollect)
            {
                data.collectTrophy.Add(trophy.id);
            }
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(savePath, json);

        Debug.Log(savePath);
    }

    public void Load()
    {
        if (File.Exists(savePath) == false)
        {
            Debug.Log("세이브 없음");
            return;
        }

        string json = File.ReadAllText(savePath);

        GameData loadData = JsonUtility.FromJson<GameData>(json);

        DataManager.instance.checkPos = new Vector3(loadData.CheckPointX, loadData.CheckPointY, loadData.CheckPointZ);
        DataManager.instance.playTime = loadData.elapsedTime;
    }

    public bool SavePath()
    {
        return File.Exists(savePath);
    }
}
