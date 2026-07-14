using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform respawn;
    GameObject player;
    PlayerController pc;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            player.transform.position = respawn.position;
            pc.Revive();
        }
    }
}
