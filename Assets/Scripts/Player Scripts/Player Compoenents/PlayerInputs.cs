using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputs : MonoBehaviour
{
    Player player;
    [SerializeField] Weapon ItemA;
     Element type;
    [SerializeField] private List<Element> elements;
    [SerializeField] private List<Weapon> weapons;
    byte currentType;
    byte currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        ItemA = weapons[currentWeapon];ItemA.gameObject.SetActive(true);
        type = elements[0];
    }
    private void OnMove(InputValue value) {
        player.Anim.SetFloat("XInput", value.Get<Vector2>().x);
        player.Anim.SetFloat("YInput", value.Get<Vector2>().y);
    }
    private void OnItemA() {
        ItemA.UseItem(type.Type);
    }
    private void OnItemB() {
        player.Anim.Play("Guard");
    }
    private void OnDodges() { 
    
    }
    private void OnInteract() { 
    
    }
    private void OnSwitchItemA() {
        currentWeapon++;
        ItemA.gameObject.SetActive(false);
        if (currentWeapon == weapons.Count) {
            currentWeapon = 0;
        }
        ItemA = weapons[currentWeapon];
        ItemA.gameObject.SetActive(true);
    }
    private void OnSwitchItemB() {
        player.Plock.SwitchTarget(1);
    }
    private void OnLockOn(InputValue input) {
        if (input.isPressed) {
            player.LockedOn = true;
        }
        else {
            player.LockedOn = false;
        }
    }
    private void OnElementWheel() {
        currentType++;
        if (currentType == elements.Count) {
            currentType = 0;
        }
        type = elements[currentType];
    }
    void OnDRight() {
        //type = Elements.Electric;
    }
    void OnDLeft() {
        //ype = Elements.Ice;
    }
    void OnDUp() {
       // type = Elements.Fire;
    }
    void OnDDown() {
       // type = Elements.Normal;
    }
}
