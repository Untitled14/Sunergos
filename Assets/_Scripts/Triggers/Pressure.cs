using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure : Trigger {
    public List<GameObject> TriggerGameObject;

    private int _triggerCount = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var trigger in TriggerGameObject)
        {
            if (collision.gameObject == trigger)
            {
                _triggerCount++;
                //TurnOn();
            }
        }
        if(_triggerCount >= TriggerGameObject.Count)
        {
            TurnOn();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var trigger in TriggerGameObject)
        {
            if (collision.gameObject == trigger)
            {
                _triggerCount--;
                //TurnOff();
            }
        }
        if (_triggerCount < TriggerGameObject.Count)
        {
            TurnOff();
        }
    }
}
