using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    protected Vector2 screenBounds;
    protected Vector3 viewPos;
    protected float objectWidth, objectHeight;
    protected SpriteRenderer sr;

    public static Vector2 DEFAULT_SCREEN_SIZE = new Vector2(1920, 1080);
    protected Vector3 scale;

    [SerializeField]
    private bool isBG;

    protected virtual void Start()
    {
        this.screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        if (this.gameObject.GetComponent<SpriteRenderer>())
        {
            this.sr = this.gameObject.GetComponent<SpriteRenderer>();

            /*if (!isBG)
            {
                this.scale = new Vector3(DEFAULT_SCREEN_SIZE.x / Screen.width, DEFAULT_SCREEN_SIZE.y / Screen.height, 1f);

                this.sr.transform.localScale = Vector3.Scale(this.sr.transform.localScale, this.scale);
                this.sr.transform.position = Vector3.Scale(this.sr.transform.position, this.scale);
            }*/

            if (isBG)
            {
                this.transform.localScale = new Vector3(1, 1, 1);

                float width = this.sr.sprite.bounds.size.x;
                float height = this.sr.sprite.bounds.size.y;


                float worldScreenHeight = Camera.main.orthographicSize * 2f;
                float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

                Vector3 xWidth = this.transform.localScale;
                xWidth.x = worldScreenWidth / width;
                this.transform.localScale = xWidth;
                
                Vector3 yHeight = this.transform.localScale;
                yHeight.y = worldScreenHeight / height;
                this.transform.localScale = yHeight;

            }

            this.objectWidth = this.sr.bounds.extents.x;
            this.objectHeight = this.sr.bounds.extents.y;
        }
    }
}
