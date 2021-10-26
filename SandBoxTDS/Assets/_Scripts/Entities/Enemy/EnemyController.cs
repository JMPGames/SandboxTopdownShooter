using UnityEngine;

public enum EnemyType { MELEE, RANGED, TURRET }

[RequireComponent(typeof(MobileEntity))]
[RequireComponent(typeof(EntityAbilityHandler))]
[RequireComponent(typeof(EntityStatusEffectHandler))]
[RequireComponent(typeof(EnemySightHandler))]
[RequireComponent(typeof(PatrolHandler))]
public class EnemyController : Entity, IMobile {
    const float MinStrafeTime = 1.0f;
    const float MaxStrafeTime = 3.0f;

    EntityAbilityHandler abilityHandler;
    EntityStatusEffectHandler seHandler;
    EnemySightHandler sightHandler;
    PatrolHandler patrolHandler;

    [SerializeField] EnemyType enemyType;
    [SerializeField] int damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackToMoveTime;

    float attackTimer;
    Vector3 strafeDirection = Vector3.left;
    float strafeTimer;

    void Start() {
        abilityHandler = GetComponent<EntityAbilityHandler>();
        seHandler = GetComponent<EntityStatusEffectHandler>();
        sightHandler = GetComponent<EnemySightHandler>();
        patrolHandler = GetComponent<PatrolHandler>();
    }

    void Update() {
        UpdateAttackTimer();
    }

    //Called every frame from MobileEntity
    public (Vector3, int) GetMove() {
        if (sightHandler.CheckForTargetInSight()) {
            transform.LookAt(sightHandler.GetTargetSpottedFacing(EntityType == EntityType.CRITTER));

            if (CanAttack()) {
                Attack();
                return (Vector3.zero, 0);
            }
            return (TargetInSightMovement(), Slowed() ? 0 : 1);
        }
        transform.LookAt(patrolHandler.GetRandomFacing());
        return (Vector3.forward, 0);
    }

    bool CanAttack() {
        return !sightHandler.TargetTooClose() && TargetInAttackRange() && AttackTimerZero() && CanAct();
    }

    bool TargetInAttackRange() {
        return sightHandler.DistanceToTarget() <= attackRange;
    }

    public virtual void Attack() {
        EntityState = EntityState.SLOWED_NOACT;
        NextEntityState = EntityState.NOACT;
        attackTimer = attackSpeed;
        StateTimer = attackToMoveTime;
    }

    public Vector3 TargetInSightMovement() {
        if (enemyType == EnemyType.MELEE) {
            return Vector3.forward;
        }
        else if (enemyType == EnemyType.TURRET) {
            return Vector3.zero;
        }

        if ((strafeTimer -= Time.deltaTime) <= 0.0f) {
            strafeDirection = Random.Range(1, 101) <= 50 ? Vector3.left : Vector3.right;
            strafeTimer = Random.Range(MinStrafeTime, MaxStrafeTime);
        }
        return strafeDirection;
    }

    public bool AttackTimerZero() {
        return attackTimer <= 0.0f;
    }

    void UpdateAttackTimer() {
        if (!AttackTimerZero() && (attackTimer -= Time.deltaTime) <= 0.0f) {
            NextEntityState = EntityState = EntityState.FREE;
        }
    }
}
