using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bullet : Enemy
{
    float[] directions;
    bool guided;

    protected override void Start()
    {
        base.Start();

        directions = new float[] { 0, 45, 45 * 2, 45 * 3, 45 * 4, 45 * 5, 45 * 6, 45 * 7 };


        if (!guided)
        {
            this.transform.Rotate(0, 0, directions[Mathf.RoundToInt(Random.Range(0, directions.Length))]);
            ChangeAlpha = 1;
            canKill = true;
            rb.velocity = transform.right * speedX;
            Invoke("Die", timeToDie);
        }
        else 
        {
            ChangeAlpha = 0;

            StartCoroutine("FadeIn");

            target = GameObject.FindGameObjectWithTag("Player").transform;

            if (guided && target != null)
            {
                transform.right = target.position - transform.position;
            }
        }
        
    }

    public override IEnumerator FadeIn()
    {

        yield return new WaitForSeconds(fadeInterval);

        if (guided && target != null)
        {
            transform.right = target.position - transform.position;
        }

        if (sr.color.a < 1)
        {
            color.a += fadeSpeed;
            sr.color = color;
            StartCoroutine("FadeIn");
        }
        else
        {
            canKill = true;
            rb.velocity = transform.right * speedX;
            Invoke("Die", timeToDie);
        }
    }

    protected override void LateUpdate()
    {
        this.viewPos = transform.position;
        if (this.viewPos.x < -this.screenBounds.x - this.objectWidth || this.viewPos.x > this.screenBounds.x + this.objectWidth
            || this.viewPos.y < -this.screenBounds.y - this.objectHeight || this.viewPos.y > this.screenBounds.y + this.objectHeight)
        {
            gManager.gEnemiesOnScreen--;
            Destroy(this.gameObject);
        }
    }

    public bool setGuided
    {
        get { return guided; }
        set { guided = value; }
    }
}
