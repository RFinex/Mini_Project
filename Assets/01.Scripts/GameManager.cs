using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject player;
    private PlayerController pc;

    private bool isStart = false;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // 씬 바뀔 때 마다 초기화
    public void Init()
    {
        player = GameObject.Find("Player");

        if (player != null)
        {
            pc = player.GetComponent<PlayerController>();
            if (SaveLoadManager.instance.SavePath())
            {
                player.transform.position = DataManager.instance.CheckPos;
            }
        }

        EffectManager.instance.Init();
        UIManager.instance.Init();
    }

    // 게임 시작 체크
    public void SetStartGame(bool start)
    {
        isStart = start;
    }
    
    public void SaveGame()
    {
        SaveLoadManager.instance.Save();
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
        SceneManager.LoadScene(scene.name);
    }
}
