using UnityEngine;
using System.Collections;

public class Weapon {
    public Type type;
    private int ammo;
    private float fireRate;

    public float bulletSpeed;
    public GameObject Bullet;
    private float nextFireTimer;

    public Weapon(Type type, int ammo, float fireRate, float bulletSpeed) {
        this.type = type;
        this.ammo = ammo;
        this.fireRate = fireRate;
        this.bulletSpeed = bulletSpeed;
    } 

    public void AddAmmo(int amount) {
        this.ammo += amount;
    }

    public GameObject Fire(GameObject player) {
        if(Time.time > nextFireTimer && ammo > 0) {
            nextFireTimer = Time.time + fireRate;
            GameObject bullet = Bullet;
            bullet.transform.position = player.transform.Find("BulletSpawn").position;
            bullet.transform.rotation = player.transform.rotation;

            return bullet;
        }
        return null;
    }


}
