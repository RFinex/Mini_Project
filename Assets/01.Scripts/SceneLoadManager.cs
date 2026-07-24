using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 모든 매니저 설정값 초기화
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu_Scene")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.Init_Menu();
            }
        }
        else if (scene.name == "Stage1_Scene")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.Init();
                GameManager.instance.SetStartGame(true);
            }
        }
    }
}
