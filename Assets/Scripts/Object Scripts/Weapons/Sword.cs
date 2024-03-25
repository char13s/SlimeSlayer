using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    
    public override void Attack() {
        PlaySound();
        Player.GetPlayer().Anim.SetTrigger("Sword Attack");
    }

    public override void ElectricAttack() {
        PlaySound();
        Player.GetPlayer().Anim.Play("Sword Electric Attack");
    }

    public override void FireAttack() {
        PlaySound();
        Player.GetPlayer().Anim.Play("Sword Fire Attack");
    }

    public override void IceAttack() {
        PlaySound();
        Player.GetPlayer().Anim.Play("Sword Ice Attack");
    }
}
