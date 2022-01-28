using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalStats : MonoBehaviour
{
    [SerializeField]
    private PiperCombat _piper;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RegenHero(_piper);
    }

    private void RegenHero(Hero hero)
    {
        if (hero.CurrentHeroStamina + hero.HeroStaminaRegenerate < hero.MaxHeroStamina)
        {
            hero.CurrentHeroStamina += hero.HeroStaminaRegenerate * Time.deltaTime;
        }
        else
        {
            hero.CurrentHeroStamina = hero.MaxHeroStamina;
        }

        Debug.Log(hero.CurrentHeroStamina);
    }
}
