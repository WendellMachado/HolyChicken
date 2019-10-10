using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : Enemy
{
    SpriteRenderer sickleSR;
    Transform sickle;

    protected override void Start()
    {
        base.Start();

        sickle = this.transform.GetChild(0);

        sickleSR = sickle.gameObject.GetComponent<SpriteRenderer>();

        color.a = 0;
        sr.color = color;
        sickleSR.color = color;
        StartCoroutine("FadeIn");

        RandomizePosition();
    }

    private void Update()
    {
        sickle.Rotate(0, 0, 2.5f * (Time.deltaTime * 60));
    }

    public override IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(fadeInterval);

        if (sr.color.a < 1 && sickleSR.color.a < 1)
        {
            color.a += fadeSpeed;
            sr.color = color;
            sickleSR.color = color;
            StartCoroutine("FadeIn");
        }
        else
        {
            canKill = true;
            transform.GetChild(0).gameObject.GetComponent<Enemy>().gCanKill = true;
            Invoke("Die", timeToDie);
        }
    }

    public override IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeInterval);

        if (sr.color.a > 0 && sickleSR.color.a > 0)
        {
            color.a -= fadeSpeed;
            sr.color = color;
            sickleSR.color = color;
            StartCoroutine("FadeOut");
        }
        else
        {
            manager.gEnemiesOnScreen = manager.gEnemiesOnScreen - 1;
            Destroy(this.gameObject);
        }
    }
}
