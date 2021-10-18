using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour, IUsable, IStackable {
    public bool Usable() {
        throw new System.NotImplementedException();
    }

    public void Use(Entity target = null) {
        throw new System.NotImplementedException();
    }

    public void AddToStack(int amount) {
        throw new System.NotImplementedException();
    }

    public void RemoveFromStack(int amount) {
        throw new System.NotImplementedException();
    }
}
