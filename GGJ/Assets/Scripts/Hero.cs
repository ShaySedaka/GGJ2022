using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    #region Light Attack Data

    [SerializeField]
    private float _lightAttackStaminaCost;

    [SerializeField]
    protected int _lightAttackDamage;

    [SerializeField]
    private float _attackSpeedBonus = 0;

    public float LightAttackCooldown { get => 1f / (1f + (_attackSpeedBonus / 100f)); }

    protected float _timeSinceLastLightAttack = 0;

    public float LightAttackCost { get => _lightAttackStaminaCost; set => _lightAttackStaminaCost = value; }
    #endregion

    #region Heavy Attack Data

    [SerializeField]
    private float _heavyAttackStaminaCost;

    [SerializeField]
    protected int _heavyAttackDamage;

    public float HeavyAttackCooldown = 2;

    protected float _timeSinceLastHeavyAttack;

    public float HeavyAttackCost { get => _heavyAttackStaminaCost; set => _heavyAttackStaminaCost = value; }

    #endregion

    #region Utility

    [SerializeField]
    private float _utilityStaminaCost;

    public float UtilityCost { get => _utilityStaminaCost; set => _utilityStaminaCost = value; }

    protected float _timeSinceLastUtility;

    public float UtilityCooldown = 1;

    #endregion

    [SerializeField]
    public float MaxHeroStamina;
    [SerializeField]
    public float CurrentHeroStamina;
    [SerializeField]
    public float HeroStaminaRegenerate;

    [SerializeField]
    public float MaxHeroHealth;
    [SerializeField]
    public float CurrentHeroHealth;
    [SerializeField]
    public float HeroHealthRegenerate;

    [SerializeField]
    public float HeroMovementSpeed;
   
    public abstract void LightAttack();
    public abstract void HeavyAttack();
    public abstract void Utility();

    protected virtual void Update()
    {
        RegenHeroHealth();
        UpdateCooldowns();
    }

    private void RegenHeroHealth()
    {
        if(!GameManager.Instance.Player.LockRegen)
        {
            if (CurrentHeroHealth + HeroHealthRegenerate < MaxHeroHealth)
            {
                CurrentHeroHealth += HeroHealthRegenerate * Time.deltaTime;
            }
            else if (CurrentHeroHealth + HeroHealthRegenerate == MaxHeroHealth)
            {
                CurrentHeroHealth = MaxHeroHealth;
            }
        }            
    }

    public void TakeDamage(int damage)
    {
        CurrentHeroHealth -= damage;
        if(CurrentHeroHealth <= 0)
        {
            GameManager.Instance.GameOverScreen();
        }
    }

    private void UpdateCooldowns()
    {
        UpdateLightAttackCooldown();
        UpdateHeavyAttackCooldown();
        UpdateUtilityCooldown();
    }

    private void UpdateLightAttackCooldown()
    {
        if(_timeSinceLastLightAttack < LightAttackCooldown)
        {
            _timeSinceLastLightAttack += Time.deltaTime;
            
        }
    }

    private void UpdateHeavyAttackCooldown()
    {
        if (_timeSinceLastHeavyAttack < HeavyAttackCooldown)
        {
            _timeSinceLastHeavyAttack += Time.deltaTime;

        }
    }

    private void UpdateUtilityCooldown()
    {
        if (_timeSinceLastUtility < UtilityCooldown)
        {
            _timeSinceLastUtility += Time.deltaTime;

        }
    }

    protected void StopMoving()
    {
        GameManager.Instance.Player.ControllerRef.RigidBody.velocity = Vector2.zero;
    }

}
