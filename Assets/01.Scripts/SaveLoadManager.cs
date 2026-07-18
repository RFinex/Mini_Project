using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private GameData data;

    private string fileName;
    private string savePath;

    private void Start()
    {
        data = new GameData();
        fileName = "gameData.json";
        savePath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void Save()
    {
        data.CheckPointX = DataManager.instance.playerPos.x;
        data.CheckPointY = DataManager.instance.playerPos.y;
        data.CheckPointZ = DataManager.instance.playerPos.z;
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

    }
}
