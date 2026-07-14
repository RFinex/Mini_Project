using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TextMeshProUGUI centerText;

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

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void OnGameOverText()
    {
        centerText.text = "Game Over Press 'R' Key"
    }
}
