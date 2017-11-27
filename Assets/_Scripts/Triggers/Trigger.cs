using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour {
    public string AudioSource = "";
    public bool Active = true;
    public bool On;
    public bool Permament;
    public void TurnOn()
    {
        if (!Active)
            return;
        On = true;
        AudioController.Instance.PlaySound(AudioSource, 0.02f);
    }
    public void TurnOff()
    {
        if (Permament)
            return;
        On = false;
        AudioController.Instance.PlaySound(AudioSource, 0.02f);
    }
    public void Toggle()
    {
        if (!Active)
            return;
        On = !On;
        if (Permament)
            On = true;
        AudioController.Instance.PlaySound(AudioSource, 0.02f);
    }
}
