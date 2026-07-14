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
        centerText = GameObject.Find("CenterText").GetComponent<TextMeshProUGUI>();
        centerText.text = "";
    }

    public void OnGameOverText()
    {
        centerText.text = "Game Over Press 'R' Key";
    }
    public void OffCenterText()
    {
        centerText.text = "";
    }
}
