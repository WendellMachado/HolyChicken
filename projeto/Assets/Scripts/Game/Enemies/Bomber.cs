using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy
{
    Animator anim;

    [SerializeField]
    GameObject bomb, bomberV;

    [SerializeField]
    bool animated;

    protected override void Start()
    {
        base.Start();

        anim = this.gameObject.GetComponent<Animator>();

        if (!animated)
        {
            ChangeAlpha = 0;

            RandomizePosition();

            StartCoroutine("FadeIn");
        }
        else 
        {
            canKill = true;
        }
    }

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
            //Invoke("Die", timeToDie);
            Bomber nb;
            nb = (Bomber) Instantiate(bomberV, this.transform.position, this.transform.rotation).GetComponent<Bomber>();
            nb.gManager = this.manager;

            manager.gEnemiesOnScreen--;
            Destroy(this.gameObject);
        }
    }

    void CreateBomb()
    {

    }

    void Disappear()
    {
        canKill = false;
        ChangeAlpha = 0;
        gManager.gEnemiesOnScreen--;
        Destroy(this.gameObject);
    }
}
