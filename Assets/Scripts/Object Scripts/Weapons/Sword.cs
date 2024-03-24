using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    
    public override void Attack() {
        base.Attack();
        Player.GetPlayer().Anim.SetTrigger("Sword Attack");
    }

    public override void ElectricAttack() {
        base.Attack();
        Player.GetPlayer().Anim.Play("Sword Electric Attack");
    }

    public override void FireAttack() {
        base.Attack();
        Player.GetPlayer().Anim.Play("Sword Fire Attack");
    }

    public override void IceAttack() {
        base.Attack();
        Player.GetPlayer().Anim.Play("Sword Ice Attack");
    }
}
