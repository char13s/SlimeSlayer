using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DodgeBox : MonoBehaviour
{
    public static event UnityAction<bool> dodge;
    private void OnTriggerEnter(Collider other) {
        dodge.Invoke(true);
        print("dodge");
    }
}
