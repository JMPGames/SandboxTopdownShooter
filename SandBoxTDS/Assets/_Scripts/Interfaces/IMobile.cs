using UnityEngine;

public interface IMobile {
    (Vector3, int) GetMove(bool slowed = false);
}