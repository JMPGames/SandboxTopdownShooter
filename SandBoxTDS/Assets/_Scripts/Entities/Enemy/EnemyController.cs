using UnityEngine;

/* 
    This class handles enemy movement and attacking logic.
    Random movement, chasing/running away from the player
    Checking if the player is in range to attack, and attacking.

    Self contained variables
 */

public enum EnemyType { RANGED, MELEE, PATTERNED }

[RequireComponent(typeof(MobileEntity))]
public class EnemyController : Entity, IMobile {
    const float LoseSightMod = 1.5f;
    const float MinPatrolTime = 3.0f;
    const float MaxPatrolTime = 6.0f;

    Transform target;
    [SerializeField] float sightRange;
    float distanceToTarget;
    bool targetSpotted;
    [SerializeField] int damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    float attackTimer;
    [SerializeField] float attackToMoveTime;
    float attackToMoveTimer;
    Vector3 patrolDirection;
    float patrolTimer;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        UpdatePatrolTimer();
    }

    public (Vector3, int) GetMove(bool slowed) {
        CheckForTargetInSight();
        if (targetSpotted) {
            GetTargetSpottedFacing();

            if (CanAttack()) {
                Attack();
                return (Vector3.zero, 0);
            }
            else if (InbetweenAttacks()) {
                slowed = true;
            }
            return (Vector3.forward, slowed ? 0 : 1);
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
        return distanceToTarget < attackRange && attackTimer <= 0.0f && ActiveState;
    }

    bool InbetweenAttacks() {
        return attackTimer > 0.0f && ActiveState && distanceToTarget > attackRange;
    }

    void Attack() {
        //animation, fire bullet/melee hit check, sound
        EntityState = EntityState.INACTIVE;
        attackTimer = attackSpeed;
        InactiveStateTimer = attackToMoveTime;
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
        if (patrolTimer > 0.0f) {
            return patrolDirection;
        }
        patrolDirection = new Vector3(0.0f, Random.Range(-180f, 180f), 0.0f);
        return patrolDirection;
    }

    void UpdatePatrolTimer() {
        if (!targetSpotted && patrolTimer > 0.0f) {
            patrolTimer -= Time.deltaTime;
        }
    }
}
