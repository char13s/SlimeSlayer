using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputs : MonoBehaviour
{
    Weapon ItemA;
    Weapon ItemB;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnItemA() {
        ItemA.Attack();
    }
    private void OnItemB() {
        ItemB.Attack();
    }
    private void OnDodges() { 
    
    }
    private void OnInteract() { 
    
    }
    private void OnSwitchItemA() { 
    }
    private void OnSwitchItemB() { 
    }
    private void OnLockOn() { 
    
    }
}
