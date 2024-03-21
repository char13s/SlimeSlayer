using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputs : MonoBehaviour
{
    Player player;
    [SerializeField] Weapon ItemA;
    [SerializeField] Weapon ItemB;
    [SerializeField] Elements type;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }
    private void OnMove(InputValue value) {
        player.Anim.SetFloat("XInput", value.Get<Vector2>().x);
        player.Anim.SetFloat("YInput", value.Get<Vector2>().y);
    }
    private void OnItemA() {
        ItemA.UseItem(type);
    }
    private void OnItemB() {
        ItemB.UseItem(type);
    }
    private void OnDodges() { 
    
    }
    private void OnInteract() { 
    
    }
    private void OnSwitchItemA() { 
    }
    private void OnSwitchItemB() { 
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
    
    }
    void OnDRight() {
        type = Elements.Electric;
    }
    void OnDLeft() {
        type = Elements.Ice;
    }
    void OnDUp() {
        type = Elements.Fire;
    }
    void OnDDown() {
        type = Elements.Normal;
    }
}
