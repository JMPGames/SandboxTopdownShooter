using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType { PLAYER, ENEMY, CRITTER, NPC }

public class Entity : GameObj {
    const float LoseSightMod = 1.5f;

    public EntityType EntityType { get { return entityType; } }
    public Ability[] Abilities { get { return abilities; } }  
    public int MaxHealth { get { return maxHealth; } }
    public int Health {
        get { return health; }
        set { AdjustHealth(value); }
    }

    int health;
    [SerializeField] float sightRange;
    bool targetSpotted;

    #region Inspector Variables
    [SerializeField] EntityType entityType;
    [SerializeField] Ability[] abilities = new Ability[4];
    [SerializeField] int maxHealth;
    #endregion

    public bool CheckForTargetInSight(float distance) {
        if (targetSpotted && distance > (sightRange * LoseSightMod)) {
            targetSpotted = false;
        }
        else if(distance < sightRange) {
            targetSpotted = true;
        }
        return targetSpotted;
    }

    public void AddAbility(Ability a, int slot) {
        abilities[slot] = a;
    }

    public void RemoveAbility(int slot) {
        abilities[slot] = null;
    }

    public bool UseAbility(int slot) {
        if (abilities[slot] != null && !abilities[slot].OnCooldown) {
            abilities[slot].Cast();
            return true;
        }
        return false;
    }

    void AdjustHealth(int amount) {
        if (amount != 0) {
            Color color = amount < 0 ? Color.red : Color.green;
            //PopUpText.Popup(this.position, Math.Abs(value), color);
            health += amount;
            if (health > MaxHealth) {
                health = MaxHealth;
            }
            else if (health < 0) {
                health = 0;
            }
        }
    }
}
