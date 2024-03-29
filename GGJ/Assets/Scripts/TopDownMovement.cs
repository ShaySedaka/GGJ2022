using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField]
    private float _baseMovementSpeed;

    private PlayerGlobalStats _playerStats;

    private Rigidbody2D _rigidBody;

    Vector2 moveDirection;

    public float MovementSpeed 
    {
        get
        {
            return(_baseMovementSpeed + GameManager.Instance.Player.PlayerGlobalStatsRef.CurrentHero.HeroMovementSpeed);
        }
        set => _baseMovementSpeed = value; 
    }

    public Rigidbody2D RigidBody { get => _rigidBody; set => _rigidBody = value; }
    public bool IsMoving = false;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerGlobalStats>();
        GameManager.Instance.Player.LockInput = false;
    }

    void Update()
    {
        MovementStatus();
        if (!GameManager.Instance.Player.LockInput)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            if( (moveX > 0 && GameManager.Instance.Player.gameObject.transform.localScale.x < 0) ||
                (moveX < 0 && GameManager.Instance.Player.gameObject.transform.localScale.x > 0) )
            {
                GameManager.Instance.Player.gameObject.transform.localScale = new Vector3(-GameManager.Instance.Player.gameObject.transform.localScale.x , 
                GameManager.Instance.Player.gameObject.transform.localScale.y, GameManager.Instance.Player.gameObject.transform.localScale.z);
            }
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
            Move();

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchCharacters();
            }
        }        
    }

    private void Move()
    {
        RigidBody.velocity = moveDirection * MovementSpeed;
    }

    private void MovementStatus()
    {
        if (RigidBody.velocity != Vector2.zero)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SwitchCharacters()
    {
        if (_playerStats.CurrentHero == _playerStats.Piper)
        {
            _playerStats.Piper.gameObject.SetActive(false);
            _playerStats.Zena.gameObject.SetActive(true);
            _playerStats.CurrentHero = _playerStats.Zena;
            _playerStats.BenchedHero = _playerStats.Piper;
        }
        else
        {
            _playerStats.Piper.gameObject.SetActive(true);
            _playerStats.Zena.gameObject.SetActive(false);
            _playerStats.CurrentHero = _playerStats.Piper;
            _playerStats.BenchedHero = _playerStats.Zena;
        }
    }



}
