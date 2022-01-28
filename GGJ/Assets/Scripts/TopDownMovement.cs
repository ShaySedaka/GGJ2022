using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField]
    private float _baseMovementSpeed;

    private PlayerGlobalStats _playerStats;

    private Rigidbody2D rb;

    Vector2 moveDirection;

    public float MovementSpeed {
        get
        {
            return(_baseMovementSpeed + GameManager.Instance.Player.PlayerGlobalStatsRef.CurrentHero.HeroMovementSpeed);
        }
        set => _baseMovementSpeed = value; }

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
        rb.velocity = moveDirection * MovementSpeed;
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
