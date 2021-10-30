using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MobileEntity : MonoBehaviour {
    Entity entity;
    (Vector3, int) move;
    Rigidbody rb;
    float[] speeds;

    [SerializeField] float minSpeed = 3.0f;
    [SerializeField] float normalSpeed = 5.0f;
    [SerializeField] float maxSpeed = 7.0f;

    void Awake() {
        speeds = new float[3] { minSpeed, normalSpeed, maxSpeed };
    }

    void Start() {
        entity = GetComponent<Entity>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        if (entity.CanMove()) {
            move = (entity as IMobile).GetMove();
        }
    }

    void FixedUpdate() {
        if (entity.CanMove()) {
            rb.velocity = move.Item1 * speeds[move.Item2];
        }
    }
}
