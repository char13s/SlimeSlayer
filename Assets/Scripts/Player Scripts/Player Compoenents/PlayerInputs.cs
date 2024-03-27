using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerInputs : MonoBehaviour
{
    Player player;
    Weapon ItemA;
    Element type;
    [SerializeField] private List<Element> elements;
    [SerializeField] private List<Weapon> weapons;
    byte currentType;
    byte currentWeapon;

    public static event UnityAction<Element> sendElement;
    public static event UnityAction<string> sendWeapon;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        ItemA = weapons[currentWeapon];ItemA.gameObject.SetActive(true);
        type = elements[0];
        if (sendElement != null) {
            sendElement(type);
        }
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
        player.Anim.SetTrigger("Jump");
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
        ItemA.HandleEffects(type);
        if (sendWeapon != null) {
            sendWeapon(ItemA.name);
        }
        
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
        ItemA.HandleEffects(type);
        if (sendElement != null) {
            sendElement(type);
        }
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
