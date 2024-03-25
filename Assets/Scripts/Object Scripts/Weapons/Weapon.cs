using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject bodyRef;
    [SerializeField] private GameObject swingSound;
    private void OnEnable() {
        bodyRef.SetActive(false);
    }
    private void OnDisable() {
        bodyRef.SetActive(true);
    }
    public abstract void Attack();
    public void PlaySound() { 
     if (swingSound != null)
            Instantiate(swingSound, transform.position, Quaternion.identity);
    }
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
