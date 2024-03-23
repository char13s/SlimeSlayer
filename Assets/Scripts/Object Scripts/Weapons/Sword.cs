using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    
    public override void Attack() {
        Player.GetPlayer().Anim.SetTrigger("Sword Attack");
    }

    public override void ElectricAttack() {
        Player.GetPlayer().Anim.Play("Sword Electric Attack");
    }

    public override void FireAttack() {
        Player.GetPlayer().Anim.Play("Sword Fire Attack");
    }

    public override void IceAttack() {
        Player.GetPlayer().Anim.Play("Sword Ice Attack");
    }
}
