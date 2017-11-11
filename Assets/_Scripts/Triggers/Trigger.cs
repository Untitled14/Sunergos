using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour {
    public bool Active = true;
    public bool On;
    public bool Permament;
    public void TurnOn()
    {
        if (!Active)
            return;
        On = true;
    }
    public void TurnOff()
    {
        On = false;
        if (Permament)
            On = true;
    }
    public void Toggle()
    {
        if (!Active)
            return;
        On = !On;
        if (Permament)
            On = true;
    }
}
