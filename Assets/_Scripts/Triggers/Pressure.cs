using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure : Trigger {
    public List<GameObject> TriggerGameObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var trigger in TriggerGameObject)
        {
            if (collision.gameObject == trigger)
            {
                TurnOn();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var trigger in TriggerGameObject)
        {
            if (collision.gameObject == trigger)
            {
                TurnOff();
            }
        }
    }
}
