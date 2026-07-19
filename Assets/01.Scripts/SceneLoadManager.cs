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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitDelay(scene));
        
    }

    private IEnumerator InitDelay(Scene scene)
    {
        yield return null;

        if (scene.name == "Stage1_Scene")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.Init();
                GameManager.instance.SetStartGame(true);
            }
        }
    }
}
