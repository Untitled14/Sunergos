using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Lamp : MonoBehaviour
{

    public Color OnColor = Color.green;
    public Color OffColor = Color.red;
    public SpriteRenderer Aura;
    public Color AuraOnColor = Color.green;
    public Color AuraOffColor = Color.red;

    private TriggerConnection _trigger;
    private SpriteRenderer _sr;

    // Use this for initialization
    void Start()
    {
        _trigger = GetComponent<TriggerConnection>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_trigger != null)
        {
            if (_trigger.Triggered)
            {
                _sr.color = OnColor;
                if(Aura != null)
                    Aura.color = AuraOnColor;
            }
            else
            {
                _sr.color = OffColor;
                if(Aura != null)
                    Aura.color = AuraOffColor;
            }
        }

    }
}
