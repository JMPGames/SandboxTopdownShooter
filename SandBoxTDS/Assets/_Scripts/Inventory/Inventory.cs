using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public Inventory instance;

    int gold;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public int GetGold() { return gold; }
    public bool CanAfford(int price) { return gold >= price; }
    public void AdjustGold(int amt) { gold = (gold + amt) < 0 ? 0 : gold + amt; }
}
