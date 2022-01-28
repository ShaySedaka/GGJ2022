using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public abstract void LightAttack();
    public abstract void HeavyAttack();
    public abstract void Utility();

    //stamina - must switch when depleted, goes down with each action that isnt movement
    //configurable attributes
    //
    
}
