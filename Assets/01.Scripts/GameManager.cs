using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform respawn;
    GameObject player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            player.transform.position = respawn.position;
        }
    }
}
