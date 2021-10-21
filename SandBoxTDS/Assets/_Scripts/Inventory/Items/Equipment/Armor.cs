﻿using System.Collections;
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
        DurabilityLoss(amount);
    }

    public void GainDurability(bool max = true, int amount = 0) {
        if (max) {
            FullRepair();
        }
        else {
            RepairBy(amount);
        }
    }
}
