using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null) {

            enemy.Hit=true;
        }
        else {

        }
    }
}
