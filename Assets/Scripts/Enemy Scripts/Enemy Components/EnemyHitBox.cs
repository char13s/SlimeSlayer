using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
#pragma warning disable 0649
public class EnemyHitBox : MonoBehaviour
{
    private Enemy enemy;
    //[SerializeField] private GameObject effect;

    [SerializeField] private bool isProjectile;
    [SerializeField] private int extraDmg;
    [SerializeField] private GameObject soundEffect;
    [SerializeField] private GameObject initialSound;
    public Enemy Enemy { get => enemy; set => enemy = value; }

    // Start is called before the first frame update
    void Start() {
        //player = Player.GetPlayer();
        if(!isProjectile)
            Enemy = GetComponentInParent<Enemy>();
        if (initialSound != null)
            Instantiate(initialSound, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other) {
        if (soundEffect != null)
            Instantiate(soundEffect, transform.position, Quaternion.identity);
        // Instantiate(effect, transform.position, Quaternion.identity);
        if (other.GetComponent<HurtBox>()) {
            if (Player.GetPlayer().Blocking) {

            }
            else if (!isProjectile) {
                Enemy.CalculateAttack(extraDmg);
            }
            else { 
                Player.GetPlayer().stats.HealthLeft -= extraDmg;
            }
            
        }
        if (other.GetComponent<ParryBox>()&&!isProjectile) {
            Enemy.Parry=true;
        }
    }
}
