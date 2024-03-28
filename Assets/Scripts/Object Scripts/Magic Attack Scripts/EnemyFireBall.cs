using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyFireball : MonoBehaviour
{
    [SerializeField] private GameObject fireballHit;
    [SerializeField] private GameObject soundEffect;
    public static event UnityAction<int> dmg;
    private Vector3 direction;
    [SerializeField] private GameObject boom;

    public Vector3 Direction { get => direction; set => direction = value; }

    // Start is called before the first frame update
    private void Start() {
        Vector3 delta =   Player.GetPlayer().transform.position-transform.position;
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        //LayerMask.GetMask("Ground");
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    private void Update() {
        transform.position += transform.forward * 20 * Time.deltaTime;
    }

    //private int AdditionPower()=>Player.GetPlayer().stats.Level 
    private void OnTriggerEnter(Collider other) {
        if(fireballHit!=null)
            Instantiate(fireballHit, transform.position, transform.rotation);
        if (other.GetComponent<ParryBox>()) {
            
            
        }
        Destroy(gameObject);
    }
}
