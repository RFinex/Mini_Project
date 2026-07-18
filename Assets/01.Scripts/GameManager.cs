using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject player;
    private PlayerController pc;

    private float sec;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        sec = 0;
    }

    // ¾À ¹Ù²ð ¶§ ¸¶´Ù ÃÊ±âÈ­
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
                
        if (SaveLoadManager.instance.SavePath())
        {
            player.transform.position = DataManager.instance.checkPos;
        }

        if (player != null)
        {
            pc = player.GetComponent<PlayerController>();
        }

        EffectManager.instance.Init();
        UIManager.instance.Init();
    }

    public void SaveGame()
    {
        SaveLoadManager.instance.Save();
    }

    private void Update()
    {
        TimerOn();

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartScene();
        }
    }

    private void TimerOn()
    {
        sec += Time.deltaTime;
        UIManager.instance.UpdateTimerText(sec);
    }

    private void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
