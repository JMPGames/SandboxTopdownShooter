using UnityEngine;

public enum EnemyType { RANGED, MELEE, PATTERNED }

[RequireComponent(typeof(MobileEntity))]
[RequireComponent(typeof(EnemySightHandler))]
public class EnemyController : Entity, IMobile {
    const float MinPatrolTime = 3.0f;
    const float MaxPatrolTime = 6.0f;

    EnemySightHandler sh;

    Vector3 patrolDirection;
    float patrolTimer;
    bool Patrolling { get { return patrolTimer > 0.0f; } }

    float attackTimer;
    bool AttackTimerZero { get { return attackTimer <= 0.0f; } }

    #region Inspector Variables
    [SerializeField] EnemyType enemyType;
    [SerializeField] int damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackToMoveTime;
    #endregion

    void Start() {
        sh = GetComponent<EnemySightHandler>();
        sh.Setup(enemyType);        
    }

    void Update() {
        UpdatePatrolTimer();
        UpdateAttackTimer();
    }

    //Called every frame
    public (Vector3, int) GetMove() {
        sh.CheckForTargetInSight();
        if (sh.TargetSpotted) {
            sh.GetTargetSpottedFacing(EntityType == EntityType.CRITTER);

            if (CanAttack()) {
                Attack();
                return (Vector3.zero, 0);
            }
            return (Vector3.forward, Slowed ? 0 : 1);
        }
        transform.LookAt(GetRandomFacing());
        return (Vector3.forward, 0);
    }

    bool CanAttack() {
        return !sh.TargetTooClose() && TargetInAttackRange() && AttackTimerZero && CanAct;
    }

    bool TargetInAttackRange() {
        return sh.DistanceToTarget() <= attackRange;
    }

    public virtual void Attack() {
        //animation, fire bullet/melee hit check, sound
        EntityState = EntityState.SLOWED_NOACT;
        NextState = EntityState.NOACT;
        attackTimer = attackSpeed;
        StateTimer = attackToMoveTime;
    }

    Vector3 GetRandomFacing() {
        if (Patrolling) {
            return patrolDirection;
        }
        patrolDirection = new Vector3(0.0f, Random.Range(-180f, 180f), 0.0f);
        patrolTimer = Random.Range(MinPatrolTime, MaxPatrolTime);
        return patrolDirection;
    }    

    void UpdatePatrolTimer() {
        if (!sh.TargetSpotted && Patrolling) {
            patrolTimer -= Time.deltaTime;
        }
    }

    void UpdateAttackTimer() {
        if (!AttackTimerZero && (attackTimer -= Time.deltaTime) <= 0.0f) {
            NextState = EntityState = EntityState.FREE;
        }
    }
}
