using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public string upgrade_name;
    public string upgrade_description;
    public Sprite upgrade_icon;
    public Color upgrade_color;
    public Color upgrade_color_dark;
    public Hero upgrade_character;

    public bool is_unique;
    public virtual void DoUpgrade()
    {

    }

    public Upgrade(Hero upgrade_character)
    {
        this.upgrade_character = upgrade_character;
    }
}

public class StrengthUpgrade : Upgrade
{
    public StrengthUpgrade(Hero upgrade_character): base(upgrade_character)
    {
        upgrade_name = "STRENGTH";
        upgrade_description = "Increase swing damage";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = false;
    }
    public override void DoUpgrade()
    {
        ZenaCombat zena = (ZenaCombat)(upgrade_character);
        zena.UpgradeStrength(3);
    }
}

public class VitalityUpgrade : Upgrade
{
    public VitalityUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "Vitality";
        upgrade_description = "Increase health regeneration rate";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = false;
    }
    public override void DoUpgrade()
    {
        ZenaCombat zena = (ZenaCombat)(upgrade_character);
        zena.UpgradeVitality(10);
    }
}

public class AgilityUpgrade : Upgrade
{
    public AgilityUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "Agility";
        upgrade_description = "Increase dash distance";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = false;
    }
    public override void DoUpgrade()
    {
        ZenaCombat zena = (ZenaCombat)(upgrade_character);
        zena.UpgradeAgility(0.1f);
    }
}

public class StunUpgrade : Upgrade
{
    public StunUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "Crushing Blow";
        upgrade_description = "Zena's heavy swing has a chance to stun enemies for a short time";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = true;
    }
    public override void DoUpgrade()
    {
        // Update Player.
    }
}

public class SlowUpgrade : Upgrade
{
    public SlowUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "Instigate";
        upgrade_description = "Dashing through enemies slows them for a short time";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = true;
    }
    public override void DoUpgrade()
    {
        // Update Player.
    }
}

public class ArmsUpgrade : Upgrade
{
    public ArmsUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "Arms";
        upgrade_description = "Increase bullet damage!";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = false;
    }
    public override void DoUpgrade()
    {
        PiperCombat piper = (PiperCombat)(upgrade_character);
        piper.UpgradeArms(3);
    }
}

public class PersistenceUpgrade : Upgrade
{
    public PersistenceUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "Persistence";
        upgrade_description = "Increase Stamina regeneration rate";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = false;
    }
    public override void DoUpgrade()
    {
        PiperCombat piper = (PiperCombat)(upgrade_character);
        piper.UpgradePersistence(3);
    }
}

public class SwiftnessUpgrade : Upgrade
{
    public SwiftnessUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "Swiftness";
        upgrade_description = "Increase movement speed";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = false;
    }
    public override void DoUpgrade()
    {
        PiperCombat piper = (PiperCombat)(upgrade_character);
        piper.UpgradeSwiftness(0.1f);
    }
}

public class BleedUpgrade : Upgrade
{
    public BleedUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "A Slow Death";
        upgrade_description = "Caltrops cause enemies to Bleed!";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = true;
    }
    public override void DoUpgrade()
    {
        // Update Player.
    }
}

public class KnockbackUpgrade : Upgrade
{
    public KnockbackUpgrade(Hero upgrade_character) : base(upgrade_character)
    {
        upgrade_name = "Back Off!";
        upgrade_description = "Heavy shot knocks enemies back";
        upgrade_color = Color.red;
        upgrade_color_dark = Color.black;
        is_unique = true;
    }
    public override void DoUpgrade()
    {
        // Update Player.
    }
}
