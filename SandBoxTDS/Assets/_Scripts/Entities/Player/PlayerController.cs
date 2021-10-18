﻿using UnityEngine;

/*
 This class handles player input and stamina logic 
 */

[RequireComponent(typeof(MobileEntity))]
public class PlayerController : Entity, IMobile {
    const float MaxSprintStamina = 100;

    public Weapon CurrentWeapon { get { return Inventory.instance.equipment.Weapons[currentWeapon]; } }
    public float SprintStamina { get; private set; }
    bool CanSprint { get { return SprintStamina > 0.0f; } }

    Camera cam;
    int currentWeapon;
    bool sprinting;

    void Start() {
        cam = Camera.main;
        currentWeapon = 0;
    }

    void Update() {
        UpdateSprint();
    }

    #region Movement
    public (Vector3, int) GetMove() {
        (Vector3, int) result = (new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")), 0);
        if (Input.GetMouseButton(0) && CanAct) {
            if (CurrentWeapon.CanFire) {
                CurrentWeapon.Fire();
            }
            //Aiming animation with laser
        }
        else if (Input.GetMouseButton(1)) {
            //Aiming animation with laser
        }
        else if (Slowed) {
            //Slowed animation
        }
        else if (Input.GetKey(KeyCode.LeftShift) && CanSprint) {
            result.Item2 = 2;
            if (!sprinting) {
                sprinting = true;
            }
            //Sprint animation
        }
        else {
            result.Item2 = 1;
            //Normal run animation
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

    void UpdateSprint() {
        if (sprinting) {
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                sprinting = false;
                return;
            }
            if ((SprintStamina -= Time.deltaTime) < 0.0f) {
                SprintStamina = 0.0f;
                sprinting = false;
            }
        }
        else if (SprintStamina < MaxSprintStamina) {
            if ((SprintStamina += Time.deltaTime) > MaxSprintStamina) { 
                SprintStamina = MaxSprintStamina;
            }
        }
    }
    #endregion
}
