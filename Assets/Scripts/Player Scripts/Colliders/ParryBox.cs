using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParryBox : MonoBehaviour
{
    public static event UnityAction parry; 
    public static event UnityAction<int> parryEffect;
    public static event UnityAction parryVolume;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetPlayer();
    }

    private void OnTriggerEnter(Collider other) {
        // parryEffect.Invoke(7);
        //parry.Invoke();
        if(other.GetComponent<EnemyHitBox>())
            player.Anim.SetTrigger("Redirect");
        //Enemy enemy=other.GetComponent<EnemyHitBox>().Enemy;
        

        //Vector3 v3=enemy.transform.position-player.transform.position;
        
        //player.transform.DOLookAt(new Vector3(0,v3.y,0),0.1f);

        //parryVolume.Invoke();
        //Time.timeScale = 0.4f;
        //StartCoroutine(waitToStop());
        Debug.Log("Player got parried");
    }
    IEnumerator waitToStop() {
        YieldInstruction wait = new WaitForSeconds(2);
        yield return wait;
        Debug.Log("end stop");
        Time.timeScale =1f;
        //parryEffect.Invoke(10);
    }
}
