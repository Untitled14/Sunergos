using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Lever : MonoBehaviour
{
    public Transform LeverObject;
    public float LeverSpeed = 1;
    public float OnAngle = -45;
    public float OffAngle = 45;

    private Trigger _trigger;
    private float _interpolation = 0;
    private bool _inPosition = false;
    // Use this for initialization
    void Start()
    {
        _trigger = GetComponent<Trigger>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLever();
    }

    void UpdateLever()
    {
        if (_trigger.On)
        {
            _interpolation += Time.deltaTime * LeverSpeed;
        }else
        {
            _interpolation -= Time.deltaTime * LeverSpeed;
        }
        if (_interpolation > 1)
            _interpolation = 1;
        else if (_interpolation < 0)
            _interpolation = 0;

        if (_interpolation >= 1 || _interpolation <= 0)
            _trigger.Active = true;
        else
            _trigger.Active = false;
        LeverObject.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(OffAngle, OnAngle, _interpolation));
        
    }
    public bool IsInPosition()
    {
        return _inPosition;
    }
}
