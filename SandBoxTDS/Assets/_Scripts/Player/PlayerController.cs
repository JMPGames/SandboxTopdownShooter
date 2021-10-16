using UnityEngine;

public class PlayerController : MonoBehaviour {
    const float ShootingMoveSpeed = 4.5f;
    const float NormalMoveSpeed = 7.5f;
    const float SprintMoveSpeed = 10.0f;

    Vector3 velocity;
    Rigidbody rb;
    Camera cam;

    void Start() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update() {
        if (CanMove()) {
            GetMoveInput();
            AimAtMouseCursor();
        }
    }

    void FixedUpdate() {
        Move();
    }

    void GetMoveInput() {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        if (Input.GetMouseButton(0) || Slowed()) {
            //Shooting animation
            //Ammo check
            Shoot();
            velocity = input * ShootingMoveSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift)) {
            //Sprint animation
            //Sprint stamina check
            velocity = input * SprintMoveSpeed;
        }
        else {
            //Normal run animation
            velocity = input * NormalMoveSpeed;
        }
    }

    bool CanMove() {
        //Check for interacting/menus/rolling/etc
        return true;
    }

    void Move() {
        if (CanMove()) {
            rb.velocity = velocity;
        }
    }

    bool Slowed() {
        //Check for reload, attacking animation
        return false;
    }

    void Shoot() {
        //Check ammo/direction/weapon type/weapon fire interval
        //instantiate bullet/melee attack
    }

    void AimAtMouseCursor() {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(camRay, out float rayLength)) {
            Vector3 mousePosition = camRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(mousePosition.x, transform.position.y, mousePosition.z));
        }
    }
}
