﻿using UnityEngine;

public enum EnemyType { RANGED, MELEE, PATTERNED }

[RequireComponent(typeof(MobileEntity))]
[RequireComponent(typeof(EntityStatusEffectHandler))]
[RequireComponent(typeof(EnemySightHandler))]
[RequireComponent(typeof(PatrolHandler))]
public class EnemyController : Entity, IMobile {
    EntityStatusEffectHandler seHandler;
    EnemySightHandler sightHandler;
    PatrolHandler patrolHandler;

    float attackTimer;

    #region Inspector Variables
    [SerializeField] EnemyType enemyType;
    [SerializeField] int damage;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackToMoveTime;
    #endregion

    void Start() {
        sightHandler = GetComponent<EnemySightHandler>();
        patrolHandler = GetComponent<PatrolHandler>();
        sightHandler.Setup(enemyType);
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
            return (Vector3.forward, Slowed() ? 0 : 1);
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
        //animation, fire bullet/melee hit check, sound
        EntityState = EntityState.SLOWED_NOACT;
        NextEntityState = EntityState.NOACT;
        attackTimer = attackSpeed;
        StateTimer = attackToMoveTime;
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
