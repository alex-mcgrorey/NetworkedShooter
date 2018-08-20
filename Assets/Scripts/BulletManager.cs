using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
    private float bulletLifetime = 5;
    private float bulletAge = 0;

    [ServerCallback]
    void Update() {
        bulletAge += Time.deltaTime;
        if (bulletAge > bulletLifetime) {
            NetworkServer.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collision2D collision) {
        GameObject hit = collision.gameObject;
        Health health = hit.GetComponent<Health>();
        if(health != null) {
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
