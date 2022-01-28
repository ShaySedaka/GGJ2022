using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade
{
    public string upgrade_text;
    public Sprite upgrade_icon;
    public Color upgrade_color;
    public Hero upgrade_character;
    void DoUpgrade()
    {
        //do upgrade on the hero.
    }
}
