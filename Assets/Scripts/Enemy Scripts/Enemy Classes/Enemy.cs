using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#pragma warning disable 0649
//[RequireComponent(typeof(EnemyTimelines))]
[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    private EnemyAiStates state;
    public enum EnemyType { soft, hard, absorbent }
    [SerializeField] private EnemyType type;
    private StatusEffects status;
    public event UnityAction onDefeat;
    public static event UnityAction<int> sendDmg;
    public enum EnemyAiStates { Null, Idle, Attacking, Chasing, ReturnToSpawn, Dead, Hit, UniqueState, UniqueState2, UniqueState3, UniqueState4, StatusEffect, Grabbed, Staggered };
    public enum EnemyHealthStatus { FullHealth, MeduimHealth, LowHealth }
    EnemyHealthStatus healthStatus;

    [Header("Enemy Health Bar")]
    #region Enemy Health Bar
    [SerializeField] private GameObject canvas;
    //[SerializeField] private Text levelText;
    [SerializeField] private GameObject lockOnArrow;
    [SerializeField] private Slider EnemyHp;
    [SerializeField] private Slider defMeter;
    #endregion
    #region Special Effects
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float reach;
    private float distanceGround;
    #endregion
    [Space]
    [Header("Enemy Parameters")]
    //[SerializeField] private int level;
    [SerializeField] private int attackDelay;
    //[SerializeField] private int baseExpYield;
    //[SerializeField] private int baseHealth;
    [SerializeField] private float attackDistance;
    [SerializeField] private bool standby;
    [SerializeField] private float gravity;
    [SerializeField] private float speed;
    [SerializeField] private int orbWorth;
    [Space]
    [Header("Object Refs")]
    [SerializeField] private GameObject hitSplat;
    [SerializeField] private GameObject shieldObj;
    private int stagger;

    // [SerializeField] private GameObject model;
    [SerializeField] private GameObject skinnedMesh;
    [SerializeField] private GameObject finisherTrigger;
    [Space]
    [Header("Effects Refs")]
    [SerializeField] private GameObject SpawninEffect;
    #region Script References
    //internal StatusEffects status = new StatusEffects();
    [SerializeField]
    internal StatsController stats = new StatsController();
    //private Player pc;
    private Player zend;
    [SerializeField] private Animator anim;
    private EnemyTimelines timelines;
    //private AudioSource sound;
    private CharacterController charCon;
    #endregion

    #region Coroutines
    private Coroutine hitCoroutine;
    private Coroutine attackCoroutine;
    private Coroutine attackingCoroutine;
    private Coroutine recoveryCoroutine;
    private Coroutine guardCoroutine;
    #endregion
    //private byte eaten;
    private bool attacking;
    private bool attack;
    private bool walk;
    private bool hit;
    private bool lockedOn;
    private bool dead;
    private bool lowHealth;
    private bool parry;
    private bool timelining;
    private int shield;
    // [SerializeField] private bool weak;

    private bool striking;
    [SerializeField] private int flip;
    private static List<Enemy> enemies = new List<Enemy>(32);
    private bool grounded;

    [SerializeField] private bool boss;
    private bool frozen;

    public static event UnityAction<Enemy> onAnyDefeated;
    public static event UnityAction onAnyEnemyDead;
    public static event UnityAction onHit;
    public static event UnityAction guardBreak;
    public static event UnityAction<AudioClip> sendsfx;
    public static event UnityAction<int> sendOrbs;
    public static event UnityAction remove;
    public static event UnityAction add;
    #region Getters and Setters
    public int Health { get { return stats.Health; } set { stats.Health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return stats.HealthLeft; } set { stats.HealthLeft = Mathf.Max(0, value); UIMaintence(); canvas.GetComponent<EnemyCanvas>().SetEnemyHealth(); if (stats.HealthLeft <= 0 && !dead) { Dead = true; } } }

    public bool Attack { get => attack; set { attack = value; Anim.SetBool("Attack", attack); } }
    protected bool Walk { get => walk; set { walk = value; Anim.SetBool("Walking", walk); } }

    public bool Hit {
        get => hit; set {
            hit = value;
            Anim.SetBool("Hurt", hit); if (onHit != null) {
                onHit();
            }
            if (hit) { OnHit(); }
        }
    }
    public EnemyAiStates State { get => state; set { state = value; States(); } }
    public bool Grounded { get => grounded; set { grounded = value; Anim.SetBool("Grounded", grounded); } }
    public bool LockedOn {
        get => lockedOn; set {
            lockedOn = value; if (lockedOn && !dead) {

                canvas.SetActive(true);
                lockOnArrow.SetActive(true);
            }
            else {
                lockOnArrow.SetActive(false);
                canvas.SetActive(false);
            }

        }
    }
    public bool Dead {
        get => dead;
        private set {
            dead = value;
            if (dead) {
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("_onOrOff", 1);
                GetComponentInChildren<SkinnedMeshRenderer>().material.SetFloat("dead", 1);

                OnDefeat();
                Anim.SetBool("Hurt", dead);
                if (onAnyDefeated != null) {
                    onAnyDefeated(this);
                }
                if (onAnyEnemyDead != null) {
                    onAnyEnemyDead();
                }

            }
        }
    }
    public bool Boss { get => boss; set => boss = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public static List<Enemy> Enemies { get => enemies; set { enemies = value; } }
    public bool Frozen { get => frozen; set { frozen = value; if (frozen) { FreezeEnemy(); } } }
    private static int totalCount;
    private float distance;

    public int AttackDelay { get => attackDelay; set => attackDelay = value; }
    //public Rigidbody Rbody { get => rbody; set => rbody = value; }
    public bool Standby { get => standby; set { standby = value; StandbyState(); } }

    //public Rigidbody Rbody { get => rbody; set => rbody = value; }
    public bool Parry { get => parry; set { parry = value; if (value) { Anim.Play("Parried"); } } }
    public CharacterController CharCon { get => charCon; set => charCon = value; }
    public Player Zend { get => zend; set => zend = value; }
    public float Distance { get => distance; set => distance = value; }
    // public GameObject Model { get => model; set => model = value; }
    public GameObject SkinnedMesh { get => skinnedMesh; set => skinnedMesh = value; }
    public static int TotalCount { get => totalCount; set { totalCount = value; } }

    public int Stagger { get { return stats.StaggerLeft; } set { stats.StaggerLeft = Mathf.Max(0, value); UIMaintence(); } }

    public bool Timelining { get => timelining; set => timelining = value; }
    public int Shield { get => shield; set { shield = Mathf.Max(0, value); CheckShield(); } }

    public StatusEffects Status { get => status; set { StatusControl(); status = value; } }
    #endregion



    public virtual void Awake() {

        //Anim = Model.GetComponent<Animator>();
        //sound = GetComponent<AudioSource>();
        //StatusEffects.onStatusUpdate += StatusControl;
        
        StatCalculation();
        state = EnemyAiStates.Idle;
        stats.staggercheck += StaggerCheck;

    }
    public void LayerSwitch(bool val) {
        if (val) {
            gameObject.layer = 13;
        }
        else {
            gameObject.layer = 9;
        }
    }
    private void CheckShield() {
        if (shield <= 0 && shieldObj) {
            shieldObj.SetActive(false);
            //sheildBreak vfx
        }
    }
    public virtual void ShieldUp() {
        Shield = Stagger;
        if (shieldObj) {
            shieldObj.SetActive(true);
        }
    }
    // Start is called before the first frame update
    public virtual void OnEnable() {

        //Instantiate(SpawninEffect,transform.position,Quaternion.identity);
        //ZaWarudo.timeFreeze += FreezeEnemy;

        //if(Player.GetPlayer()!=null)
        //zend = Player.GetPlayer();

        Player.onPlayerDeath += OnPlayerDeath;

        //ReactionRange.dodged += SlowEnemy;
        //Enemies.Add(this);
        //if (add != null) {
        //    add();
        //}
        timelines = GetComponent<EnemyTimelines>();
        HealthLeft = stats.Health;
        StandbyState();
        //PlayerAnimationEvents.letGo += UnSetParent;
        //TimelineManager.timelining += TimeliningControl;
        //DialogueControl.cutscening += TimeliningControl;
        Anim.Play("Spawn In");
    }
    private void OnDisable() {
        TotalCount = Enemies.Count;
        //ZaWarudo.timeFreeze -= FreezeEnemy;

        Player.onPlayerDeath -= OnPlayerDeath;

        //ReactionRange.dodged -= SlowEnemy;
        //PlayerAnimationEvents.letGo -= UnSetParent;
        //TimelineManager.timelining -= TimeliningControl;
        //DialogueControl.cutscening -= TimeliningControl;
        Enemies.Remove(this);
        if (remove != null) {
            remove();
        }
    }
    public virtual void Start() {
        CharCon = GetComponent<CharacterController>();
        // distanceGround = GetComponent<Collider>().bounds.extents.y;
        Zend = Player.GetPlayer();
        TotalCount = Enemies.Count;
    }
    public void TimeliningControl(bool val) {
        Timelining = val;
        print("Boss timelingggg");
    }
    private void EnemiesNeedToRespawn(int c) {
        Destroy(gameObject);
    }
    // Update is called once per frame
    public virtual void Update() {
        States();
        if (Zend != null)
            Distance = Vector3.Distance(Zend.transform.position, transform.position);
        
        //canvas.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
    public virtual void FixedUpdate() {

        //transform.LookAt(Zend.transform.position);
        //transform.rotation = Quaternion.LookRotation(delta);
        //if (!hit)
            //CharCon.Move(new Vector3(0, -gravity, 0) * Time.deltaTime);
        Grounded = CharCon.isGrounded;
    }
    private void StatusControl() {
        if (!dead) {
            switch (Status.Status) {
                case StatusEffects.Statuses.stunned|StatusEffects.Statuses.frozen:
                    State = EnemyAiStates.StatusEffect;
                    if (!dead) {
                        Anim.speed = 0;
                    }
                    break;
            }
        }
    }
    private void StatCalculation() {
        Health = (int)(stats.BaseHealth * 2f);
        stats.Attack = (int)(stats.BaseAttack * 1.2f);
        stats.Defense = (int)(stats.BaseDefense * 1.5f);
        stats.StaggerLeft = (int)(stats.Stagger * 1.2f);
        Stagger = stats.StaggerLeft;
    }
    //public static Enemy GetEnemy(int i) => Enemies[i];
    public void OnPlayerDeath() {
        Enemies.Clear();
    }
    #region Reactions
    public void StunHit() {
        anim.Play("StunHit");
    }
    public void Knocked() {
        //Vector3 delta = (Zend.transform.position - transform.position);
        //delta.y = 0;
        //// transform.LookAt(Zend.transform.position);
        //transform.rotation = Quaternion.LookRotation(delta);
        timelines.KnockedBack();//swap this with an anim controlled interaction
    }
    public void CancelKnocked() {
        print("smack that all up on the ");
        anim.Play("Drop Fast");
    }
    private IEnumerator SendToGround() {
        while (isActiveAndEnabled && !grounded) {
            yield return null;
            charCon.Move(new Vector3(0, -15, 0));
        }
    }
    public void KnockedUp() {

        if (shield <= 0) { 
        Anim.Play("KnockedUp");
        //Anim.ResetTrigger("AirHit");
        }
    }
    public void KnockedBack() {
        if (shield <= 0)
            Anim.Play("KnockedBack");
    }
    public void KnockedDown() {
        Anim.Play("KnockedDown");
    }
    public void PullIn() {
        Anim.Play("Pull In");
    }
    private void KillEnemy() {
        Destroy(this);
    }
    public void Burned() {
        if (Status.Status == StatusEffects.Statuses.neutral) {
            Status.Status = StatusEffects.Statuses.burned;
            //Add a red glow material
            StartCoroutine(BurnEnemy());
        }
    }
    public void Froze() {
        //if (Status.Status == StatusEffects.Statuses.neutral) {
            //Status.Status = StatusEffects.Statuses.frozen;
            //Add a baby blue material and ice particles
            StartCoroutine(FrozeEnemy());
        //}
    }
    public void Pararlyzed() {
        if (Status.Status == StatusEffects.Statuses.neutral) {
            Status.Status = StatusEffects.Statuses.stunned;
            //Add a yellow material and electricity particles
            StartCoroutine(StunEnemy());
        }
    }
    #region old freeze
    private void SwitchFreezeOn() {
        Frozen = true;
    }
    private void FreezeEnemy() {
        Debug.Log("Froze");
        Anim.SetFloat("Speed", 0.1f);
        //anim.speed = 0;
        //State = EnemyAiStates.Null;
        StartCoroutine(UnFreeze());
    }
    private IEnumerator UnFreeze() {
        YieldInstruction wait = new WaitForSeconds(4);
        yield return wait;
        Anim.SetFloat("Speed", 0.1f);
        UnFreezeEnemy();
    }
    private void UnFreezeEnemy() {
        anim.speed = 1;
        State = EnemyAiStates.Idle;
    }
    private void NullEnemy() {
        State = EnemyAiStates.Null;
    }
    #endregion
    #endregion
    #region Event handlers
    public void CharConControl() {
        charCon.enabled = false;
        StartCoroutine(waitToTrue());
    }
    IEnumerator waitToTrue() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        charCon.enabled = true;
    }
    #endregion

    #region State Logic
    public virtual void StateSwitch() {
        if (!Timelining) {
            if (HealthLeft < Health / 4) {
                lowHealth = true;
                //finisherTrigger.SetActive(true);
            }
            if (state == EnemyAiStates.Chasing) {
                SwitchToAttack();
                BackToIdle();
            }
            if (state == EnemyAiStates.Idle) {//What happens in Idle
                SwitchToAttack();
                ChasePlayer();
            }
            if (state == EnemyAiStates.Attacking) {
                ChasePlayer();
                BackToIdle();
                //SwitchToAttack();
            }
        }
    }
    public virtual void SwitchToAttack() {
        if (Distance < attackDistance && !dead && !Hit) {
            State = EnemyAiStates.Attacking;
        }
        else {
            attacking = false;
        }
    }
    public virtual void ChasePlayer() {
        if (Distance > attackDistance && !dead && !Hit) {
            State = EnemyAiStates.Chasing;
        }
        else {
            Walk = false;
        }
    }
    public virtual void BackToIdle() {
        if (Distance > 10f | AttackDelay > 0) {
            State = EnemyAiStates.Idle;
        }
    }
    public virtual void StaggerCheckState() {
        if (stats.StaggerLeft <= 0) {
            recoveryCoroutine = StartCoroutine(WaitForStaggerRecovery());
        }
    }
    public virtual void SuperAttack() {

    }
    public virtual void States() {
        switch (state) {
            case EnemyAiStates.Idle:
                Idle();
                break;
            case EnemyAiStates.Attacking:
                //Rbody.velocity = new Vector3(0, 0, 0);
                StartCoroutine(waitToAttack());
                break;
            //LowHealth();
            case EnemyAiStates.Chasing:
                Walk = true;

                //Chasing();
                break;
            //case EnemyAiStates.Staggered:
            //    //Call recovery coroutine
            //    recoveryCoroutine = StartCoroutine(WaitForStaggerRecovery());
            //    //set animation to stunned
            //    anim.SetBool("Stagger", true);
            //    break;
            default:
                break;
        }
    }
    IEnumerator waitToSuperAttack() {
        int num = Random.Range(7, 10);
        YieldInstruction wait = new WaitForSeconds(num);
        yield return wait;
        Anim.Play("Super");
    }
    IEnumerator waitToAttack() {
        int num = Random.Range(1, 3);
        YieldInstruction wait = new WaitForSeconds(num);
        yield return wait;
        Anim.SetTrigger("Attack 0");
        AttackDelay = 3;
    }
    public virtual void Idle() {
        Walk = false;
        attackDelay = 0;
    }
    public virtual void Chasing() {

        Walk = true;
        Vector3 delta = Zend.transform.position - transform.position;
        delta.y = 0;

        transform.rotation = Quaternion.LookRotation(delta);
        CharCon.Move(transform.forward * speed * Time.deltaTime);
    }
    public virtual void AttackRot() {
        Vector3 delta = Zend.transform.position - transform.position;
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
    }
    private void StandbyState() {
        if (standby) {
            State = EnemyAiStates.Null;
        }
    }
    #endregion
    private void UIMaintence() {

        //levelText.GetComponent<Text>().text = "Lv. " + stats.Level;
        EnemyHp.maxValue = stats.Health;
        EnemyHp.value = stats.HealthLeft;
        defMeter.maxValue = stats.Stagger;
        defMeter.value = Shield;
    }
    public virtual void OnHit() {
        if (Shield <= 0) {
            if (!Grounded) {
                Anim.Play("AirHit");

            }
            else {
                print("hit was herrrrr");
                Anim.Play("Hurt");

            }
        }
    }


    #region Coroutines
    IEnumerator waitToFall() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        //rbody.useGravity = true;
    }
    IEnumerator BurnEnemy() {
        int counter = 5;
        YieldInstruction wait = new WaitForSeconds(1);
        while (isActiveAndEnabled && counter > 5) {
            yield return wait;
            counter--;
            //Particle come up
            HealthLeft -= (int)(Health * 0.05f);
        }
        Status.Status = StatusEffects.Statuses.neutral;
    }
    IEnumerator FrozeEnemy() {
        Anim.speed = 0;
        YieldInstruction wait = new WaitForSeconds(5);
        yield return wait;
        Anim.speed = 1;
        GameObject frozeBox=gameObject.transform.Find("FreezeBox(Clone)").gameObject;
        Destroy(frozeBox.gameObject);
    }
    IEnumerator StunEnemy() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        Anim.speed = 0;
    }
    #endregion
    #region status stuff
    private void SlowEnemy() {

        FreezeEnemy();
        print("Wth?????");
    }
    public void OnDefeat() {
        //onAnyDefeated(this);
        if (onDefeat != null) {
            onDefeat();
        }
        Drop();
        Enemies.Remove(this);
        if (deathEffect != null) {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        //sendOrbs.Invoke(orbWorth);
        //deathEffect.transform.position = transform.position;
        Destroy(gameObject, 0.5f);
        //drop.transform.SetParent(null);
    }
    public void Grabbed() {
        charCon.enabled = false;
    }
    public void UnSetParent() {
        charCon.enabled = true;
        transform.SetParent(null);
    }
    public void UnsetHit() {
        Hit = false;
        //Anim.ResetTrigger("Attack 0");
        State = EnemyAiStates.Idle;
    }
    #endregion
    public void CalculateDamage(float addition, HitBoxType dmgType,float multiplier) {
        if (!dead) {
            float dmgModifier = 1;
            dmgModifier = DmgMod(dmgModifier, dmgType)*multiplier;
            int dmg;
            if (Shield > 0) {
                dmg = 0;
                Shield -= (int)(Zend.stats.Attack);
                //effect for shield getting hit
                //Instantiate(shieldHit, transform.position,Quarternion.idnetity);
            }
            else {
                dmg = (int)Mathf.Clamp(((Zend.stats.Attack * addition)) * dmgModifier, 1, 999);
                if (sendDmg != null) {
                    sendDmg(dmg);
                }
            }
            HealthLeft -= dmg;
            //HitText hitSplat= new HitText();
            //Debug.Log(hitSplat.Text.ToString());
            hitSplat.GetComponent<HitText>().Text = dmg.ToString();
            Instantiate(hitSplat, transform.position+new Vector3(0,1,0), Quaternion.identity);
            //Hit = true;

            if (HealthLeft <= Health / 4 && !lowHealth) {
                lowHealth = true;
            }
        }
    }
    private float DmgMod(float dmg, HitBoxType dmgType) {
        switch (type) {
            case EnemyType.absorbent:
                switch (dmgType) {
                    case HitBoxType.Heavy:
                        return dmg;
                    case HitBoxType.Magic:
                        return dmg / 4;
                    default:
                        return dmg * 1.5f;
                }
            case EnemyType.soft:
                switch (dmgType) {
                    case HitBoxType.Heavy:
                        return dmg / 4;
                    case HitBoxType.Magic:
                        return dmg;
                    default:
                        return dmg * 1.5f;
                }
            case EnemyType.hard:
                switch (dmgType) {
                    case HitBoxType.Heavy:
                        return dmg * 1.5f;
                    case HitBoxType.Magic:
                        return dmg;
                    default:
                        return dmg / 4;
                }
        }
        return dmg;
    }
    public void CalculateAttack(int extraDmg) {
        Zend.stats.HealthLeft -= Mathf.Max(1, stats.Attack + extraDmg);
    }
    private void StaggerCheck() {
        print("Stagger check");
        if (stats.StaggerLeft <= 0) {
            print("Stagger broke");
            Anim.Play("Parried");
            recoveryCoroutine = StartCoroutine(WaitForStaggerRecovery());
            state = EnemyAiStates.Staggered;
        }
    }
   //public void TeleportPlayer() {
   //    //turnoff characterController
   //    Debug.Log("ported");
   //    Zend.CharCon.enabled = false;
   //    //move player to teleportTo
   //
   //    StartCoroutine(WaitToCharCon());
   //}
   //IEnumerator WaitToCharCon() {
   //    YieldInstruction wait = new WaitForSeconds(0.1f);
   //    yield return wait;
   //    Zend.CharCon.enabled = true;
   //}
    IEnumerator WaitForStaggerRecovery() {
        YieldInstruction wait = new WaitForSeconds(5f);
        while (isActiveAndEnabled && Stagger != stats.Stagger) {
            yield return wait;
            Stagger = stats.Stagger;
        }
        if (Stagger == stats.Stagger) {
            state = EnemyAiStates.Idle;
        }
    }
    public void HitGuard() {
        if (Zend.stats.MPLeft > 0) {
            Zend.stats.MPLeft -= Mathf.Max(1, stats.Attack);
            if (sendsfx != null) {

            }
        }
        else {

            if (guardBreak != null) {
                guardBreak();
            }
        }
    }
    private void Drop() {
        int exp = stats.BaseHealth * stats.ExpYield;
        sendOrbs.Invoke(orbWorth);
    }

}