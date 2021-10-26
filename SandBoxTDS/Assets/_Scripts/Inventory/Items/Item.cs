using System.Collections.Generic;
using UnityEngine;

public enum ItemType { CONSUMABLE, RESOURCE, WEAPON, CHIP, QUEST, JUNK }

public class Item : GameObj {
    public ItemType ItemType { get { return itemType; } }
    public UIContainer Container { get; set; }

    public int MaxStackSize { get { return maxStackSize; } }
    public int CurrentStackSize { get; set; }

    int durability;

    #region Inspector Variables
    [SerializeField] ItemType itemType;
    [SerializeField] int price;
    [SerializeField] int maxStackSize;
    [SerializeField] int maxDurability;
    [SerializeField] int goldToRepairPerPoint;
    #endregion

    public bool StackIsFull() {
        return CurrentStackSize >= MaxStackSize;
    }

    public void Buy(int numberToBuy) {
        if (!CanAfford()) {
            return;
        }
        List<Item> stacksInInventory = Inventory.instance.CheckInventoryForItemById(Id);
        for (int i = 0; i < numberToBuy; i++) {
            if (CanAfford()) {
                if (stacksInInventory.Count > 0 && !stacksInInventory[0].StackIsFull()) {
                    if ((stacksInInventory[0].CurrentStackSize += 1) >= stacksInInventory[0].MaxStackSize) {
                        stacksInInventory.RemoveAt(0);
                    }
                    Inventory.instance.Gold -= price;
                    continue;
                }
                else if (!Inventory.instance.InventoryIsFull()) {
                    Inventory.instance.AddItem(this);
                    if (maxStackSize > 1) {
                        stacksInInventory.Add(this);
                    }
                    Inventory.instance.Gold -= price;
                    continue;
                }
            }
            break;
        }
    }

    public bool CanAfford() {
        return price <= Inventory.instance.Gold;
    }

    public void Sell(int numberToSell) {
        if (numberToSell >= CurrentStackSize) {
            Inventory.instance.Gold += price * CurrentStackSize;
            Container.Remove();
        }
        else {
            Inventory.instance.Gold += price * numberToSell;
            CurrentStackSize -= numberToSell;
        }
    }

    public virtual string GetDescription(string addition = "") {
        return $"{Title} x{CurrentStackSize}\nDurability: {durability} / {maxDurability}\nPrice: {price}\nType: {ItemType.ToString()}\n{addition}";
    }

    public void DurabilityLoss(int amount) {
        if ((durability -= amount) <= 0) {
            durability = 0;
        }
    }

    public bool RepairBy(int amount) {
        if (CantRepair()) {
            return false;
        }
        amount = amount > DurabilityMissing() ? DurabilityMissing() : amount;
        EuclideanRepair(amount);
        return true;
    }

    public bool FullRepair() {
        if (CantRepair()) {
            return false;
        }

        if (Inventory.instance.Gold >= CostToFullRepair()) {
            durability = maxDurability;
            Inventory.instance.Gold -= CostToFullRepair();
        }
        else {
            EuclideanRepair(maxDurability);
        }
        return true;
    }

    public int DurabilityMissing() {
        return maxDurability - durability;
    }

    public bool DurabilityIsMaxed() {
        return durability >= maxDurability;
    }

    public int CostToFullRepair() {
        return DurabilityMissing() * goldToRepairPerPoint;
    }

    public bool IsBroken() {
        return durability <= 0;
    }

    bool CantRepair() {
        return this as IRepairable == null || Inventory.instance.Gold < goldToRepairPerPoint;
    }

    void EuclideanRepair(int maxQuotient) {
        int q = 0;
        int r = Inventory.instance.Gold;

        while (r >= goldToRepairPerPoint && q < maxQuotient) {
            q += 1;
            r -= goldToRepairPerPoint;
        }
        durability += q;
        Inventory.instance.Gold = r;
    }

    public virtual void SaveData() { }
    public virtual void LoadData() { }
}
