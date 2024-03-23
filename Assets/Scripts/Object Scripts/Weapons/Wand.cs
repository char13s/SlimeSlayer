using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapon
{
    public override void Attack() {
        Player.GetPlayer().Anim.SetTrigger("Wand Attack");
    }

    public override void ElectricAttack() {
        Player.GetPlayer().Anim.Play("Wand Electric Attack");
    }

    public override void FireAttack() {
        Player.GetPlayer().Anim.Play("Fire Electric Attack");
    }

    public override void IceAttack() {
        Player.GetPlayer().Anim.Play("Ice Electric Attack");
    }
}
