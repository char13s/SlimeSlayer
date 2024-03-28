using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{

    [SerializeField] private static List<ItemData> items = new List<ItemData>();
    private List<GameObject> slots = new List<GameObject>();
    [SerializeField] private GameObject emptyItem;
    private static Inventory instance;
    public static List<ItemData> Items { get => items; set => items = value; }

    public static event UnityAction saveGame;
    public static event UnityAction<GameObject> sendObj;
    public static event UnityAction updateThisInvent;
    public static Inventory GetInventory() => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }

    }
    private void Start() {
        
        //ItemSlot.tellInventSendFirst += CheckFirstSlot;
        print(Items.Count);
    }
    public void AddItems(ItemData item) {
        if (HasItem(item.ID)) {
            ItemData i = GetItem(item);
            i.Quantity++;
            print(i.ItemName + " " + i.Quantity);
        }
        else {
            Items.Add(item);
            item.Quantity = 0;
            item.Quantity++;
            print(item.ItemName + " " + item.Quantity);
            if (saveGame != null) {
                saveGame();
            }
        }
        if (updateThisInvent != null)
            updateThisInvent();
    }
    public void RemoveItem(ItemData item) {
        item.Quantity--;
        if (item.Quantity == 0) {
            Items.Remove(item);
        }
        if (updateThisInvent != null)
            updateThisInvent();
    }
    public bool HasItem(int ID) {
        foreach (ItemData i in Items) {
            if (i.ID == ID) {
                return true;
            }
        }
        return false;
    }
    public ItemData GetItem(ItemData item) {
        foreach (ItemData i in Items) {
            if (i.ID == item.ID) {
                return i;
            }
        }
        return null;
    }
    public void GenerateInvent(GameObject invent) {
        ClearInventSpace(invent);
        print(Items.Count);
        foreach (ItemData data in Items) {
            GameObject item = Instantiate(emptyItem, invent.transform);
           // item.GetComponent<RelicHolder>().Relic = data;
            ItemInvent itemVent = item.GetComponent<ItemInvent>();
            itemVent.Data = data;
            //itemVent.ItemSprite.sprite = SpriteAssign.GetSprite().SetImage(data);
            slots.Add(item);
        }
    }
    private void CheckFirstSlot() {
        if (items[0] != null) {
            print(slots[0]);
            if (sendObj != null) {
                sendObj(slots[0]);
            }
        }
        else {
            print("invent empty");
        }
    }
    private void ClearInventSpace(GameObject invent) {
        foreach (Transform transform in invent.transform) {
            Destroy(transform.gameObject);
        }
        slots = new List<GameObject>();
    }
}
