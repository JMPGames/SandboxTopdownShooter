using UnityEngine;

public class Projectile : MonoBehaviour {
    int damage;
    int penetrations;
    bool playerProjectile;

    [SerializeField] string title;
    [SerializeField] float range_inSeconds;
    [SerializeField] float speed;
    [SerializeField] bool explosive;
    [SerializeField] float explosionRange;
    [SerializeField] bool penetrates;
    [SerializeField] int maxPenetrations;

    void FixedUpdate() {
        //transform.position += Vector3.forward * (speed * Time.delta)
    }

    public string GetDetails() {
        return $"{title}\nRange: {range_inSeconds}\nSpeed: {speed}";
    }

    public void Fire(bool playerProjectile, int damage) {
        this.playerProjectile = playerProjectile;
        this.damage = damage;
        penetrations = maxPenetrations;
        Destroy(gameObject, range_inSeconds);
    }

    void OnCollisionEnter(Collision other) {
        if (playerProjectile) {
            if (other.gameObject.CompareTag("Enemy")) {
                if (penetrates && penetrations > 0) {
                    penetrations--;
                    return;
                }
                if (explosive) {
                    //explosion by explosionRange
                }
                other.gameObject.GetComponent<Entity>().LoseHealth(damage);
            }
        }
        else {
            if (other.gameObject.CompareTag("Player")) {
                other.gameObject.GetComponent<Entity>().LoseHealth(damage);

            }
        }
        //TODO:::# Object pooling
        Destroy(gameObject);
    }
}
