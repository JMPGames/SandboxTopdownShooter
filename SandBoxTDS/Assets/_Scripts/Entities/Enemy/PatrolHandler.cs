using UnityEngine;

public class PatrolHandler : MonoBehaviour {
    [SerializeField] float minPatrolTime = 3.0f;
    [SerializeField] float maxPatrolTime = 6.0f;

    Vector3 patrolDirection;
    float patrolTimer;

    void Update() {
        UpdatePatrolTimer();
    }

    public bool Patrolling() {
        return patrolTimer > 0.0f;
    }

    public Vector3 GetRandomFacing() {
        if (Patrolling()) {
            return patrolDirection;
        }
        patrolDirection = new Vector3(0.0f, Random.Range(-180f, 180f), 0.0f);
        patrolTimer = Random.Range(minPatrolTime, maxPatrolTime);
        return patrolDirection;
    }

    void UpdatePatrolTimer() {
        if (Patrolling()) {
            patrolTimer -= Time.deltaTime;
        }
    }
}
