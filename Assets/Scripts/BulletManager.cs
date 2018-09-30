using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletManager : NetworkBehaviour {

    void Update() {

    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject hit = collision.gameObject;
        Health health = hit.GetComponent<Health>();
        if(health != null) {
            health.TakeDamage(10);
        }

        NetworkServer.Destroy(gameObject);
    }
}