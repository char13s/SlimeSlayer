using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class EnemyTimelines : MonoBehaviour
{
    [SerializeField] private PlayableDirector knocked;
    [SerializeField] private PlayableDirector knockedUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void KnockedBack() {
        knocked.Play();
    }
    public void CancelKnockUp() {
        knockedUp.Stop();
    }
    public void KnockUp() {
        knockedUp.Play();
    }
}
