using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapon
{
    public override void Attack() {
        base.Attack();
        Player.GetPlayer().Anim.SetTrigger("Wand Attack");
    }

    public override void ElectricAttack() {
        base.Attack();
        Player.GetPlayer().Anim.Play("Wand Electric Attack");
    }

    public override void FireAttack() {
        base.Attack();
        Player.GetPlayer().Anim.Play("Wand Fire Attack");
    }

    public override void IceAttack() {
        base.Attack();
        Player.GetPlayer().Anim.Play("Wand Ice Attack");
    }
}
