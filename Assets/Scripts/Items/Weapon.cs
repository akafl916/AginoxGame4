using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    public int DamageMultiplier;
    public override void effect()
    {
        player.increaseAttack(Player.INITIAL_DAMAGE*DamageMultiplier);
    }
}
