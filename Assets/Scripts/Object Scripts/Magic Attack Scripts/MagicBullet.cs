using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeSpan;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,lifeSpan);
        direction = Player.GetPlayer().transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
