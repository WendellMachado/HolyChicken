using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : Actor
{
    [SerializeField]
    float minSX, maxSx;

    protected override void Start()
    {
        base.Start();
        float xP = Random.Range(-screenBounds.x, screenBounds.x);
        float yP = Random.Range(-screenBounds.y / 3, screenBounds.y);
        this.transform.position = new Vector2(xP, yP);

        this.viewPos = transform.position;
        this.viewPos.x = Mathf.Clamp(this.viewPos.x, -this.screenBounds.x + this.objectWidth, this.screenBounds.x - this.objectWidth);
        this.viewPos.y = Mathf.Clamp(this.viewPos.y, -this.screenBounds.y + this.objectHeight, this.screenBounds.y - this.objectHeight);
        this.transform.position = viewPos;

        speedX = Random.Range(maxSx, minSX);
        rb.velocity = new Vector2(speedX, speedY);
    }

    protected override void LateUpdate()
    {
        if (this.transform.position.x < -this.screenBounds.x - this.objectWidth)
        {
            float yP = Random.Range(-screenBounds.y, screenBounds.y);
            viewPos = transform.position;
            this.viewPos.y = Mathf.Clamp(this.viewPos.y, -this.screenBounds.y + this.objectHeight, this.screenBounds.y - this.objectHeight);

            this.transform.position = new Vector2(this.screenBounds.x + this.objectWidth, this.viewPos.y);
        }
    }
}
