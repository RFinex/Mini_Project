using JetBrains.Annotations;
using UnityEngine;

public class Background_Menu : MonoBehaviour
{
    private Transform[] back;

    [SerializeField] private float scrollSpeed;

    private float backgroundWidth;
    private float totalWidth;

    private void Start()
    {
        back = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            back[i] = transform.GetChild(i);
        }

        backgroundWidth = back[0].GetComponent<SpriteRenderer>().bounds.size.x;
        totalWidth = backgroundWidth * back.Length;
    }

    private void Update()
    {
        for (int i = 0; i < back.Length; i++)
        {
            back[i].position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        for (int i = 0; i < back.Length; i++)
        {
            if (back[i].position.x < -backgroundWidth)
            {
                back[i].position += Vector3.right * totalWidth;
            }
        }
    }
}
