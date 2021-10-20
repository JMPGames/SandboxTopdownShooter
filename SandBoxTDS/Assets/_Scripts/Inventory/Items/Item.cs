using UnityEngine;

public enum ItemType { CONSUMABLE, RESOURCE, WEAPON, ARMOR, QUEST, JUNK }

public class Item : GameObj {
    public ItemType ItemType { get { return itemType; } }
    public UIContainer Container { get; set; }
    public int Price { get { return price; } }

    public int MaxStackSize { get { return maxStackSize; } }
    public int CurrentStackSize { get; set; }
    public bool IsStackFull { get { return CurrentStackSize >= MaxStackSize; } }

    public int MaxDurability { get { return maxDurability; } }
    public int CurrentDurability { get; set; }
    public bool IsAtMaxDurability { get { return CurrentDurability >= MaxDurability; } }
    public int CostToRepair { get { return (MaxDurability - CurrentDurability) * goldToRepairPerPoint; } }

    #region Inspector Variables
    [SerializeField] ItemType itemType;
    [SerializeField] int price;
    [SerializeField] int maxStackSize;
    [SerializeField] int maxDurability;
    [SerializeField] int goldToRepairPerPoint;
    #endregion

    public bool Repair() {
        if(CantRepair()) {
            return false;
        }

        if(Inventory.instance.Gold >= CostToRepair) {
            CurrentDurability = MaxDurability;
            Inventory.instance.Gold -= CostToRepair;
        }
        else {
            EuclideanRepair();
        }
        return true;
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

    bool CantRepair() {
        return this as IRepairable == null || Inventory.instance.Gold <= 0 || Inventory.instance.Gold < goldToRepairPerPoint;
    }

    void EuclideanRepair() {
        int q = 0;
        int r = Inventory.instance.Gold;

        while (r >= goldToRepairPerPoint) {
            q += 1;
            r -= goldToRepairPerPoint;
        }
        CurrentDurability += q;
        Inventory.instance.Gold = r;
    }
}
