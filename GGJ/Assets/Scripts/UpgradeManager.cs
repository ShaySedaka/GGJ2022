using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public bool is_upgrading;
    public List<Upgrade> zena_upgrades;
    public List<Upgrade> piper_upgrades;

    
    void Start()
    {
        is_upgrading = false;
    }

    
    void Update()
    {
        
    }
}
