using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType { PASSIVE, OFFENSIVE, DEFENSIVE }

public class Ability : GameObj {
    public AbilityType AbilityType { get { return abilityType; } }
    public int Amount { get { return amount; } }
    public StatusEffect StatusEffect { get { return statusEffect; } }
    public bool AddsStatusEffect { get { return statusEffect != null; } }
    public float Cooldown { get { return cooldown; } }
    public float CooldownTimer { get; private set; }
    public bool OnCooldown { get { return CooldownTimer > 0.0f; } }

    #region Inspector Variables
    [SerializeField] AbilityType abilityType;
    [SerializeField] int amount;
    [SerializeField] StatusEffect statusEffect;
    [SerializeField] float cooldown;
    #endregion

    void Update() {
        UpdateCooldown();
    }

    public virtual void Cast() {
        CooldownTimer = Cooldown;
    }

    public void DecreaseCooldown(bool total = true, float amount = 0) {
        if (total || (CooldownTimer - amount) < 0.0f) {
            CooldownTimer = 0.0f;
        }
        else {
            CooldownTimer -= amount;
        }
    }

    void UpdateCooldown() {
        if (OnCooldown) {
            CooldownTimer -= Time.deltaTime;
        }
    }
}
