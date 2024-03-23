using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    // Start is called before the first frame update
    private void OnEnable() {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
