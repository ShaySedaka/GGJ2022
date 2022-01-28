using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField]
    private float _lightAttackStaminaCost;
    [SerializeField]
    private float _heavyAttackStaminaCost;
    [SerializeField]
    private float _utilityStaminaCost;

    [SerializeField]
    protected int _lightAttackDamage;
    [SerializeField]
    protected int _heavyAttackDamage;

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

    public float LightAttackCost { get => _lightAttackStaminaCost; set => _lightAttackStaminaCost = value; }
    public float HeavyAttackCost { get => _heavyAttackStaminaCost; set => _heavyAttackStaminaCost = value; }
    public float UtilityCost { get => _utilityStaminaCost; set => _utilityStaminaCost = value; }

    public abstract void LightAttack();
    public abstract void HeavyAttack();
    public abstract void Utility();

    protected void Update()
    {
        RegenHeroHealth();
    }

    private void RegenHeroHealth()
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

    public void TakeDamage(int damage)
    {
        CurrentHeroHealth -= damage;
        if(CurrentHeroHealth <= 0)
        {
            GameManager.Instance.GameOverScreen();
        }
    }

}
