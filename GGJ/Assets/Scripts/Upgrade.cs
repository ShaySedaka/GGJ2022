using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade
{
    public string upgrade_name;
    public string upgrade_description;
    public Sprite upgrade_icon;
    public Color upgrade_color;
    public Color upgrade_color_dark;
    public Hero upgrade_character;

    public bool is_unique;
    public abstract void DoUpgrade();
}
