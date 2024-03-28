using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    [SerializeField] private string itemName;
    [SerializeField] private int id;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int rarity;
    private int quantity;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }

    public int Rarity { get => rarity; set => rarity = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public int ID { get => id; set => id = value; }
}
