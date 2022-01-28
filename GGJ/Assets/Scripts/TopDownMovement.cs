using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;

    private PlayerGlobalStats _playerStats;

    private Rigidbody2D rb;

    Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerGlobalStats>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        Move();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCharacters();
        }
    }

    private void Move()
    {
        rb.velocity = moveDirection * MoveSpeed;
    }

    private void SwitchCharacters()
    {
        if (_playerStats.CurrentHero == _playerStats.Piper)
        {
            _playerStats.Piper.gameObject.SetActive(false);
            _playerStats.Zena.gameObject.SetActive(true);
            _playerStats.CurrentHero = _playerStats.Zena;
        }
        else
        {
            _playerStats.Piper.gameObject.SetActive(true);
            _playerStats.Zena.gameObject.SetActive(false);
            _playerStats.CurrentHero = _playerStats.Piper;
        }
    }

}
