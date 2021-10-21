using UnityEngine;

public enum ReloadType { CLIPLESS, MANUAL, AUTO }
public class Weapon : Item, IEquippable, IRepairable {
    const float TimeAfterShootingToAutoReload = 5.0f;

    public ReloadType ReloadType { get { return reloadType; } }
    public int Damage { get { return damage; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public int MaxAmmo { get { return maxAmmoCharge; } }
    public int Ammo { get; private set; }

    float attackTimer;
    float autoReloadTimer;
    float secondsSinceLastShot;

    #region Inspector Variables
    [SerializeField] ReloadType reloadType;
    [SerializeField] GameObject projectile;
    [SerializeField] int damage;
    [SerializeField] float attackSpeed;
    [SerializeField] int maxAmmoCharge;
    [SerializeField] int ammoPerShot;
    [SerializeField] int ammoPerAutoReload;
    [SerializeField] float reloadSpeed;
    #endregion

    void Update() {
        UpdateTimers();
    }

    public virtual void Fire() {
        //Check for Projectile Object pool for projectile with id:id
        //Instantiate projectile if no match in object pooling system
        //Quaterion.rotation on projectile
        //Get direction
        //Projectile.Fire(direction, damage);

        if (ReloadType != ReloadType.CLIPLESS && (Ammo -= ammoPerShot) < 0) {
            Ammo = 0;
        }
        attackTimer = attackSpeed;
        autoReloadTimer = 0;
        secondsSinceLastShot = 0;
    }

    public bool CanFire() {
        return (ReloadType == ReloadType.CLIPLESS || Ammo > ammoPerShot) && attackTimer <= 0.0f;
    }

    public void ManualReload(Entity entity) {
        //entity.(Start reload animation for (reloadSpeed) seconds)
        //coroutine with WaitForSeconds(reloadSpeed)
        //  Ammo = MaxAmmo (after WaitForSeconds)
    }

    void AutoReload() {
        if ((Ammo += ammoPerAutoReload) > MaxAmmo) {
            Ammo = MaxAmmo;
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

        if (Ammo < MaxAmmo && ReloadType == ReloadType.AUTO) {
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
