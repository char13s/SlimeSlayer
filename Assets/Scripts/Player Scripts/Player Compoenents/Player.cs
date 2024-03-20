using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public static Player instance;
    private Animator anim;
    internal Stats stats = new Stats();
    public static event UnityAction onPlayerDeath;
    private bool moving;
    private bool blocking;
    public bool Moving { get => moving; set { moving = value; Anim.SetBool("Moving", value); } }

    public bool Blocking { get => blocking; set => blocking = value; }
    public Animator Anim { get => anim; set => anim = value; }

    public static Player GetPlayer() => instance;
    private void Awake() {
        Anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
