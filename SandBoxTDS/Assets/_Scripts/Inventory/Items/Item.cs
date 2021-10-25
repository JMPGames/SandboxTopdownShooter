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
        //check inventory for space
        //if CanAfford()
        //add to inventory and take gold
    }

    public bool CanAfford(int numberToBuy) {
        return (price * numberToBuy) <= Inventory.instance.Gold;
    }

    public void Sell(int numberToSell) {
        if (numberToSell >= CurrentStackSize) {
            Inventory.instance.Gold += price * CurrentStackSize;
            //remove item from inventory
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
