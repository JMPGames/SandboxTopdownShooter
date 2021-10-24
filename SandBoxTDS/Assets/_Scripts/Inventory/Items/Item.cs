using UnityEngine;

public enum ItemType { CONSUMABLE, RESOURCE, WEAPON, CHIP, QUEST, JUNK }

public class Item : GameObj {
    public ItemType ItemType { get { return itemType; } }
    public UIContainer Container { get; set; }
    public int Price { get { return price; } }

    public int MaxStackSize { get { return maxStackSize; } }
    public int CurrentStackSize { get; set; }

    public int MaxDurability { get { return maxDurability; } }
    public int Durability { get; private set; }

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
        return (Price * numberToBuy) <= Inventory.instance.Gold;
    }

    public void Sell(int numberToSell) {
        //remove from inventory and give gold
    }

    public virtual string GetDescription(string addition = "") {
        return $"{Title}    Price: {Price}  Type: {ItemType.ToString()}\n{addition}";
    }

    public void DurabilityLoss(int amount) {
        if ((Durability -= amount) <= 0) {
            Durability = 0;
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
            Durability = MaxDurability;
            Inventory.instance.Gold -= CostToFullRepair();
        }
        else {
            EuclideanRepair(MaxDurability);
        }
        return true;
    }

    public int DurabilityMissing() {
        return MaxDurability - Durability;
    }

    public bool DurabilityIsMaxed() {
        return Durability >= MaxDurability;
    }

    public int CostToFullRepair() {
        return DurabilityMissing() * goldToRepairPerPoint;
    }

    public bool IsBroken() {
        return Durability <= 0;
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
        Durability += q;
        Inventory.instance.Gold = r;
    }

    public virtual void SaveData() { }
    public virtual void LoadData() { }
}
