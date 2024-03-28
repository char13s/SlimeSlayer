using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject caster;
    // Start is called before the first frame update
    private void OnEnable() {
        GameObject blast=Instantiate(effect, transform.position, transform.rotation);
        blast.transform.forward = transform.forward;
    }
    private void Update() {
        //transform.rotation = transform.parent.transform.rotation;
    }
}
