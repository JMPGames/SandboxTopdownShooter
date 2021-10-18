using UnityEngine;

public enum ReloadType { CLIPLESS, MANUAL, AUTO }
public class Weapon : Item, IEquippable, IRepairable {
    public ReloadType ReloadType { get { return reloadType; } }
    public int Damage { get { return damage; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public int MaxAmmo { get { return maxAmmoCharge; } }
    public int Ammo { get; private set; }
    public bool CanFire { get { return Ammo > ammoPerShot && attackTimer <= 0.0f; } }
    public bool CanReload { get { return ReloadType == ReloadType.MANUAL && Ammo < MaxAmmo; } }

    float attackTimer;

    #region Inspector Variables
    [SerializeField] ReloadType reloadType;
    [SerializeField] GameObject projectile;
    [SerializeField] int damage;
    [SerializeField] float attackSpeed;
    [SerializeField] int maxAmmoCharge;
    [SerializeField] int ammoPerShot;
    [SerializeField] float reloadSpeed;
    #endregion

    void Update() {
        if (attackTimer > 0.0f) {
            attackTimer -= Time.deltaTime;
        }
    }

    public virtual void Fire() {
        Ammo -= ammoPerShot;
        attackTimer = attackSpeed;
    }

    public void Reload(Entity entity) {
        if (ReloadType == ReloadType.MANUAL) {

        }
        else if (ReloadType == ReloadType.AUTO) {

        }
    }

    public void AdjustStats(bool equipping = true) {
        throw new System.NotImplementedException();
    }

    public void Equip() {
        throw new System.NotImplementedException();
    }

    public void GainDurability(bool max = true, int amount = 0) {
        throw new System.NotImplementedException();
    }

    public void LoseDurability(int amount) {
        throw new System.NotImplementedException();
    }

    public void Unequip() {
        throw new System.NotImplementedException();
    }
}
