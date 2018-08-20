using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public enum Type {
    rifle, shotgun
}

public class WeaponDropped : NetworkBehaviour {
    private Weapon weapon;
    private Weapon rifle = new Weapon(Type.rifle, 30, 0.5f, 25f);
    private Weapon shotgun = new Weapon(Type.shotgun, 15, 1f, 15f);
    public Type type;

    private void Start() {
        switch (type) {
            case Type.rifle:
                weapon = rifle;
                break;
            case Type.shotgun:
                weapon = shotgun;
                break;
            default:
                break;
        }
        weapon.Bullet = Resources.Load("Prefabs/Bullet") as GameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {

            collision.gameObject.GetComponent<PlayerManager>().weapon = weapon;
            collision.gameObject.GetComponent<PlayerManager>().ChangeWeapon(weapon.type);
            Destroy(this.gameObject);
        }
    }
}
