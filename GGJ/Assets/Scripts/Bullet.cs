using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    public Vector2 direction;

    public int[] layers;

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
        for (int i = 0; i < layers.Length; i++)
        {
            if (collision.gameObject.layer == layers[i])
            {
                if (collision.GetComponent<Enemy>())
                {
                    collision.GetComponent<Enemy>().GetAttacked(5);
                }
                if (DestroyOnImpact)
                {
                    Disable();
                }
            }
        }
        
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

}
