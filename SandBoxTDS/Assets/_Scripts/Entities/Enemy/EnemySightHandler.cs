using UnityEngine;

public class EnemySightHandler : MonoBehaviour {
    [SerializeField] float sightRange;
    [SerializeField] float loseSightMod = 1.5f;
    [SerializeField] float minDistanceFromTarget;

    Transform target;
    bool targetSpotted;

    public void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool CheckForTargetInSight() {
        if (targetSpotted && DistanceToTarget() > (sightRange * loseSightMod)) {
            targetSpotted = false;
        }
        else if (!targetSpotted && DistanceToTarget() < sightRange) {
            targetSpotted = true;
        }
        return targetSpotted;
    }

    public Vector3 GetTargetSpottedFacing(bool critter) {
        Vector3 direction = target.position - transform.position;
        if (TargetTooClose() || critter) {
            return new Vector3(-direction.x, transform.position.y, -direction.z);
        }
        return new Vector3(direction.x, transform.position.y, direction.z);
    }

    public float DistanceToTarget() {
        return Vector3.Distance(transform.position, target.position);
    }

    public bool TargetTooClose() {
        return DistanceToTarget() <= minDistanceFromTarget;
    }
}
