using UnityEngine;

public class PlayerEquipment : MonoBehaviour {
    #region Inspector Variables
    [SerializeField] Weapon[] weapons = new Weapon[2];
    [SerializeField] Armor[] armor = new Armor[4];
    [SerializeField] Consumable[] consumables = new Consumable[2];
    #endregion

    public void AddWeapon(in Weapon w, in int slot) {
        weapons[slot] = w;
    }

    public Weapon GetWeaponAtSlot(int slot) {
        return weapons[slot];
    }

    public void RemoveWeapon(int slot) {
        weapons[slot] = null;
    }

    public void AddArmor(Armor a, int slot) {
        armor[slot] = a;
    }

    public Armor GetArmorAtSlot(int slot) {
        return armor[slot];
    }

    public void RemoveArmor(int slot) {
        armor[slot] = null;
    }

    public void AddConsumable(Consumable c, int slot) {
        consumables[slot] = c;
    }

    public Consumable GetConsumableAtSlot(int slot) {
        return consumables[slot];
    }

    public void RemoveConsumable(int slot) {
        consumables[slot] = null;
    }
}
