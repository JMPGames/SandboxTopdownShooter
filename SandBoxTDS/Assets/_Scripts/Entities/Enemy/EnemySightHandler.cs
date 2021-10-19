using UnityEngine;

public class EnemySightHandler : MonoBehaviour {
    const float LoseSightMod = 1.5f;

    Transform target;
    EnemyType enemyType;
    public bool TargetSpotted { get; private set; }

    [SerializeField] float sightRange;
    [SerializeField] float minDistanceFromTarget;

    public void Setup(EnemyType enemyType) {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        this.enemyType = enemyType;
    }

    public void CheckForTargetInSight() {
        if (TargetSpotted && DistanceToTarget() > (sightRange * LoseSightMod)) {
            TargetSpotted = false;
        }
        else if (!TargetSpotted && DistanceToTarget() < sightRange) {
            TargetSpotted = true;
        }
    }

    public void GetTargetSpottedFacing(bool critter) {
        Vector3 direction = target.position - transform.position;
        if (TargetTooClose() || critter) {
            transform.LookAt(new Vector3(-direction.x, transform.position.y, -direction.z));
        }
        else {
            transform.LookAt(new Vector3(direction.x, transform.position.y, direction.z));
        }
    }

    public float DistanceToTarget() {
        return Vector3.Distance(transform.position, target.position);
    }

    public bool TargetTooClose() {
        return enemyType == EnemyType.RANGED && DistanceToTarget() <= minDistanceFromTarget;
    }
}
