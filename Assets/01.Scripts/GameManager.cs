using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject player;
    private PlayerController pc;

    private bool isStart = false;

    [SerializeField] private float speedrunTimeLimit;
    [SerializeField] private int clearTrophyId;
    [SerializeField] private int speedRunTrophyId;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // 씬 로드 마다 초기화
    // Init은 가능하면 리팩토링 할 예정
    public void Init()
    {
        player = GameObject.Find(ConstString.Player);

        if (player != null)
        {
            pc = player.GetComponent<PlayerController>();
            if (SaveLoadManager.instance.SaveFileCheck())
            {
                player.transform.position = DataManager.instance.CheckPos;
            }
        }

        EffectManager.instance.Init();
        UIManager.instance.Init();
        StageManager.instance.Init();
    }

    public void Init_Menu()
    {
        UIManager.instance.Init_Menu();
    }

    public void StartGame()
    {
        SceneLoadManager.instance.ChangeScene(ConstString.Stage1_Scene);
    }

    // 게임 시작 체크
    public void SetStartGame(bool start)
    {
        Debug.Log("Start활성화");
        isStart = start;
    }
    
    public void SaveGame()
    {
        SaveLoadManager.instance.Save();
    }

    public void LoadGame()
    {
        SaveLoadManager.instance.Load();
        SceneLoadManager.instance.ChangeScene(ConstString.Stage1_Scene);
    }

    private void Update()
    {
        if (isStart)
        {
            TimerOn();

            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                RestartScene();
            }
        }
    }


    // 타이머는 DataManager에 바로 저장
    private void TimerOn()
    {
        DataManager.instance.UpdatePlayTime(Time.deltaTime);
        UIManager.instance.UpdateTimerText(DataManager.instance.PlayTime);
    }

    private void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneLoadManager.instance.ChangeScene(scene.name);
    }

    public void GameClear()
    {
        if (DataManager.instance.PlayTime >= speedrunTimeLimit)
        {
            DataManager.instance.GetTrophy(speedRunTrophyId);
            UIManager.instance.OpenTrophyPanel(speedRunTrophyId);
        }
        DataManager.instance.GetTrophy(clearTrophyId);
        UIManager.instance.OpenTrophyPanel(clearTrophyId);
        Time.timeScale = 0f;
        UIManager.instance.OnClearUI();
    }

    public void RestartAfterClear()
    {
        Time.timeScale = 1f;
        SaveLoadManager.instance.ResetSave();
        RestartScene();
    }
}
