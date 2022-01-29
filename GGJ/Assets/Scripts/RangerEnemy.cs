using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private void Update()
    {
        playerFound = Physics2D.OverlapCircleAll(transform.position, AttackRange, PlayerLayer);
        if (playerFound.Length > 0)
        {
            AttackUpdate();
        }
        Flip();
    }

    protected void Flip()
    {
        if ((facingR && GameManager.Instance.Player.transform.position.x > transform.position.x)
            || (!facingR && GameManager.Instance.Player.transform.position.x <= transform.position.x))
        {
            facingR = !facingR;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
    IEnumerator DealDmg()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject bullet = Shots.GetPooledObjects();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            Bullet shot = bullet.GetComponent<Bullet>();
            bullet.SetActive(true);
            shot.direction = firePoint.right;
        }
    }
    protected override void AttackUpdate()
    {
        if (Time.time - lastShot < CooldDownShot)
        {
            return;
        }
        lastShot = Time.time;
        StartCoroutine("DealDmg");
       
    }

    

    protected override void DyingUpdate()
    {

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }


}
