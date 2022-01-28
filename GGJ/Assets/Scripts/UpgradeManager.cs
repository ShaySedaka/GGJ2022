using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //[SerializeField]public UpgradeCanvas upgradeCanvas;

    private System.Random random;

    public bool is_upgrading;
    public List<Upgrade> zena_upgrades;
    public List<Upgrade> piper_upgrades;
    private Upgrade[] upgradeSlots = new Upgrade[4];
    public uint upgradesToMake;

    
    void Start()
    {
        is_upgrading = false;
        upgradesToMake = 0;
        random = new System.Random();
    }

    
    void Update()
    {
        
    }

    public void CreateUpgrades()
    {

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
        Upgrade upgradeZena1 = zena_upgrades[indexZena1];
        Upgrade upgradeZena2 = zena_upgrades[indexZena2];
        Upgrade upgradePiper1 = piper_upgrades[indexPiper1];
        Upgrade upgradePiper2 = piper_upgrades[indexPiper2];
        //UpgradeCanvas.PromptUpgradeCanvas(upgradeZena1, upgradeZena2, upgradePiper1, upgradePiper2);
    }

    public void OnUpgradePress(int upgrade_slot)
    {
        if (!(upgrade_slot < 4 && upgrade_slot >= 0))
        {
            Debug.LogError("invalid upgrade_slot given in OnUpgradePress()");
            return;
        }
        upgradeSlots[upgrade_slot].DoUpgrade();
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
            //UpgradeCanvas.HideCanvas();
            is_upgrading = false;
        }
    }
}
