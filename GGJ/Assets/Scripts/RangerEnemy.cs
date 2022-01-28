using UnityEngine;

public class RangerEnemy : Enemy
{
    public LayerMask PlayerLayer;
    public float AttackRange;
    public ParticleSystem attackEffect;
    Collider2D[] playerFound;
    float lastShot;
    [SerializeField]
    float CooldDownShot;
    public ObjectPool Shots;
    [SerializeField]
    Transform firePoint;
    LineRenderer liner;

    private void Start()
    {
        liner = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        playerFound = Physics2D.OverlapCircleAll(transform.position, AttackRange, PlayerLayer);
        if (playerFound.Length > 0)
        {

            AttackUpdate();
        }
    }
    protected override void AttackUpdate()
    {
        if (Time.time - lastShot < CooldDownShot)
        {
            return;
        }
        lastShot = Time.time;

        GameObject bullet = Shots.GetPooledObjects();
        if (bullet != null)
        {
            Debug.Log("found");
            bullet.transform.position = firePoint.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);

            shot.direction = firePoint.right;
        }
    }

    protected override void DyingUpdate()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }


}
