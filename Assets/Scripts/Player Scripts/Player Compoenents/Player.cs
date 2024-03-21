using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public static Player instance;
    private Animator anim;
    private CharacterController charCon;
    internal Stats stats = new Stats();

    public static event UnityAction onPlayerDeath;

    private bool moving;
    private bool blocking;
    private bool lockedOn;
    private bool hasTarget;

    [SerializeField] private GameObject aimmingPoint;
    public bool Moving { get => moving; set { moving = value; Anim.SetBool("Moving", value); } }

    public bool Blocking { get => blocking; set => blocking = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public bool LockedOn { get => lockedOn; set { lockedOn = value; Anim.SetBool("Lockon",value); } }

    public bool HasTarget { get => hasTarget; set => hasTarget = value; }
    public GameObject AimmingPoint { get => aimmingPoint; set => aimmingPoint = value; }
    public CharacterController CharCon { get => charCon; set => charCon = value; }

    public static Player GetPlayer() => instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        charCon = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
