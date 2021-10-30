using UnityEngine;

public enum ReloadType { CLIPLESS, MANUAL, AUTO }
public class Weapon : Item, IEquippable, IRepairable {
    const float TimeAfterShootingToAutoReload = 5.0f;

    int ammo;
    float attackTimer;
    float autoReloadTimer;
    float secondsSinceLastShot;

    [SerializeField] ReloadType reloadType;
    [SerializeField] GameObject projectile;
    [SerializeField] int damage;
    [SerializeField] float attackSpeed;
    [SerializeField] int maxAmmo;
    [SerializeField] int ammoPerShot;
    [SerializeField] int ammoPerAutoReload;
    [SerializeField] float reloadSpeed;

    void Update() {
        UpdateTimers();
    }

    public override string GetDescription(string addition = "") {
        string output = $"Damage: {damage}\nFire Rate: {attackSpeed}\nAmmo Type: {reloadType.ToString()}\n";
        string ammoInfo = $"Projectile: {projectile.GetComponent<Projectile>().GetDetails()}";
        output += reloadType == ReloadType.CLIPLESS ? $"{ammoInfo}" : $"Ammo: {ammo} / {maxAmmo}\n{ammoInfo}";
        return base.GetDescription(output);
    }

    public virtual void Fire() {
        //Check for Projectile Object pool for projectile with id:id
        //Instantiate projectile if no match in object pooling system
        //Quaterion.rotation on projectile
        //Get direction
        //Projectile.Fire(direction, damage);

        ammo -= reloadType == ReloadType.CLIPLESS ? 0 : ammoPerShot;

        attackTimer = attackSpeed;
        autoReloadTimer = 0;
        secondsSinceLastShot = 0;
    }

    public bool CanFire() {
        return (reloadType == ReloadType.CLIPLESS || ammo >= ammoPerShot) && !IsBroken() && attackTimer <= 0.0f;
    }

    public void ManualReload(Entity entity) {
        //entity.(Start reload animation for (reloadSpeed) seconds)
        //coroutine with WaitForSeconds(reloadSpeed)
        //  Ammo = MaxAmmo (after WaitForSeconds)
    }

    void AutoReload() {
        ammo += ammoPerAutoReload;
        if (ammo > maxAmmo) {
            ammo = maxAmmo;
        }
        autoReloadTimer = reloadSpeed;
    }

    public void Equip() {
        throw new System.NotImplementedException();
    }

    public void Unequip() {
        throw new System.NotImplementedException();
    }

    public void AdjustStats(bool equipping = true) {
        throw new System.NotImplementedException();
    }

    public void GainDurability(bool max = true, int amount = 0) {
        if (max) {
            FullRepair();
        }
        else {
            RepairBy(amount);
        }
    }

    public void LoseDurability(int amount) {
        DurabilityLoss(amount);
    }

    void UpdateTimers() {
        if (attackTimer > 0.0f) {
            attackTimer -= Time.deltaTime;
        }

        if (reloadType == ReloadType.AUTO && ammo < maxAmmo) {
            if (autoReloadTimer > 0.0f) {
                autoReloadTimer -= Time.deltaTime;
            }

            if (secondsSinceLastShot < TimeAfterShootingToAutoReload) {
                secondsSinceLastShot += Time.deltaTime;
            }
            else if (autoReloadTimer <= 0.0f) {
                AutoReload();
            }
        }
    }
}
