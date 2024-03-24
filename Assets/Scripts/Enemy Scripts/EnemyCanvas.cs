using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] Slider hpSlider;
    [SerializeField] Image fillRef;
    [SerializeField] Slider defSlider;
    [SerializeField] private GameObject cam;
    GameManager player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.GetManager();
    }
    private void Update() {
        FacePlayer();
    }
    private void FacePlayer() { 
        Vector3 direction = cam.transform.position - transform.position;
        Quaternion qTo;
        qTo = Quaternion.LookRotation(direction);
        transform.rotation = qTo;
    }
    public void SetEnemyHealth() {
        if (hpSlider.value < (hpSlider.maxValue / 4)) {
            
            fillRef.color = Color.yellow;
        }
    }
    public void SetDefMeter() { 
    
    }
}
