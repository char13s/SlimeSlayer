using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Attack(); 
    public abstract void IceAttack();
    public abstract void ElectricAttack();
    public abstract void FireAttack();
    public void UseItem(Elements type) {
        switch (type) {
            case Elements.Normal:
                Attack();
                break;
            case Elements.Ice:
                IceAttack();
                break;
            case Elements.Electric:
                ElectricAttack();
                break;
            case Elements.Fire:
                FireAttack();
                break;
        }
    }
}
