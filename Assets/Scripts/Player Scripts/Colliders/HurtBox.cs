using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HurtBox : MonoBehaviour
{
    public static event UnityAction<float> shakeCam;
    public static event UnityAction hitPanel;
    public static event UnityAction<float,float> vibe;
    Player player;
    private void Start() {
        player = Player.GetPlayer();
    }
    private void OnTriggerEnter(Collider other) {
        if (vibe != null) {
            vibe(0.45f,0.45f);
        }
        if (shakeCam != null) {
            shakeCam(2);
        }
        if (hitPanel != null) {
            hitPanel();
        }
        if (other.GetComponent<EnemyWeakerHit>()) {
            player.Anim.Play("WeakHitReaction");
        }
        if (other.GetComponent<EnemyHeavyHitter>()) {
            player.Anim.Play("HeavyHitReaction");
        }
    }
}
