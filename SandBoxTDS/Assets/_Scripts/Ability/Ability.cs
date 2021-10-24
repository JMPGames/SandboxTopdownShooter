using UnityEngine;

public class Ability : GameObj, IUsable {
    [SerializeField] float cooldown;
    float cooldownTimer;    

    void Update() {
        UpdateCooldown();
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
        if (total || (cooldownTimer -= amount) < 0.0f) {
            cooldownTimer = 0.0f;
        }
    }

    public virtual void UpdateCooldown() {
        if (!Usable() && (cooldownTimer -= Time.deltaTime) < 0.0f) {
            cooldownTimer = 0.0f;
        }
    }
}
