using UnityEngine;

[RequireComponent(typeof(MobileEntity))]
public class PlayerController : Entity, IMobile {
    Camera cam;

    void Start() {
        cam = Camera.main;
    }

    public (Vector3, int) GetMove(bool slowed) {
        (Vector3, int) result = (Vector3.zero, 0);
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        if (Input.GetMouseButton(0) || slowed) {
            //Shooting animation
            //Ammo check
            Shoot();
            result = (input, 0);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) {
            //Sprint animation
            //Sprint stamina check
            result = (input, 2);
        }
        else {
            //Normal run animation
            result = (input, 1);
        }
        AimAtMouseCursor();
        return result;
    }

    void AimAtMouseCursor() {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(camRay, out float rayLength)) {
            Vector3 mousePosition = camRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(mousePosition.x, transform.position.y, mousePosition.z));
        }
    }

    void Shoot() {
        //Check ammo/direction/weapon type/weapon fire interval
        //instantiate bullet/melee attack
    }
}
