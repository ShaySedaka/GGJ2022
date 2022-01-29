using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZenaCombat : Hero
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private Transform _lightAttackPoint;
    [SerializeField]
    private Transform _heavyAttackPoint;

    [SerializeField]
    private float _lightAttackRange = 0.5f;
    [SerializeField]
    private float _heavyAttackRange = 0.5f;

    public LayerMask enemyLayers;

    [SerializeField]
    private float _dashSpeed;
    [SerializeField]
    private float _dashTime;
    private float _startDashTime;
    private int _direction;

    private float _lightAttackDelay = 0.7f;
    private float _heavyAttackDelay = 1.3f;

    protected override void Update()
    {
        base.Update();
        if(GameManager.Instance.Player.ControllerRef.IsMoving)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }


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

    public override void LightAttack()
    {
        if (CurrentHeroStamina >= LightAttackCost && _timeSinceLastLightAttack >= LightAttackCooldown)
        {
            CurrentHeroStamina -= LightAttackCost;
            _timeSinceLastLightAttack = 0;
            StopMoving();
            StartCoroutine(LightSwing());
        }

    }

    public IEnumerator LightSwing()
    {
        // Play Animation
        GameManager.Instance.Player.LockInput = true;
        _animator.SetBool("LightAttack", true);
        yield return new WaitForSeconds(_lightAttackDelay);  //Animation Duration = delay      

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_lightAttackPoint.position, _lightAttackRange, enemyLayers);

        if (enemiesHit.Length > 0)
        {
            foreach (var enemy in enemiesHit)
            {
                enemy.gameObject.GetComponent<Enemy>().GetAttacked(_lightAttackDamage);
            }
        }
        Debug.Log("LightSwing");
        _animator.SetBool("LightAttack", false);
        GameManager.Instance.Player.LockInput = false;
    }

    public override void HeavyAttack()
    {
        if (CurrentHeroStamina >= HeavyAttackCost && _timeSinceLastHeavyAttack >= HeavyAttackCooldown)
        {
            CurrentHeroStamina -= HeavyAttackCost;
            _timeSinceLastHeavyAttack = 0;
            StopMoving();
            StartCoroutine(HeavySwing());
        }
    }

    public IEnumerator HeavySwing()
    {
        // Play Animation
        GameManager.Instance.Player.LockInput = true;
        
        _animator.SetBool("HeavyAttack", true);
        yield return new WaitForSeconds(_heavyAttackDelay);  //Animation Duration = delay      

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_heavyAttackPoint.position, _heavyAttackRange, enemyLayers);

        if (enemiesHit.Length > 0)
        {
            foreach (var enemy in enemiesHit)
            {
                enemy.gameObject.GetComponent<Enemy>().GetAttacked(_heavyAttackDamage);
            }
        }
        Debug.Log("HeavySwing");
        _animator.SetBool("HeavyAttack", false);
        GameManager.Instance.Player.LockInput = false;
    }

    public override void Utility()
    {
        if (CurrentHeroStamina >= UtilityCost && _timeSinceLastUtility >= UtilityCooldown)
        {
            CurrentHeroStamina -= UtilityCost;
            _timeSinceLastUtility = 0;
            StopMoving();
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        GameManager.Instance.Player.LockInput = true;
        _animator.SetBool("IsMoving", false);
        _animator.SetBool("IsDashing", true);
        float elapsedTime = 0;
        while (elapsedTime < _dashTime)
        {
            _direction = Math.Sign(GameManager.Instance.Player.gameObject.transform.localScale.x);
            _rigidBody.velocity = Vector2.right * _dashSpeed * _direction;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        _animator.SetBool("IsDashing", false);
        GameManager.Instance.Player.LockInput = false;
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_lightAttackPoint.position, _lightAttackRange);
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
