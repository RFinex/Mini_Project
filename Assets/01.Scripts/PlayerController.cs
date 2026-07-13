using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    private float dir;
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        speed = 5f;
    }


    private void Update()
    {
        PlayerControll();
    }

    private void PlayerControll()
    {
        dir = 0;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            dir += -1;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            dir += 1;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
    }
}
