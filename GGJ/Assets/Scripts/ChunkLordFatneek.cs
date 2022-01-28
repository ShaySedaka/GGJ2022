using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLordFatneek : Enemy
{
    public LayerMask PlayerLayer;
    public float AttackRange;
    public GameObject attackEffect;
    Collider2D[] playerFound;
    float lastStrike;
    [SerializeField]
    float CoolDownMelee;

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
        if (Time.time - lastStrike < CoolDownMelee)
        {
            return;
        }
        lastStrike = Time.time;
        Instantiate(attackEffect, playerFound[0].gameObject.transform.position, new Quaternion());
    }

    protected override void DyingUpdate()
    {
        // death animation + destroy?
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    /*IEnumerator Heal()
    {
        //hp+= mispar;
        //yield return new WaitForSeconds(קולדאון);
    }*/
}
