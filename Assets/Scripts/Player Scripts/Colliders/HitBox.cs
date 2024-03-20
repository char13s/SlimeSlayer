using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//using XInputDotNetPure;
#pragma warning disable 0649
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class HitBox : MonoBehaviour
{
    private Player player;

    [SerializeField] private HitBoxType type;
    [SerializeField] private Elements elementType;
    [SerializeField] private GameObject effects;
    [SerializeField] private float additionalDamage;
    [SerializeField] private bool fireball;
    [SerializeField] private bool shakeOrNot;
    [SerializeField] private bool auraAttack;
    [SerializeField] private bool destroyAfter;
    [SerializeField] private float hitStop;
    [SerializeField] private bool hasType;
    private AudioSource audio;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject enemyImAttacking;

    public static UnityAction onEnemyHit;
    public static event UnityAction<Enemy, float> sendFlying;
    public static event UnityAction<AudioClip> sendsfx;
    public static event UnityAction<int> sendHitReaction;
    public static event UnityAction<float> shake;
    public static event UnityAction weaponHit;
    public static event UnityAction<float> zawarudo;
    public static event UnityAction<float, float> vibe;
    bool isWaiting;
    public GameObject EnemyImAttacking { get => enemyImAttacking; set => enemyImAttacking = value; }
    public HitBoxType Type { get => type; set => type = value; }
    public float AdditionalDamage { get => additionalDamage; set => additionalDamage = value; }
    public bool HasType { get => hasType; set => hasType = value; }

    // Start is called before the first frame update
    void Start() {
        //player = Player.GetPlayer();
        //audio = player.Sfx;

    }
    private void OnDisable() {

        enemies.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        //if (!enemies.Contains(other.gameObject)) {
        if (effects != null)
            Instantiate(effects, other.gameObject.transform);

        if (other != null && other.GetComponent<EnemyBody>() && !enemies.Contains(other.gameObject)) {
            //weaponHit.Invoke();
            if (fireball) {
                Destroy(gameObject);
            }
            if (vibe != null) {
                vibe(0.2f,0.65f);
            }
            if (shakeOrNot) {
                if (shake != null)
                    shake(4);
            }
            else {
                if (shake != null)
                    shake(0.5f);
            }
            if (!auraAttack) {
                if (onEnemyHit != null) {
                    onEnemyHit();
                }
            }
            if(zawarudo!=null&&hitStop>0)
                zawarudo(hitStop);
            if (destroyAfter) {
                Destroy(gameObject,1);
            }
            enemies.Add(other.gameObject);
            StartCoroutine(StopRumble());
        }
    }
    private IEnumerator StopRumble() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        //GamePad.SetVibration(0, 0, 0);
    }
    
}
