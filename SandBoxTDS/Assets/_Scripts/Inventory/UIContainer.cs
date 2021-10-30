using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ContainerType { INVENTORY, WEAPON, ARMOR, CONSUMABLE }

public class UIContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public ContainerType ContainerType { get { return containerType; } }
    public Item Item { get; private set; }
    public bool InventorySlot { get { return ContainerType == ContainerType.INVENTORY; } }
    public bool IsEmpty { get { return Item == null; } }

    [SerializeField] ContainerType containerType;
    [SerializeField] Image icon;

    public virtual void Add(Item item) {
        Item = item;
        icon.sprite = item.Icon;
        icon.color = Color.white;
        item.Container = this;
    }

    public virtual void Remove() {
        Item = null;
        icon.color = new Color(0, 0, 0, 0);
    }

    public virtual void RightClick() { }

    public bool ItemTypeMatches(string itemType) {
        return ContainerType.ToString() == itemType;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!IsEmpty) {
            SlotTooltip.instance.ShowTooltip(transform.position, Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        SlotTooltip.instance.HideTooltip();
    }
}
