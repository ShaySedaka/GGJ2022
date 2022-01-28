using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZenaCombat : Hero
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 0.5f;

    public LayerMask enemyLayers;

    [SerializeField]
    private float _dashSpeed;
    [SerializeField]
    private float _dashTime;
    private float _startDashTime;
    private int direction;
    private bool _isDashing = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LightAttack();
        }

        if( (Input.GetKeyDown(KeyCode.LeftShift)) )
        {
            Utility();
        }
    }

    public override void HeavyAttack()
    {
        if (CurrentHeroStamina >= HeavyAttackCost)
        {
            
        }
    }

    public override void LightAttack()
    {
        if (CurrentHeroStamina >= LightAttackCost)
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, enemyLayers);

            if (enemiesHit.Length > 0)
            {
                foreach (var enemy in enemiesHit)
                {
                    enemy.gameObject.GetComponent<Enemy>().GetAttacked(_lightAttackDamage);
                }
            }
            
            Debug.Log("Light");
        }

    }

    public override void Utility()
    {
        if (CurrentHeroStamina >= UtilityCost)
        {
            if(!_isDashing)
            {
                StartCoroutine(Dash());
            }            
            CurrentHeroStamina -= UtilityCost;
        }
    }

    private IEnumerator Dash()
    {
        float elapsedTime = 0;
        while (elapsedTime < _dashTime)
        {
            _rigidBody.velocity = Vector2.right * _dashSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
