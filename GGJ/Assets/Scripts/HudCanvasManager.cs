using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudCanvasManager : MonoBehaviour
{
    [SerializeField] private Image _currentHeroPortrait;
    [SerializeField] private Image _benchedHeroPortrait;
    [SerializeField] private TextMeshProUGUI _currentHeroName;
    [SerializeField] private Slider _currentHeroHP;
    [SerializeField] private Slider _currentHeroStamina;
    [SerializeField] private Slider _benchedHeroHP;
    [SerializeField] private Slider _benchedHeroStamina;

    private Hero _currentHero;
    private Hero _benchedHero;

    // Update is called once per frame
    void Update()
    {
        _currentHero = GameManager.Instance.Player.PlayerGlobalStatsRef.CurrentHero;
        _benchedHero = GameManager.Instance.Player.PlayerGlobalStatsRef.BenchedHero;

        UpdateHero(_currentHero, _currentHeroHP, _currentHeroStamina, _currentHeroPortrait);
        _currentHeroName.text = _currentHero.Name;
        UpdateHero(_benchedHero, _benchedHeroHP, _benchedHeroStamina, _benchedHeroPortrait);
    }

    private void UpdateHero(Hero hero, Slider hp, Slider stamina, Image image)
    {
        hp.value = hero.CurrentHeroHealth / hero.MaxHeroHealth;
        stamina.value = hero.CurrentHeroStamina / hero.MaxHeroStamina;
        image.sprite = hero.HeroPortrait;
    }
}
