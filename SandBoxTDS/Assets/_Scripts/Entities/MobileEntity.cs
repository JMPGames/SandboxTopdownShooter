using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MobileEntity : MonoBehaviour {
    Entity entity;
    (Vector3, int) move;
    Rigidbody rb;
    float[] speeds;

    #region Inspector Variables
    [SerializeField] float maxSpeed;
    [SerializeField] float normalSpeed;
    [SerializeField] float minSpeed;
    #endregion

    void Awake() {
        speeds = new float[3] { minSpeed, normalSpeed, maxSpeed };
    }

    void Start() {
        entity = GetComponent<Entity>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (entity.CanMove() && entity is IMobile) {
            move = (entity as IMobile).GetMove();
            //GetMove();
        }
    }

    void FixedUpdate() {
        if (entity.CanMove()) {
            Move();
        }
    }

    /*
    void GetMove() {
        if (entity.EntityType == EntityType.PLAYER) {
            move = (entity as PlayerController).GetMove();
        }
        else if (entity.EntityType == EntityType.NPC) {
            move = (entity as NPCController).GetMove();
        }
        else {
            move = (entity as EnemyController).GetMove();
        }
    }
    */

    void Move() {
        rb.velocity = move.Item1 * speeds[move.Item2];
    }
}
