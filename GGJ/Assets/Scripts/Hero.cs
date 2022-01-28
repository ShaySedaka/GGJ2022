using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    private float _lightAttackStaminaCost;
    private float _heavyAttackStaminaCost;
    private float _utilityStaminaCost;


    [SerializeField]
    public float MaxHeroStamina;
    [SerializeField]
    public float CurrentHeroStamina;
    [SerializeField]
    public float HeroStaminaRegenerate;

    public float LightAttackCost { get => _lightAttackStaminaCost; set => _lightAttackStaminaCost = value; }
    public float HeavyAttackCost { get => _heavyAttackStaminaCost; set => _heavyAttackStaminaCost = value; }
    public float UtilityCost { get => _utilityStaminaCost; set => _utilityStaminaCost = value; }

    public abstract void LightAttack();
    public abstract void HeavyAttack();
    public abstract void Utility();

    //stamina - must switch when depleted, goes down with each action that isnt movement
    //configurable attributes
    //
    
}
