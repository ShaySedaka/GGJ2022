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
        Flip();
    }
    protected void Flip()
    {
        if ((facingR && GameManager.Instance.Player.transform.position.x < transform.position.x)
            || (!facingR && GameManager.Instance.Player.transform.position.x >= transform.position.x))
        {
            facingR = !facingR;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
    protected override void AttackUpdate()
    {
        if (Time.time - lastStrike < CoolDownMelee)
        {
            return;
        }
        lastStrike = Time.time;
        anim.SetTrigger("Attack");
        StartCoroutine("DealDmg");
    }
    IEnumerator DealDmg()
    {
        yield return new WaitForSeconds(1f);
        GameObject go = Instantiate(attackEffect,transform.position, new Quaternion());
        go.GetComponent<AoeAttack>().SetDmg(stats.Damage);
    }

    protected override void DyingUpdate()
    {
        
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
