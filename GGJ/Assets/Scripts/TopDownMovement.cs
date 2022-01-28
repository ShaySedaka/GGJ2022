using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;

    private Rigidbody2D rb;

    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        Move();
    }

    void Move()
    {
        rb.velocity = moveDirection * MoveSpeed;
    }

}
