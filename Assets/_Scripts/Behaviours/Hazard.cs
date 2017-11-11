using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int Score = 1;
    public float PushBackForce = 10;
    public bool Vertical = false;
    public List<string> DoDamageTo;
    public List<string> TakeDamageFrom;

    void OnCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crushinator")
        {
            Die();
        }
        foreach (var item in DoDamageTo)
        {
            if (collision.tag == item)
            {
                Player player = collision.GetComponent<Player>();
                if (player != null)
                {
                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    if (Vertical)
                        player.TakeDamage(Vector2.up, PushBackForce);
                    else
                        player.TakeDamage(direction, PushBackForce);
                }
            }
        }
        foreach (var item in TakeDamageFrom)
        {
            if (collision.tag == item)
            {
                Player player = collision.GetComponent<Player>();
                if (player != null)
                {
                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    if (Vertical)
                        player.Push(Vector2.up, PushBackForce);
                    else
                        player.Push(direction, PushBackForce);
                    Die();
                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision.collider);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnCollision(collision);
    }
    void Die()
    {
        LevelController.Instance.AddScore(Score);
        Destroy(gameObject);
    }
}
