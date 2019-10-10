using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hord : Enemy
{
    [SerializeField]
    GameObject bullet;

    protected override void Start()
    {
        base.Start();

        ChangeAlpha = 0;

        RandomizePosition();

        StartCoroutine("FadeIn");

        Invoke("Detach", timeToDie / 2);
    }

    void Detach()
    {
        ChangeAlpha = 0;

        Bullet nBullet;

        for (int i = 0; i < 4; i++)
        {
            nBullet = (Bullet) Instantiate(bullet, this.transform.position, bullet.transform.rotation).GetComponent<Bullet>();
            nBullet.setGuided = false;
            nBullet.gManager = this.manager;
            manager.gEnemiesOnScreen++;
        }
        manager.gEnemiesOnScreen--;
        Destroy(this.gameObject);
    }

    private void Update()
    {
       this.transform.Rotate(0, 0, 2.5f * (Time.deltaTime * 60));
    }
}
