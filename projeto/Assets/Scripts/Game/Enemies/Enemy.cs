using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField]
    protected float timeToDie;

    protected bool canKill;

    protected mSceneManagement manager;

    protected Transform target;

    protected override void Start()
    {
        base.Start();
    }

    protected override void LateUpdate()
    {
        return;
    }

    public float gTimeToDie { get { return timeToDie; } set { timeToDie = value; } }
    public bool gCanKill { get { return canKill; } set { canKill = value; } }

    public mSceneManagement gManager { get { return manager; } set { manager = value; } }

    public override IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(fadeInterval);

        if (sr.color.a < 1)
        {
            color.a += fadeSpeed;
            sr.color = color;
            StartCoroutine("FadeIn");
        }
        else
        {
            canKill = true;
            Invoke("Die", timeToDie);
        }
    }

    public override IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeInterval);

        if (sr.color.a > 0)
        {
            color.a -= fadeSpeed;
            sr.color = color;
            StartCoroutine("FadeOut");
        }
        else
        {
            manager.gEnemiesOnScreen = manager.gEnemiesOnScreen - 1;
            Destroy(this.gameObject);
        }
    }

    protected virtual void Die()
    {
        StartCoroutine("FadeOut");
    }

    protected virtual void FollowTarget()
    {
        if (target != null)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, speedX);
        }
    }

    protected virtual void RandomizePosition()
    {
        float xP = Random.Range(-screenBounds.x, screenBounds.x);
        float yP = Random.Range(-screenBounds.y, screenBounds.y);
        this.transform.position = new Vector2(xP, yP);

        this.viewPos = transform.position;
        this.viewPos.x = Mathf.Clamp(this.viewPos.x, -this.screenBounds.x + this.objectWidth, this.screenBounds.x - this.objectWidth);
        this.viewPos.y = Mathf.Clamp(this.viewPos.y, -this.screenBounds.y + this.objectHeight, this.screenBounds.y - this.objectHeight);
        this.transform.position = viewPos;
    }
}


