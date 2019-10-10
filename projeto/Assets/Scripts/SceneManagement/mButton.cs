using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mButton : Scaler
{
    [SerializeField]
    GameObject receiver;

    [SerializeField]
    string message;

    void OnMouseDown()
    {
        if (sr.color.a >= 1)
        {
            receiver.SendMessage(message);
        }
    }

    public GameObject setReceiver
    {
        get { return receiver; }
        set { receiver = value; }
    }
}
