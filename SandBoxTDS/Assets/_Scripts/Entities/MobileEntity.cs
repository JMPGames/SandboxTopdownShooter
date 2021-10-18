using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class handles the game Entities movement, speed and facing.
    Requires Unities Rigidbody component to handle movement.

    Self contained variables
 */

[RequireComponent(typeof(Rigidbody))]
public class MobileEntity : MonoBehaviour {
    Entity entity;
    (Vector3, int) move;
    Vector3 Velocity { get { return move.Item1 * speeds[move.Item2]; } }
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
        if (entity.CanMove) {
            if (entity.EntityType == EntityType.PLAYER) {
                move = (entity as PlayerController).GetMove();
            }
            else if (entity.EntityType == EntityType.NPC) {
                //move = (entity as NPCController).GetMove();
            }
            else {
                move = (entity as EnemyController).GetMove();
            }
        }
    }

    void FixedUpdate() {
        if (entity.CanMove) {
            Move();
        }
    }

    void Move() {
        rb.velocity = Velocity;
    }
}
