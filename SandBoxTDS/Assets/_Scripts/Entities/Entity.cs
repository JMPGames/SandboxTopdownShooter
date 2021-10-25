using UnityEngine;

public enum EntityType { PLAYER, ENEMY, CRITTER, NPC }
public enum EntityState { FREE, NOACT, SLOWED, SLOWED_NOACT, IMMOBILE, IMMOBILE_NOACT, DEAD }

public class Entity : GameObj, IDamagable {
    public EntityType EntityType { get { return entityType; } }
    [SerializeField] EntityType entityType;
    
    public int Health { get; private set; }
    public int MaxHealth { get { return maxHealth; } }
    [SerializeField] int maxHealth;

    public EntityState EntityState { get; set; }
    public EntityState NextEntityState { get; set; }
    public float StateTimer { get; set; }    

    void Update() {
        UpdateStateTimers();
    }

    public void GainHealth(int amount) {
        //PopUpText.Popup(this.position, amount, green);
        if ((Health += amount) > MaxHealth) {
            Health = MaxHealth;
        }
    }

    public void LoseHealth(int amount) {
        //PopUpText.Popup(this.position, amount, red);
        if ((Health -= amount) <= 0) {
            Health = 0;
            Destroyed();
        }
    }

    public bool Slowed() {
        return EntityState == EntityState.SLOWED || EntityState == EntityState.SLOWED_NOACT;
    }

    public bool CanMove() {
        return EntityState != EntityState.IMMOBILE && EntityState != EntityState.IMMOBILE_NOACT;
    }

    public bool CanAct() {
        return EntityState != EntityState.NOACT && EntityState != EntityState.SLOWED_NOACT && EntityState != EntityState.IMMOBILE_NOACT;
    }

    public bool IsDead() {
        return EntityState == EntityState.DEAD;
    }

    public void Destroyed() {
        Debug.Log("dead");
        EntityState = EntityState.DEAD;
        //death animation, sound, object pooling
    }

    void UpdateStateTimers() {
        if (!IsDead() && StateTimer > 0.0f && (StateTimer -= Time.deltaTime) <= 0.0f) {
            EntityState = NextEntityState;
        }
    }
}
