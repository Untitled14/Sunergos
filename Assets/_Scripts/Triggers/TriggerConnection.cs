using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TriggerConnection : MonoBehaviour
{
    public float Delay = 0;
    public List<Trigger> Triggers;
    [HideInInspector]
    public bool Triggered;

    private bool _allOn = false;
    private float _interploation = 0;
    private void Start()
    {
        if (Triggers == null)
            Triggers = new List<Trigger>();
    }
    // Update is called once per frame
    void Update()
    {
        _allOn = IsTriggered();
        if (!_allOn)
        {
            Triggered = false;
            _interploation = 0;
        }
        else
            UpdateDelay();

    }
    void UpdateDelay()
    {
        if (_interploation >= Delay)
            Triggered = true;
        else
            _interploation += Time.deltaTime;

    }
    bool IsTriggered()
    {
        if (Triggers.Count == 0)
            return Triggered;
        bool On = true;
        foreach (var trigger in Triggers)
        {
            if (trigger != null)
            {
                Color color = Color.red;
                if (trigger.On)
                    color = Color.green;
                else
                    On = false;
                Debug.DrawLine(transform.position, trigger.transform.position, color);
            }
            else
                return Triggered;
        }

        return On;
    }


}

