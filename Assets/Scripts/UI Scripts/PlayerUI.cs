using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject electric;
    [SerializeField] private GameObject ice;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject normal;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider mpBar;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject wand;
    // Start is called before the first frame update
    private void OnEnable() {
        PlayerInputs.sendElement += PickAnElement;
        PlayerInputs.sendWeapon += PickAWeapon;
        Stats.onHealthChange += SetHealthLeft;
        //Stats.sendMp += SetMpBar;
    }
    private void OnDisable() {
        PlayerInputs.sendElement -= PickAnElement;
        Stats.onHealthChange -= SetHealthLeft;
        PlayerInputs.sendWeapon -= PickAWeapon;
        Stats.sendMp -= SetMpBar;
    }
    void Start()
    {
        //healthBar.maxValue = Player.GetPlayer().stats.Health;
        //healthBar.value = Player.GetPlayer().stats.HealthLeft;
        //mpBar.maxValue= Player.GetPlayer().stats.MP;
        //mpBar.value = Player.GetPlayer().stats.MPLeft;
    }
    private void SetHealthLeft() { 
        healthBar.value= Player.GetPlayer().stats.HealthLeft;
    }
    private void SetMpBar(int val) {
        mpBar.value = val;
    }
    private void PickAnElement(Element type) {
        ScaleDownElements();
        switch (type.Type) {
            case Elements.Electric:
                AdjustScale(electric);
                break;
            case Elements.Ice:
                AdjustScale(ice);
                break;
            case Elements.Fire:
                AdjustScale(fire);
                break;
            default:
                AdjustScale(normal);
                break;
        }
    }
    private void PickAWeapon(string val) {
        ScaleDownWeapons();
        switch (val) {
            case "Sword":
                AdjustScale(sword);
                break;
            case "Wand":
                AdjustScale(wand);
                break;
        }
    }
    private void AdjustScale(GameObject target) {
        target.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private void ScaleDownElements() { 
        electric.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        normal.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        fire.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        ice.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
    private void ScaleDownWeapons() {
        sword.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        wand.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
}
