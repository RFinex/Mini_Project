using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera camera;
    private float screenHalfSizeX;
    private float screenHalfSizeY;
    [SerializeField] Transform target;

    private void Awake()
    {
        camera = Camera.main;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
