using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GuardBox : MonoBehaviour
{
    public static event UnityAction<int> sendAmt;
    [SerializeField] private GameObject effects;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        Instantiate(effects, transform.position, Quaternion.identity);
        sendAmt.Invoke(-1);
    }
}
