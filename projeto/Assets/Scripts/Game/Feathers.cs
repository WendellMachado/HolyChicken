using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feathers : Actor
{
    protected override void Start()
    {
        base.Start();

        StartCoroutine("FadeOut");
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
            Destroy(this.gameObject);
        }
    }

    protected override void LateUpdate()
    {
        return;
    }
}
