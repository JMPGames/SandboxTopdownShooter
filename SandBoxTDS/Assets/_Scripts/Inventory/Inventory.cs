using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InventoryCursor))]
[RequireComponent(typeof(PlayerEquipment))]
public class Inventory : MonoBehaviour {
    public static Inventory instance;

    public Slot[] inventorySlots = new Slot[25];

    public int Gold { get; set; }

    void Awake() {
        if(instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public bool InventoryIsFull() {
        return !inventorySlots.Any(slot => slot.IsEmpty);
    }

    public List<Item> CheckInventoryForStackableItemById(int id) {
        List<Item> stacks = new List<Item>();

        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].Item.Id == id && inventorySlots[i].Item.Stackable()) {
                stacks.Add(inventorySlots[i].Item);
            }
        }
        stacks.Sort((i1, i2) => i2.CurrentStackSize.CompareTo(i1.CurrentStackSize));
        return stacks;
    }

    public Item CheckInventoryForItemById(int id) {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].Item.Id == id) {
                return inventorySlots[i].Item;
            }
        }
        return null;
    }

    public bool AddItem(Item item) {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].IsEmpty) {
                inventorySlots[i].Add(item);
                return true;
            }
        }
        return false;
    }

    public void SaveData() { }
    public void LoadData() { }
}
