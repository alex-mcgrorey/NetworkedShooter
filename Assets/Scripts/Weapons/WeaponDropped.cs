using UnityEngine;
//using System.Collections;
//using UnityEngine.Networking;

public enum Type {
    rifle, shotgun, unarmed
}

public class WeaponDropped : MonoBehaviour {
    private Weapon weapon;
    private Weapon rifle = new Weapon(Type.rifle, 30, 0.5f, 25f);
    private Weapon shotgun = new Weapon(Type.shotgun, 15, 1f, 15f);
    private Weapon unarmed = new Weapon(Type.unarmed, 0, 0, 0);
    public Type type;

    public Weapon GetWeapon() {
        return this.weapon;
    }

    public Type GetWeaponType() {
        return this.type;
    }

    private void Start() {
        switch (type) {
            case Type.rifle:
                weapon = rifle;
                break;
            case Type.shotgun:
                weapon = shotgun;
                break;
            case Type.unarmed:
                weapon = unarmed;
                break;
            default:
                break;
        }
        weapon.Bullet = Resources.Load("Prefabs/Bullet") as GameObject;
    }
}
