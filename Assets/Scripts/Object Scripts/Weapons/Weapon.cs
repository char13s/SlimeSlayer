using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject bodyRef;
    [SerializeField] private GameObject swingSound;

    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject iceEffect;
    [SerializeField] private GameObject electricEffect;

    public GameObject FireEffect { get => fireEffect; set => fireEffect = value; }
    public GameObject IceEffect { get => iceEffect; set => iceEffect = value; }
    public GameObject ElectricEffect { get => electricEffect; set => electricEffect = value; }
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
    public void HandleEffects(Element type) {
        ResetAllElements();
        switch (type.Type) {
            case Elements.Electric:
                ElectricEffect.SetActive(true);
                break;
            case Elements.Ice:
                IceEffect.SetActive(true);
                break;
            case Elements.Fire:
                FireEffect.SetActive(true);
                break;
            default:
                break;
        }
    }
    private void ResetAllElements() {
        FireEffect.SetActive(false);
        IceEffect.SetActive(false);
        ElectricEffect.SetActive(false);
    }
}
