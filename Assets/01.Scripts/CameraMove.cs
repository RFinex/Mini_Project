using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera camera;
    private float screenHalfSizeX;
    private float screenHalfSizeY;
    [SerializeField] private Transform target;

    private void Awake()
    {
        camera = Camera.main;
        screenHalfSizeY = camera.orthographicSize;
        screenHalfSizeX = screenHalfSizeY * camera.aspect;
    }

    private void LateUpdate()
    {
        if (transform.position.x < target.position.x - screenHalfSizeX)
        {
            transform.position += Vector3.right * screenHalfSizeX * 2;
        }
        else if (transform.position.x > target.position.x + screenHalfSizeX)
        {
            transform.position += Vector3.left * screenHalfSizeX * 2;
        }

        if (transform.position.y < target.position.y - screenHalfSizeY)
        {
            transform.position += Vector3.up * screenHalfSizeY * 2;
        }
        else if (transform.position.y > target.position.y + screenHalfSizeY)
        {
            transform.position += Vector3.down * screenHalfSizeY * 2;
        }
    }
}
