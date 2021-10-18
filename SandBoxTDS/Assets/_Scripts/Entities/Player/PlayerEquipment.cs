using UnityEngine;

public class PlayerEquipment : MonoBehaviour {
    public Weapon[] Weapons { get { return weapons; } }
    public Armor[] Armor { get { return armor; } }
    public Consumable[] Consumables { get { return consumables; } }

    #region Inspector Variables
    [SerializeField] Weapon[] weapons = new Weapon[2];
    [SerializeField] Armor[] armor = new Armor[4];
    [SerializeField] Consumable[] consumables = new Consumable[2];
    #endregion

    public void AddWeapon(Weapon w, int slot) {
        weapons[slot] = w;
    }

    public void RemoveWeapon(int slot) {
        weapons[slot] = null;
    }

    public void AddArmor(Armor a, int slot) {
        armor[slot] = a;
    }

    public void RemoveArmor(int slot) {
        armor[slot] = null;
    }

    public void AddConsumable(Consumable c, int slot) {
        consumables[slot] = c;
    }

    public void RemoveConsumable(int slot) {
        consumables[slot] = null;
    }
}
