using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HellPortal : Ghost
{
    [SerializeField]
    GameObject bullet;

    protected override void Start()
    {
        base.Start();

        Invoke("SpawnBullet", timeToDie / 2);
    }

    void SpawnBullet()
    {
        Bullet nBullet;
        nBullet = (Bullet) Instantiate(bullet, this.transform.position, Quaternion.identity).gameObject.GetComponent<Bullet>();
        nBullet.gManager = this.manager;
        nBullet.setGuided = true;
        manager.gEnemiesOnScreen++;
    }
}
