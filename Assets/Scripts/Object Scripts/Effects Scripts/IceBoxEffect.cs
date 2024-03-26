using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoxEffect : MonoBehaviour
{
    [SerializeField] private GameObject FreezeBox;
    private void OnTriggerEnter(Collider other) {
        EnemyBody enemy;
        if (enemy=other.GetComponent<EnemyBody>()) {
            enemy.Body.Froze();
            Instantiate(FreezeBox, enemy.Body.transform);
        }
    }
}
