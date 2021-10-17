using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType { PLAYER, ENEMY, CRITTER, NPC }
public enum EntityState { ACTIVE, INACTIVE, DEAD }

public class Entity : GameObj {
    public EntityType EntityType { get { return entityType; } }
    public EntityState EntityState { get; set; }
    public bool ActiveState { get { return EntityState == EntityState.ACTIVE; } }
    public bool DeadState { get { return EntityState == EntityState.DEAD; } }
    public float InactiveStateTimer { get; set; }
    public Ability[] Abilities { get { return abilities; } }
    public int MaxHealth { get { return maxHealth; } }
    public int Health {
        get { return health; }
        set { AdjustHealth(value); }
    }
    int health;  

    #region Inspector Variables
    [SerializeField] EntityType entityType;
    [SerializeField] Ability[] abilities = new Ability[4];
    [SerializeField] int maxHealth;
    #endregion

    void Update() {
        UpdateInactiveStateTimer();
    }

    public void AddAbility(Ability a, int slot) {
        abilities[slot] = a;
    }

    public void RemoveAbility(int slot) {
        abilities[slot] = null;
    }

    public bool UseAbility(int slot) {
        if (abilities[slot] != null && abilities[slot].Usable()) {
            abilities[slot].Use();
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

    void UpdateInactiveStateTimer() {
        if (InactiveStateTimer > 0.0f && !DeadState) {
            InactiveStateTimer -= Time.deltaTime;
            if (InactiveStateTimer <= 0.0f) {
                EntityState = EntityState.ACTIVE;
            }
        }
    }
}
