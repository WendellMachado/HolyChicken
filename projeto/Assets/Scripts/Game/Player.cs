using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Actor
{
    [SerializeField]
    GameObject feathers;

    private Vector2 targetPos;
    private Camera cam;
    private mSceneManagement manager;
    Animator anim;

    [SerializeField]
    float fadeOutSpeed, fadeOutInterval;

    bool dead, gameStarted;

    protected override void Start()
    {
        base.Start();

        color.a = 0;
        sr.color = color;
        StartCoroutine("FadeIn");

        anim = this.gameObject.GetComponent<Animator>();

        cam = Camera.main;
    }

    void Update()
    {
        if (dead) { this.transform.Translate(0, (Time.deltaTime * 60) * 0.01f, 0); return; }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(feathers, this.transform.position, feathers.transform.rotation);

            StopAllCoroutines();
            color.a = 0;
            sr.color = color;

            StartCoroutine("FadeIn");
        }

        if (Input.GetMouseButton(0))
        {
            targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = targetPos;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Enemy") && !dead)
        {
            if (c.gameObject.GetComponent<Enemy>().gCanKill)
            {
                dead = true;
                StopAllCoroutines();
                ChangeAlpha = 1;
                anim.SetTrigger("Dying");
                sr.sortingLayerName = "UI";
                StartCoroutine("FadeOut");
                manager.CallGameOver();
            }
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.CompareTag("Enemy") && !dead)
        {
            if (c.gameObject.GetComponent<Enemy>().gCanKill)
            {
                dead = true;
                StopAllCoroutines();
                ChangeAlpha = 1;
                anim.SetTrigger("Dying");
                sr.sortingLayerName = "UI";
                StartCoroutine("FadeOut");
                manager.CallGameOver();
            }
        }
    }

    public override IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutInterval);

        if (sr.color.a > 0)
        {
            color.a -= fadeOutSpeed;
            sr.color = color;
            StartCoroutine("FadeOut");
        }
        else
        {
            Destroy(this.gameObject);
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
            if (!gameStarted)
            {
                gameStarted = true;
                gManager.SpawnEnemies();
            }
        }
    }

    public mSceneManagement gManager { get { return manager; } set { manager = value; } }

}
