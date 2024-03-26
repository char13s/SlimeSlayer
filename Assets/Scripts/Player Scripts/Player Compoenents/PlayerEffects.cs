using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject iceEffect;
    [SerializeField] private GameObject electricEffect;

    public GameObject FireEffect { get => fireEffect; set => fireEffect = value; }
    public GameObject IceEffect { get => iceEffect; set => iceEffect = value; }
    public GameObject ElectricEffect { get => electricEffect; set => electricEffect = value; }
    public void HandleEffects(Element type) {
        ResetAllElements();
        switch (type.Type) {
            case Elements.Electric:
                ElectricEffect.SetActive(true);
                break;
            case Elements.Ice:
                IceEffect.SetActive(true);
                break;
            case Elements.Fire:
                FireEffect.SetActive(true);
                break;
        }
    }
    private void ResetAllElements() {
        FireEffect.SetActive(false);
        IceEffect.SetActive(false);
        ElectricEffect.SetActive(false);
    }
}
