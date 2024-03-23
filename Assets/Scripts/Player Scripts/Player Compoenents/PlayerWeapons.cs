using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject wand;

    public GameObject Sword { get => sword; set => sword = value; }
    public GameObject Wand { get => wand; set => wand = value; }
    public void SetWeapon(string weapon) {
        switch (weapon) {
            case "Sword":
                
                break;
        }
    }
}
