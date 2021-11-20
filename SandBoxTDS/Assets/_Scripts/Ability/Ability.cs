using UnityEngine;

public class Ability : GameObj, IUsable {
    [SerializeField] float cooldown;
    float cooldownTimer;

    void Update() {
        if (!Usable()) {
            UpdateCooldown();
        }

        if (cooldownTimer < 0.0f) {
            cooldownTimer = 0.0f;
        }
    }

    public virtual void Use(Entity target = null) {
        cooldownTimer = cooldown;
    }

    public virtual string GetDescription(string addition = "") {
        return $"{Title}\nCooldown: {cooldown} seconds\n{Description}\n{addition}";
    }

    public (float, float) GetCooldownBarData() {
        return (cooldownTimer, cooldown);
    }

    public virtual bool Usable() {
        return cooldownTimer <= 0.0f;
    }

    public virtual void DecreaseCooldown(bool total = true, float amount = 0) {
        cooldownTimer = total ? 0.0f : cooldownTimer - amount;
    }

    public virtual void UpdateCooldown() {
        cooldownTimer -= Time.deltaTime;
    }
}
