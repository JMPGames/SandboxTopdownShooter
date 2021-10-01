using UnityEngine;

public class PlayerController : MonoBehaviour {
    const float MoveSpeed = 7.5f;

    Vector3 moveInput;
    Vector3 velocity;
    Rigidbody rb;
    Camera cam;

    void Start() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update() {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        velocity  = moveInput * MoveSpeed;

        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(camRay, out float rayLength)) {
            Vector3 mousePosition = camRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(mousePosition.x, transform.position.y, mousePosition.z));
        }
    }

    void FixedUpdate() {
        rb.velocity = velocity;
    }
}
