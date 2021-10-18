using UnityEngine;

/* 
    This class handles enemy movement and attacking logic.
    Random movement, chasing/running away from the player
    Checking if the player is in range to attack, and attacking.
 */

public enum EnemyType { RANGED, MELEE, PATTERNED }

[RequireComponent(typeof(MobileEntity))]
public class EnemyController : Entity, IMobile {
    const float LoseSightMod = 1.5f;
    const float MinPatrolTime = 3.0f;
    const float MaxPatrolTime = 6.0f;

    [SerializeField] float sightRange;
    [SerializeField] int damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackToMoveTime;

    Transform target;
    float distanceToTarget;
    bool targetSpotted;
    float attackTimer;
    Vector3 patrolDirection;
    float patrolTimer;

    bool TargetInAttackRange { get { return distanceToTarget <= attackRange; } }
    bool AttackTimerZero { get { return attackTimer <= 0.0f; } }
    bool Patrolling { get { return patrolTimer > 0.0f; } }

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        UpdatePatrolTimer();
        UpdateAttackTimer();
    }

    //Called every frame
    public (Vector3, int) GetMove() {
        CheckForTargetInSight();
        if (targetSpotted) {
            GetTargetSpottedFacing();

            if (CanAttack()) {
                Attack();
                return (Vector3.zero, 0);
            }
            return (Vector3.forward, Slowed ? 0 : 1);
        }
        GetRandomFacing();
        return (Vector3.forward, 0);
    }

    void CheckForTargetInSight() {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (targetSpotted && distanceToTarget > (sightRange * LoseSightMod)) {
            targetSpotted = false;
        }
        else if (distanceToTarget < sightRange) {
            targetSpotted = true;
        }
    }

    bool CanAttack() {
        return TargetInAttackRange && AttackTimerZero && CanAct;
    }

    bool InbetweenAttacks() {
        return !AttackTimerZero && CanAct && !TargetInAttackRange;
    }

    void Attack() {
        //animation, fire bullet/melee hit check, sound
        EntityState = EntityState.SLOWED_NOACT;
        NextState = EntityState.NOACT;
        attackTimer = attackSpeed;
        StateTimer = attackToMoveTime;
    }

    void GetTargetSpottedFacing() {
        Vector3 direction = target.position - transform.position;
        if (EntityType == EntityType.CRITTER) {
            transform.LookAt(new Vector3(-direction.x, transform.position.y, -direction.z));
        }
        else {
            transform.LookAt(new Vector3(direction.x, transform.position.y, direction.z));
        }
    }

    Vector3 GetRandomFacing() {
        if (Patrolling) {
            return patrolDirection;
        }
        patrolDirection = new Vector3(0.0f, Random.Range(-180f, 180f), 0.0f);
        return patrolDirection;
    }

    void UpdatePatrolTimer() {
        if (!targetSpotted && Patrolling) {
            patrolTimer -= Time.deltaTime;
        }
    }

    void UpdateAttackTimer() {
        if (!AttackTimerZero && (attackTimer -= Time.deltaTime) <= 0.0f) {
            NextState = EntityState = EntityState.FREE;
        }
    }
}
