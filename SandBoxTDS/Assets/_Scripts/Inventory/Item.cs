using UnityEngine;

public enum ItemType { Consumable, Tradable, Trash, Material, Equipment, Quest }

public class Item : MonoBehaviour {
    [SerializeField] int id;
    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] int buyPrice;
    [SerializeField] int sellPrice;
    [SerializeField] int stackSize;
    [SerializeField] int maxStackSize;
    [SerializeField] Sprite icon;
    [SerializeField] Item[] itemsToCraft;
    [SerializeField] int[] amountToCraft;

    public Slot Slot { get; set; }

    public virtual void Create(int stackSize) { this.stackSize = stackSize; }
    public virtual int[] SaveData(int[] data) { return data; }

    public virtual string GetDescription(string addition = "") {
        addition = addition == "" ? addition : addition + "\n";
        return $"{title}\n{description}\n{addition}Price: {buyPrice}  -  Stack: {stackSize}";        
    }

    public string GetCraftingList() {
        string output = "Material List:\n";
        for (int i = 0; i < itemsToCraft.Length; i++) {
            output += $"{itemsToCraft[i].title}: {amountToCraft[i]}\n";
        }
        return output;
    }

    public void AdjustStack(int amount) {
        stackSize += amount;

        if (AvailableStackSpace() < 0) {
            int extra = stackSize - maxStackSize;
            stackSize = maxStackSize;
            Create(extra);
        }
        else if (stackSize <= 0) {
            Delete();
        }
    }

    public void Sell(int amount) {
        //Inventory.gold += sellPrice * amount;
        AdjustStack(amount);
    }

    public void Delete() {
        Debug.Log($"Item {id}-{title} has been deleted");
    }

    public int GetId() { return id; }
    public int GetBuyPrice() { return buyPrice; }
    public int GetSellPrice() { return sellPrice; }
    public int GetStackSize() { return stackSize; }
    public int AvailableStackSpace() { return maxStackSize - stackSize; }
    public Sprite GetIcon() { return icon; }
}
