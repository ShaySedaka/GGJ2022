using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalStats : MonoBehaviour
{
    private Hero _currentHero;

    [SerializeField]
    private PiperCombat _piper;

    [SerializeField]
    private ZenaCombat _zena;

    public Hero CurrentHero { get => _currentHero; set => _currentHero = value; }
    public PiperCombat Piper { get => _piper; set => _piper = value; }
    public ZenaCombat Zena { get => _zena; set => _zena = value; }

    // Start is called before the first frame update
    void Start()
    {
        _currentHero = Zena;
    }

    // Update is called once per frame
    void Update()
    {
        RegenHeroStamina(Zena);
        RegenHeroStamina(Piper);
    }

    private void RegenHeroStamina(Hero hero)
    {
        if (hero.CurrentHeroStamina + hero.HeroStaminaRegenerate < hero.MaxHeroStamina)
        {
            hero.CurrentHeroStamina += hero.HeroStaminaRegenerate * Time.deltaTime;
        }
        else
        {
            hero.CurrentHeroStamina = hero.MaxHeroStamina;
        }
    }
}
