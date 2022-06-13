using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthItem : Item
{
    public Sprite sprite;
    public override void effect()
    {
        Player.MAX_HEALTH += Player.MAX_HEALTH/2;
    }
    public override Sprite getSprite()
    {
        return sprite;
    }
}
