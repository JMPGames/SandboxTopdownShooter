using UnityEngine;

public class CameraController : MonoBehaviour {
    const float Smoothing = 150f;

    Transform player;
    float yOffset;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        yOffset = transform.position.y - player.position.y;
    }

    void LateUpdate() {
        Vector3 p = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.Lerp(transform.position, p, Smoothing * Time.deltaTime);
    }
}
