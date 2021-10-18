using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerEquipment))]
public class Inventory : MonoBehaviour {
    public static Inventory instance;
    public PlayerEquipment equipment;

    public int Gold { get; set; }

    void Awake() {
        if(instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        equipment = GetComponent<PlayerEquipment>();
    }

    public void ShowTooltip(Vector3 position, Item item) {

    }

    public void HideTooltip() {

    }
}
