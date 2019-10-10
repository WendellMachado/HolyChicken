using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGod : MonoBehaviour
{
    Animator ant;
    int state;

    void Start()
    {
        ant = this.GetComponent<Animator>();
        state = Mathf.RoundToInt(Random.Range(0, 1));
        ant.SetInteger("State", state);
    }

    void Change()
    {
        state++;
        state = Mathf.RoundToInt(Mathf.PingPong(state, 1));
        ant.SetInteger("State", state);
    }
}
