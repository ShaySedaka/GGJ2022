using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZenaCombat : Hero
{


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

    // Update is called once per frame
    void Update()
    {
        
    }
}
