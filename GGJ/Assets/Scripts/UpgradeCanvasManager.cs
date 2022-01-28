using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UpgradeCanvasManager : Singleton<UpgradeCanvasManager>
{
    [SerializeField] private GameObject _upgradeCanvas;

    [SerializeField] private TextMeshProUGUI _upgradesToMake;

    [SerializeField] private TextMeshProUGUI _slot1Title;
    [SerializeField] private TextMeshProUGUI _slot1Description;

    [SerializeField] private TextMeshProUGUI _slot2Title;
    [SerializeField] private TextMeshProUGUI _slot2Description;

    [SerializeField] private TextMeshProUGUI _slot3Title;
    [SerializeField] private TextMeshProUGUI _slot3Description;

    [SerializeField] private TextMeshProUGUI _slot4Title;
    [SerializeField] private TextMeshProUGUI _slot4Description;


    public void PromptUpgradeCanvas(Upgrade upgradeSlot1, Upgrade upgradeSlot2, Upgrade upgradeSlot3, Upgrade upgradeSlot4, uint upgradesToMake)
    {
        _upgradesToMake.text = upgradesToMake.ToString();

        _slot1Title.text = upgradeSlot1.upgrade_name;
        _slot1Description.text = upgradeSlot1.upgrade_description;

        _slot2Title.text = upgradeSlot2.upgrade_name;
        _slot2Description.text = upgradeSlot2.upgrade_description;

        _slot3Title.text = upgradeSlot3.upgrade_name;
        _slot3Description.text = upgradeSlot3.upgrade_description;

        _slot4Title.text = upgradeSlot4.upgrade_name;
        _slot4Description.text = upgradeSlot4.upgrade_description;

        _upgradeCanvas.SetActive(true);
    }

    public void HideUpgradeCanvas()
    {
        _upgradeCanvas.SetActive(false);
 
    }

    public void UnHideUpgradeCanvas()
    {
        _upgradeCanvas.SetActive(true);

    }

}
