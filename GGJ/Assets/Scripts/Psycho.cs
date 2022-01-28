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
    }
    protected override void AttackUpdate()
    {
        Instantiate(attackEffect, transform.position, new Quaternion());
        Destroy(this.gameObject);
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
