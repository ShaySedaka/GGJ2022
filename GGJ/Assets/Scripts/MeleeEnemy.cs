using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public LayerMask PlayerLayer;
    public float AttackRange;
    public ParticleSystem attackEffect;
    Collider2D[] playerFound;
    float lastStrike;
    [SerializeField]
    float CoolDownMelee;

    private void Update()
    {
        playerFound = Physics2D.OverlapCircleAll(transform.position, AttackRange, PlayerLayer);
        if (playerFound != null)
        {
            if (playerFound.Length > 0)
            {
                AttackUpdate();
            }
        }

        Flip();

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
    protected void Flip()
    {
        if ((facingR && GameManager.Instance.Player.transform.position.x < transform.position.x)
            || (!facingR && GameManager.Instance.Player.transform.position.x >= transform.position.x))
        {
            facingR = !facingR;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
    IEnumerator DealDmg()
    {
        yield return new WaitForSeconds(1.3f);
        GameManager.Instance.Player.PlayerGlobalStatsRef.CurrentHero.TakeDamage(stats.Damage);
    }
    protected override void DyingUpdate()
    {
        Debug.Log("I DIED");
        // death animation + destroy?
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
