using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZenaCombat : Hero
{
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 0.5f;

    public LayerMask enemyLayers;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LightAttack();
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
            Dash();
            CurrentHeroStamina -= UtilityCost;
        }
    }

    private void Dash()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
