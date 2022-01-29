using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private System.Random random;

    public bool is_upgrading;
    public List<Upgrade> zena_upgrades;
    public List<Upgrade> piper_upgrades;
    private Upgrade[] upgradeSlots = new Upgrade[4];
    public int upgradesToMake;

    private bool advanced_upgrade_created = false;
    private uint advanced_upgrade_minlevel = 5;
    private uint currect_level;

    [SerializeField] ZenaCombat zena;
    [SerializeField] PiperCombat piper;

    
    void Awake()
    {
        is_upgrading = false;
        upgradesToMake = 0;
        currect_level = 1;
        random = new System.Random();
        CreateBaseUpgrades();
    }

    public void Upgrade(int upgrade_count)
    {
        is_upgrading = true;
        upgradesToMake = upgrade_count;
        if(upgradesToMake > 0)
        {
            OfferUpgrades();
        }
        else
        {
            is_upgrading = false;
        }
    }

    public void CreateBaseUpgrades()
    {
        zena_upgrades = new List<Upgrade>();
        piper_upgrades = new List<Upgrade>();
        zena_upgrades.Add(new StrengthUpgrade(zena));
        zena_upgrades.Add(new VitalityUpgrade(zena));
        zena_upgrades.Add(new AgilityUpgrade(zena));
        piper_upgrades.Add(new ArmsUpgrade(piper));
        piper_upgrades.Add(new PersistenceUpgrade(piper));
        piper_upgrades.Add(new SwiftnessUpgrade(piper));
    }

    public void CreateAdvancedUpgrades()
    {
        zena_upgrades.Add(new StunUpgrade(zena));
        zena_upgrades.Add(new SlowUpgrade(zena));
        piper_upgrades.Add(new BleedUpgrade(piper));
        piper_upgrades.Add(new KnockbackUpgrade(piper));
        advanced_upgrade_created = true;
    }

    public void OfferUpgrades()
    {
        int indexZena1 = random.Next(zena_upgrades.Count);
        int indexZena2;
        int indexPiper1 = random.Next(piper_upgrades.Count);
        int indexPiper2;
        do
        {
            indexZena2 = random.Next(zena_upgrades.Count);
        }
        while (indexZena2 == indexZena1);
        do
        {
            indexPiper2 = random.Next(piper_upgrades.Count);
        }
        while (indexPiper2 == indexPiper1);
        upgradeSlots[0] = zena_upgrades[indexZena1];
        upgradeSlots[1] = zena_upgrades[indexZena2];
        upgradeSlots[2] = piper_upgrades[indexPiper1];
        upgradeSlots[3] = piper_upgrades[indexPiper2];
        UpgradeCanvasManager.Instance.PromptUpgradeCanvas(upgradeSlots[0], upgradeSlots[1], upgradeSlots[2], upgradeSlots[3], upgradesToMake);
    }

    public void OnUpgradePress(int upgrade_slot)
    {
        if (!(upgrade_slot < 4 && upgrade_slot >= 0))
        {
            Debug.LogError("invalid upgrade_slot given in OnUpgradePress()");
            return;
        }
        Upgrade chosen_upgrade = upgradeSlots[upgrade_slot];
        if (chosen_upgrade.is_unique)
        {
            Hero upgrade_character = chosen_upgrade.upgrade_character;
            if (upgrade_character == zena)
            {
                zena_upgrades.Remove(chosen_upgrade);
            }
            else if (upgrade_character == piper)
            {
                piper_upgrades.Remove(chosen_upgrade);
            }
            else
            {
                Debug.LogError("Invalid upgrade character");
            }
        }
        chosen_upgrade.DoUpgrade();
        currect_level++;
        if (currect_level >= advanced_upgrade_minlevel && !advanced_upgrade_created)
        {
            CreateAdvancedUpgrades();
        }
        if (upgradesToMake == 0)
        {
            Debug.LogError("upgradesToMake was already zero WTF");
            return;
        }
        upgradesToMake--;
        if (upgradesToMake > 0)
        {
            OfferUpgrades();
        }
        else
        {
            UpgradeCanvasManager.Instance.HideUpgradeCanvas();
            is_upgrading = false;
        }
    }
}
