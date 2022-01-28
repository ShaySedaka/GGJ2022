using System;
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

    private float _lightAttackDelay = 0.7f;
    private float _heavyAttackDelay = 0.7f;


    [SerializeField]
    private float _attackSpeedBonusForTesting;

    protected override void Update()
    {
        base.Update();
        if (!GameManager.Instance.Player.LockInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LightAttack();
            }

            if (Input.GetMouseButtonDown(1))
            {
                HeavyAttack();
            }

            if ((Input.GetKeyDown(KeyCode.LeftShift)))
            {
                Utility();
            }
        }
        
            
    }

    public override void HeavyAttack()
    {
        if (CurrentHeroStamina >= HeavyAttackCost && _timeSinceLastHeavyAttack >= HeavyAttackCooldown)
        {
            CurrentHeroStamina -= HeavyAttackCost;
            _timeSinceLastHeavyAttack = 0;
            StartCoroutine(HeavySwing());
        }
    }

    public IEnumerator HeavySwing()
    {
        // Play Animation
        GameManager.Instance.Player.LockInput = true;
        yield return new WaitForSeconds(_heavyAttackDelay);  //Animation Duration = delay      

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, enemyLayers);

        if (enemiesHit.Length > 0)
        {
            foreach (var enemy in enemiesHit)
            {
                enemy.gameObject.GetComponent<Enemy>().GetAttacked(_heavyAttackDamage);
            }
        }
        Debug.Log("HeavySwing");
        GameManager.Instance.Player.LockInput = false;
    }

    public override void LightAttack()
    {
        if (CurrentHeroStamina >= LightAttackCost && _timeSinceLastLightAttack >= LightAttackCooldown)
        {
            CurrentHeroStamina -= LightAttackCost;
            _timeSinceLastLightAttack = 0;
            StartCoroutine(LightSwing());
        }

    }

    public IEnumerator LightSwing()
    {
        // Play Animation
        GameManager.Instance.Player.LockInput = true; 
        yield return new WaitForSeconds(_lightAttackDelay);  //Animation Duration = delay      
        
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, enemyLayers);

        if (enemiesHit.Length > 0)
        {
            foreach (var enemy in enemiesHit)
            {
                enemy.gameObject.GetComponent<Enemy>().GetAttacked(_lightAttackDamage);
            }
        }
        Debug.Log("LightSwing");
        GameManager.Instance.Player.LockInput = false;        
    }

    public override void Utility()
    {
        if (CurrentHeroStamina >= UtilityCost && _timeSinceLastUtility >= UtilityCooldown)
        {
            CurrentHeroStamina -= UtilityCost;
            _timeSinceLastUtility = 0;
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        GameManager.Instance.Player.LockInput = true;
        float elapsedTime = 0;
        while (elapsedTime < _dashTime)
        {
            float direction = Math.Sign(GameManager.Instance.Player.gameObject.transform.localScale.x);
            _rigidBody.velocity = Vector2.right * _dashSpeed * direction;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GameManager.Instance.Player.LockInput = false;
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public void UpgradeStrength(int damageIncrease)
    {
        _lightAttackDamage += damageIncrease;
    }

    public void UpgradeVitality(float regenIncrease)
    {
        HeroHealthRegenerate += regenIncrease;
    }

    public void UpgradeAgility(float movementIncrease)
    {
        _dashSpeed += movementIncrease;
    }
}
