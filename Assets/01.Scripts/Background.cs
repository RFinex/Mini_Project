using UnityEngine;

public class Background : MonoBehaviour
{
    Transform back;

    float backgroundWidth;
    float backgroundHeight;

    [SerializeField] Transform target;

    private void Awake()
    {
        back = transform.GetChild(0);

        backgroundWidth = back.GetComponent<SpriteRenderer>().bounds.size.x;
        backgroundHeight = back.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        if (back.position.x < target.position.x - backgroundWidth / 2)
        {
            back.position += Vector3.right * backgroundWidth;
        }
        else if (back.position.x > target.position.x + backgroundWidth / 2)
        {
            back.position += Vector3.left * backgroundWidth;
        }

        if (back.position.y < target.position.y - backgroundHeight / 2)
        {
            back.position += Vector3.up * backgroundHeight;
        }
        else if (back.position.y > target.position.y + backgroundHeight / 2)
        {
            back.position += Vector3.down * backgroundHeight;
        }
    }
}
