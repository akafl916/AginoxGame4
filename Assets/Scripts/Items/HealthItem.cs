using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    public Sprite sprite;
    public override void effect()
    {
        player.takeDamage(Player.MAX_HEALTH/-10);
    }
    public override Sprite getSprite()
    {
        return sprite;
    }
}