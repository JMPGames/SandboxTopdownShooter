using UnityEngine;

public class InventoryCursor : MonoBehaviour {
    public static InventoryCursor instance;

    public GameObject tooltip;

    public Item Item { get; set; }
    public UIContainer FromContainer { get; set; }
    public bool IsHoldingItem { get { return Item != null; } }
    public ItemType HeldItemType { get { return Item.ItemType; } }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Putback();
        }
    }

    public void PickUp(Item item, UIContainer container) {
        Item = item;
        FromContainer = container;
    }

    public void Place(UIContainer container) {
        if (MatchCheck(Item, container)) { 
            container.Add(Item);
            Empty();
        }
        else {
            //Negative SFX
        }
    }

    public void Putback() {
        FromContainer.Add(Item);
        Empty();
    }

    public void Swap(Item item, UIContainer container) {
        if (MatchCheck(item, FromContainer) && MatchCheck(Item, container)) {
            container.Add(Item);
            FromContainer.Add(item);
            Empty();
        }
        else {
            //Negative SFX
        }
    }

    void Empty() {
        Item = null;
        FromContainer = null;
    }

    bool MatchCheck(Item item, UIContainer container) {
        return container.InventorySlot || container.ItemTypeMatches(Item.ItemType.ToString());
    }
}
