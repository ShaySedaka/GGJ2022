using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psycho : Enemy
{
    public LayerMask PlayerLayer;
    public float AttackRange;
    public GameObject attackEffect;
    Collider2D[] playerFound;



    private void Update()
    {
        playerFound = Physics2D.OverlapCircleAll(transform.position, AttackRange, PlayerLayer);
        if (playerFound.Length > 0)
        {
            AttackUpdate();
        }
        Flip();
    }
    protected override void AttackUpdate()
    {
        anim.SetTrigger("Attack");
        Destroy(gameObject, 1f);
    }
    IEnumerator DealDmg()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject go = Instantiate(attackEffect, transform.position, new Quaternion());
        go.GetComponent<FadeIn>().SetDmg(stats.Damage);
        
    }

    protected void Flip()
    {
        if ((facingR && GameManager.Instance.Player.transform.position.x >= transform.position.x)
            || (!facingR && GameManager.Instance.Player.transform.position.x < transform.position.x))
        {
            facingR = !facingR;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
    protected override void DyingUpdate()
    {
        // death animation + destroy?
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    
}
