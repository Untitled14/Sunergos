using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractGameObject
{
    public int ID;
    public float MaxSpeed = 4;
    public float Acc = 1;
    [Range(1, 2)]
    public int JumpCount = 1;
    public float JumpForce = 5;
    [Range(0, 1)]
    public float Deacc = 0.9f;

    [HideInInspector]
    public bool ActionKey = false;
    public bool Active = true;
    [HideInInspector]
    public bool Grounded = false;
    [HideInInspector]
    public bool IsTakingDamage = false;

    [HideInInspector]
    public Vector2 OriginVelocity;

    public Color DamageColor = Color.red;

    public ParticleSystem ParticleTrail;
    private bool _particlesPlaying;
    private string _id;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private HeatAnimation _ha;
    private Color _spriteColor;

    private int _jumpCount = 0;
    // Use this for initialization
    void Start()
    {
        _id = "P" + ID + "_";
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _ha = GetComponent<HeatAnimation>();
        _spriteColor = _sr.color;
    }
    void UpdatePose()
    {
        float angle = transform.localRotation.eulerAngles.z;
        if (angle > 180)
            angle -= 360;

        if (angle > 30)
        {
            _rb.angularVelocity = -45;
        }
        else if (angle < -30)
        {
            _rb.angularVelocity = 45;
        }
        _rb.angularVelocity *= 0.9f;
    }
    void UpdateActionKey()
    {
        ActionKey = false;
        if (Input.GetButtonDown(_id + "Action"))
        {
            ActionKey = true;
        }
    }
    void UpdateJump()
    {
        if (Grounded)
            _jumpCount = 0;
        if (Input.GetButtonDown(_id + "Jump") && Grounded)
        {
            Jump();
            return;
        }
        if (Input.GetButtonDown(_id + "Jump") && _jumpCount < JumpCount - 1)
        {
            Jump();
        }
    }
    void Jump()
    {
        if (gameObject.tag == "Circle")
            AudioController.Instance.PlaySound("circle jump", 0.02f);
        else if (gameObject.tag == "Square")
            AudioController.Instance.PlaySound("square jump", 0.02f);
        Vector2 velocity = _rb.velocity;
        velocity.y = JumpForce;
        _rb.velocity = velocity;
        _jumpCount++;
    }
    void UpdateMovement()
    {
        float direction = Input.GetAxisRaw(_id + "Horizontal");
        Vector2 velocity = _rb.velocity - OriginVelocity;
        if (Mathf.Abs(velocity.x) <= MaxSpeed)
        {
            velocity.x += Acc * direction;
        }
        velocity.x *= Deacc;
        _rb.velocity = velocity + OriginVelocity;

    }
    public void Push(Vector2 direction, float force = 5)
    {
        _rb.velocity = direction * force;
    }
    public void TakeDamage(Vector2 direction, float force = 5)
    {
        if (!IsTakingDamage)
        {
            AudioController.Instance.PlaySound("spikes damage", 0f);
            LevelController.Instance.TakeDamage();
            GameMenuController.Instance.ShowDamagePlayerImage();
            if (LevelController.Instance.Alive && LevelController.Instance.TimeLeft > 0)
            {
                _rb.velocity = direction * force;
                DamageAnimation();
            }
            else
                Die();
        }

    }
    void DamageAnimation()
    {
        IsTakingDamage = true;

        StartCoroutine(StartDamageAnimation());
    }
    void Die()
    {
        LevelController.Instance.GameOver();
        ParticleController.Instance.SpawnParticles("death 1", transform.position);
        if (tag == "Cirlce")
            LevelController.Instance.Player_1 = null;
        else if (tag == "Square")
            LevelController.Instance.Player_2 = null;
        Destroy(gameObject);
    }
    bool IsGrounded()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, transform.localScale.y / 2 + 0.1f);
        List<RaycastHit2D> hitsList = new List<RaycastHit2D>(hits);
        bool grounded = false;
        foreach (var hit in hitsList)
        {
            if (hit.collider.tag == gameObject.tag || hit.collider.tag == "Ignore") continue;

            if (_ha != null)
                if (hit.collider.tag == "Lava")
                    _ha.HeatUp();
            grounded = true;
        }
        return grounded;

    }
    public void ResetDoubleJump()
    {
        _jumpCount = JumpCount - 2;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crushinator")
        {
            Die();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crushinator")
        {
            Die();
        }
        foreach (var contact in collision.contacts)
        {

            //Check grounded
            if (contact.point.y < transform.position.y
                && Mathf.Abs(contact.point.x - transform.position.x) < transform.localScale.x / 3)
            {
               
                Grounded = true;
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float hitSpeed = 8;
        if (collision.relativeVelocity.magnitude >= hitSpeed)
        {
            float hitSound = (collision.relativeVelocity.magnitude - hitSpeed) / 16;
            if (hitSound > 1)
                hitSound = 1;
            if (tag == "Square")
                AudioController.Instance.PlaySound("metal impact", 0.1f, hitSound);
            else if (tag == "Circle")
                AudioController.Instance.PlaySound("wood impact", 0.1f, hitSound);
        }

    }
    IEnumerator StartDamageAnimation()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i % 2 == 0)
                _sr.color = DamageColor;
            else
                _sr.color = _spriteColor;
            yield return new WaitForSeconds(0.1f);
        }
        _sr.color = _spriteColor;
        IsTakingDamage = false;
    }
    public override void UpdateGame()
    {
        //Grounded = IsGrounded();
        UpdateActionKey();
        UpdateMovement();
        UpdateJump();
        UpdatePose();
        OriginVelocity = Vector2.zero;
        Grounded = IsGrounded();

        if(Grounded && _rb.velocity.magnitude > 0.2f)
        {
            if (ParticleTrail != null && !_particlesPlaying)
            {
                ParticleTrail.Play();
                _particlesPlaying = true;
            }

        }
        else
        {
            if (ParticleTrail != null)
            {
                ParticleTrail.Stop();
                _particlesPlaying = false;
            }
        }
    }
}
