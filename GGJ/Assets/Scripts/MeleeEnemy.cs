using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class MeleeEnemy : Enemy
{
    protected override void AttackUpdate()
    {
        // Use attack speed here, to calc how long the attack is.
    }

    protected override void DyingUpdate()
    {
        // death animation + destroy?
    }
}
