using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeatAnimation : MonoBehaviour
{
    public SpriteRenderer HeatSpriteRenderer;
    private Color _color;

    public float HeatLevel;
    private bool _heating = false;
    private void Start()
    {
        _color = HeatSpriteRenderer.color;
    }
    // Update is called once per frame
    void Update()
    {
        if (_heating) _color.a += Time.deltaTime/4;
        else _color.a -= Time.deltaTime/8;

        if (_color.a < 0) _color.a = 0;
        else if (_color.a > 1) _color.a = 1;

        HeatSpriteRenderer.color = _color;
        _heating = false;
    }

    public void HeatUp()
    {
        _heating = true;
    }


}
