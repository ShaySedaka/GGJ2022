using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public TopDownMovement ControllerRef;
    [SerializeField]
    public PlayerGlobalStats PlayerGlobalStatsRef;
    [SerializeField]
    public ZenaCombat ZenaRef;
    [SerializeField]
    public PiperCombat PiperRef;

    public bool LockInput = false;

    public bool LockRegen = false;

    public void FreezePlayer()
    {
        LockInput = true;
        LockRegen = true;
    }

    public void UnfreezePlayer()
    {
        LockInput = false;
        LockRegen = false;
    }


}
