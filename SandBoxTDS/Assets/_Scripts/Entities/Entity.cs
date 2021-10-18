using UnityEngine;

/*
 This class handles all Entity States and Type, and health
 */

public enum EntityType { PLAYER, ENEMY, CRITTER, NPC }
public enum EntityState { FREE, NOACT, SLOWED, SLOWED_NOACT, IMMOBILE, IMMOBILE_NOACT, DEAD }

public class Entity : GameObj {
    public EntityType EntityType { get { return entityType; } }

    public EntityState EntityState { get; set; }
    public EntityState NextState { get; set; }
    public bool FreeState { get { return EntityState == EntityState.FREE; } }
    public bool DeadState { get { return EntityState == EntityState.DEAD; } }
    public float StateTimer { get; set; }

    public bool Slowed {
        get {
            return EntityState == EntityState.SLOWED || EntityState == EntityState.SLOWED_NOACT;
        }
    }

    public bool CanMove {
        get {
            return EntityState != EntityState.IMMOBILE && EntityState != EntityState.IMMOBILE_NOACT;
        }
    }

    public bool CanAct {
        get {
            return EntityState != EntityState.NOACT && EntityState != EntityState.SLOWED_NOACT && EntityState != EntityState.IMMOBILE_NOACT;
        }
    }

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
        UpdateStateTimers();
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
                EntityState = EntityState.DEAD;
            }
        }
    }

    void Death() {
        Debug.Log("dead");
        //death animation, sound, object pooling
    }

    void UpdateStateTimers() {
        if (!DeadState) {
            if (StateTimer > 0.0f) {
                if ((StateTimer -= Time.deltaTime) <= 0.0f) {
                    EntityState = NextState;
                }
            }
        }
    }
}
