using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Ghost : Enemy
{
    protected override void Start()
    {
        base.Start();

        ChangeAlpha = 0;

        RandomizePosition();

        StartCoroutine("FadeIn");

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }
}
