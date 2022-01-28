using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    private float _lightAttackCost;
    private float _heavyAttackCost;
    private float _utilityAttackCost;


    [SerializeField]
    public float MaxHeroStamina;
    [SerializeField]
    public float CurrentHeroStamina;
    [SerializeField]
    public float HeroStaminaRegenerate;

    public abstract void LightAttack();
    public abstract void HeavyAttack();
    public abstract void Utility();

    //stamina - must switch when depleted, goes down with each action that isnt movement
    //configurable attributes
    //
    
}
