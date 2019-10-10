using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    Color color = Color.white;
    Animator anim;
    mSceneManagement manager;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mSceneManagement>();
    }

    void VanishEggOver()
    {
        Destroy(this.transform.GetChild(0).gameObject);

        anim.SetTrigger("Move");
    }

    void Restart()
    {
        manager.EggOverRespawn();
        Destroy(this.gameObject);
    }

    public mSceneManagement Manager 
    {
        get { return manager; }
        set { manager = value; }
    }

    void Appear()
    {
        color.a = 1;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }

}
