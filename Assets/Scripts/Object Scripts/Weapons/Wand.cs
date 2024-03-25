using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Weapon
{
    public override void Attack() {
        PlaySound();
        Player.GetPlayer().Anim.SetTrigger("Wand Attack");
    }

    public override void ElectricAttack() {
        PlaySound();
        Player.GetPlayer().Anim.Play("Wand Electric Attack");
    }

    public override void FireAttack() {
        PlaySound();
        Player.GetPlayer().Anim.Play("Wand Fire Attack");
    }

    public override void IceAttack() {
        PlaySound();
        Player.GetPlayer().Anim.Play("Wand Ice Attack");
    }
}
