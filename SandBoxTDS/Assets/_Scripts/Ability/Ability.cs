﻿using UnityEngine;

public enum AbilityType { PASSIVE, OFFENSIVE, DEFENSIVE }

public class Ability : GameObj, IUsable {
    public AbilityType AbilityType { get { return abilityType; } }
    public int Amount { get { return amount; } }
    public StatusEffect StatusEffect { get { return statusEffect; } }
    public bool AddsStatusEffect { get { return statusEffect != null; } }
    public float Cooldown { get { return cooldown; } }
    public float CooldownTimer { get; private set; }

    #region Inspector Variables
    [SerializeField] AbilityType abilityType;
    [SerializeField] int amount;
    [SerializeField] StatusEffect statusEffect;
    [SerializeField] float cooldown;
    #endregion

    void Update() {
        UpdateCooldown();
    }

    public virtual void Use(Entity target = null) {
        CooldownTimer = cooldown;
    }

    public bool Usable() {
        return CooldownTimer <= 0.0f;
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
        if (!Usable()) {
            CooldownTimer -= Time.deltaTime;
        }
    }
}
