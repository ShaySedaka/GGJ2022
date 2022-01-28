using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    public Vector2 direction;

    public LayerMask layer;

    [SerializeField]
    bool DestroyOnImpact;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnEnable()
    {
        Invoke("Disable", 0.8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 7)
        {
            if (DestroyOnImpact)
            {
                Disable();
            }
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

}
