using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class EnemyGroundCheck : MonoBehaviour
{
    private AudioClip landing;
    //public static event UnityAction<AudioClip> landed;
    [SerializeField] private float reach;
    private float distanceGround;
    [SerializeField]private Enemy enemy;
    private Ray ray;
    // Start is called before the first frame update
    void Start() {
        distanceGround = GetComponent<Collider>().bounds.extents.y;
        //enemy=GetComponent<Enemy>();
    }
    private void FixedUpdate() {
        RaycastHit hit;
        Debug.DrawRay(transform.position, -Vector2.up,Color.red ,distanceGround + reach);
        if (Physics.Raycast(transform.position, -Vector2.up,out hit , distanceGround + reach)) {
            enemy.Grounded = true;

            //Gizmos.DrawRay(ray);
        }
        else {

            enemy.Grounded = false;
        }
        //+" is Enemy";
    }
}
