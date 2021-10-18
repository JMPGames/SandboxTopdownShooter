using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item, IEquippable, IRepairable {
    public Ability Ability { get { return ability; } }

    [SerializeField] Ability ability;

    public void Equip() {
        throw new System.NotImplementedException();
    }

    public void Unequip() {
        throw new System.NotImplementedException();
    }

    public void AdjustStats(bool equipping = true) {
        throw new System.NotImplementedException();
    }

    public void LoseDurability(int amount) {
        throw new System.NotImplementedException();
    }

    public void GainDurability(bool max = true, int amount = 0) {
        throw new System.NotImplementedException();
    }
}
