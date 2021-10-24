using UnityEngine;

public class PlayerEquipment : MonoBehaviour {
    public static PlayerEquipment instance;

    [SerializeField] EquipmentSlot[] weaponSlots = new EquipmentSlot[2];
    [SerializeField] EquipmentSlot[] armorSlots = new EquipmentSlot[4];
    [SerializeField] EquipmentSlot[] consumableSlots = new EquipmentSlot[2];

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void WeaponRightClickEquip(Slot fromSlot, Weapon weapon) {
        RightClickEquip(weaponSlots, fromSlot, weapon);
    }

    public Weapon GetWeaponAtSlot(int slot) {
        if (weaponSlots[slot].IsEmpty) {
            return null;
        }
        return weaponSlots[slot].Item as Weapon;
    }

    public void ArmorRightClickEquip(Slot fromSlot, Chip armor) {
        RightClickEquip(armorSlots, fromSlot, armor);
    }

    public Ability GetAbilityFromArmorSlot(int slot) {
        if (armorSlots[slot].IsEmpty) {
            return null;
        }
        return (armorSlots[slot].Item as Chip).Ability;
    }

    public void ConsumableRightClickEquip(Slot fromSlot, Consumable consumable) {
        RightClickEquip(consumableSlots, fromSlot, consumable);
    }

    void RightClickEquip(EquipmentSlot[] slots, Slot fromSlot, Item item) {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].IsEmpty) {
                slots[i].Add(item);
                return;
            }
        }
        fromSlot.Add(slots[slots.Length - 1].Item);
        slots[slots.Length - 1].Add(item);
    }

    public void SaveData() { }
    public void LoadData() { }
}
