using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Scaler
{
    protected Rigidbody2D rb;
    protected Color color;

    [SerializeField]
    protected float speedX, speedY, fadeSpeed = 0.1f, fadeInterval = 0.5f;

    public virtual IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeInterval);

        if (sr.color.a > 0)
        {
            color.a -= fadeSpeed;
            sr.color = color;
            StartCoroutine("FadeOut");
        }
    }

    public virtual IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(fadeInterval);

        if (sr.color.a < 1)
        {
            color.a += fadeSpeed;
            sr.color = color;
            StartCoroutine("FadeIn");
        }
    }

    protected override void Start()
    {
        base.Start();

        color = Color.white;

        if (this.gameObject.GetComponent<Rigidbody2D>())
        { 
            this.rb = this.gameObject.GetComponent<Rigidbody2D>(); 
        }
    }

    protected virtual void LateUpdate()
    {
        this.viewPos = transform.position;
        this.viewPos.x = Mathf.Clamp(this.viewPos.x, -this.screenBounds.x + this.objectWidth, this.screenBounds.x - this.objectWidth);
        this.viewPos.y = Mathf.Clamp(this.viewPos.y, -this.screenBounds.y + this.objectHeight, this.screenBounds.y - this.objectHeight);
        this.transform.position = viewPos;
    }

    public SpriteRenderer Gsr { get { return this.sr; }}

    public Color Gcolor { get { return this.color; } set { this.color = value; this.sr.color = this.color;} }

    public Rigidbody2D Grb { get { return this.rb;} }

    public float ChangeAlpha
    {
        get { return sr.color.a; }
        set { color.a = value; sr.color = color; }
    }
}
