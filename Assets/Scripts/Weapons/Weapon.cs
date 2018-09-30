﻿using UnityEngine;
using System.Collections;

public enum Type {
    rifle, shotgun, smg, unarmed
}


public class Weapon {
    public Type type;
    protected int ammo;
    protected float fireRate;
    protected float bulletSpeed;
    protected float zoom;
    public GameObject Bullet;
    private float nextFireTimer;

    public Weapon(Type type) {
        this.type = type;
    }

    public void AddAmmo(int amount) {
        this.ammo += amount;
    }

    public float getBulletSpeed() {
        return bulletSpeed;
    }

    public GameObject Fire(GameObject player) {
        if (Time.time > nextFireTimer && ammo > 0) {
            nextFireTimer = Time.time + fireRate;
            GameObject bullet = Bullet;
            bullet.transform.position = player.transform.Find("BulletSpawn").position;
            bullet.transform.rotation = player.transform.rotation;

            return bullet;
        }
        return null;
    }


}