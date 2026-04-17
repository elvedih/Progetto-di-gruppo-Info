using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 lastDirection;
    public float moveSpeed;
    Rigidbody2D rb;
    [HideInInspector]
    public Vector2 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastDirection = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY);
        
        if(moveDir != Vector2.zero)
        {
            lastDirection = moveDir.normalized;
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
