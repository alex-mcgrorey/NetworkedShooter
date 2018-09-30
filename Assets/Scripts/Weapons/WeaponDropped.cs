using UnityEngine;

public class WeaponDropped : MonoBehaviour {
    private Weapon weapon;
    public Type type;

    void Start() {
        switch (type) {
            case Type.rifle:
                weapon = new Rifle();
                break;
            case Type.shotgun:
                weapon = new Shotgun();
                break;
            case Type.smg:
                weapon = new SMG();
                break;
            default:
                weapon = new Unarmed();
                break;
        }
        weapon.Bullet = Resources.Load("Prefabs/Bullet") as GameObject;
    }

    public Weapon GetWeapon() {
        return this.weapon;
    }

    public Type GetWeaponType() {
        return this.type;
    }
}
