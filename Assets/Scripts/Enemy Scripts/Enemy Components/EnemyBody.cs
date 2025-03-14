﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyBody : MonoBehaviour
{
    [SerializeField] private Element type;
    [SerializeField] private Enemy body;
    [SerializeField] private Material bodyMat;
    [SerializeField] private Material attMat;
    [SerializeField] private GameObject hitSound;

    [SerializeField] private SkinnedMeshRenderer mesh;

    public Enemy Body { get => body; set => body = value; }


    private void OnTriggerEnter(Collider other) {
        HitBox hitbox;
        if (hitbox=other.GetComponent<HitBox>()) {
            Attacked(other);
            if (!Body.Parry)
                if(hitSound)
                    Instantiate(hitSound);
            Body.CalculateDamage(hitbox.AdditionalDamage, hitbox.Type,ReadElement(hitbox.ElementType));
            //if(!hitbox.HasType)
                Body.OnHit();
        }
    }
    private void Attacked(Collider other) {
        mesh.material = attMat;
        StartCoroutine(waitToReset());
    }
    IEnumerator waitToReset() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        mesh.material = bodyMat;
    }
    private float ReadElement(Element element) {
        if (type.Weakness == element.Type) {
            return 2;
        }
        else if (type.Advantage == element.Type) {
            return 0.5f;
        }
        else {
            return 1;
        }
    }
}
