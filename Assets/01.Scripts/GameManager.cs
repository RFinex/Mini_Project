using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject player;
    public GameObject Player
    {
        get
        {
            return player;
        }
    }
    public Transform PlayerPos
    {
        get
        {
            return player != null ? player.transform : null;
        }
    }

    private PlayerController pc;

    private bool isStart = false;

    [Header("Speed run Trophy Info")]
    [SerializeField] private float speedRunTimeLimit;
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
    public void Init()
    {
        if (player != null)
        {
            pc = player.GetComponent<PlayerController>();
            if (SaveLoadManager.instance.SaveFileCheck())
            {
                player.transform.position = DataManager.instance.CheckPos;
            }
        }
    }

    public void PlayerInit(GameObject player)
    {
        this.player = player;
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

    public void BossClear()
    {
        SoundManager.instance.PlayBGM(BGMType.Game);

        if (DataManager.instance.PlayTime <= speedRunTimeLimit)
        {
            DataManager.instance.GetTrophy(speedRunTrophyId);
        }
        StageManager.instance.EnterTrophyRoom();
    }

    public void GameOver()
    {
        SoundManager.instance.PlayBGM(BGMType.Die);
        UIManager.instance.OnGameOverText();
    }

    public void GameClear()
    {
        Time.timeScale = 0;
        SoundManager.instance.PlayBGM(BGMType.Clear);
        UIManager.instance.OnClearUI();
    }

    public void RestartAfterClear()
    {
        Time.timeScale = 1;
        SaveLoadManager.instance.ResetSave();
        RestartScene();
    }
}