using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private Rigidbody2D _rb;
    public float Speed = 2;
    public float AccDistance = 1;

    [HideInInspector]
    public int OffPosition = 0;
    [HideInInspector]
    public int OnPosition = 0;

    [HideInInspector]
    public List<PlatformState> Points;

    [HideInInspector]
    public int Position = 0;

    private TriggerConnection _trigger;
    private int _currentPosition;
    private int _nextPosition;

    private Vector2 _destination;
    private Vector2 _current;

    private bool _canPlaySound = true;
    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _trigger = GetComponent<TriggerConnection>();
        if (Points.Count > 0)
            transform.position = Points[OffPosition].Position;
        _currentPosition = OffPosition;
        _nextPosition = OffPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_trigger != null && _trigger.enabled)
            UpdateWithTriggers();
        else
            UpdateWithoutTriggers();
    }
    private void LateUpdate()
    {
        if (Points.Count > 0)
            MoveTowards();
    }
    void UpdateWithoutTriggers()
    {
        if (_currentPosition == Position)
        {
            if (Position == OffPosition)
                Position = OnPosition;
            else
                Position = OffPosition;
        }
    }
    void UpdateWithTriggers()
    {
        if (_trigger.Triggered)
            Position = OnPosition;
        else
            Position = OffPosition;

        if(_currentPosition != Position && _canPlaySound)
        {
            AudioController.Instance.PlaySound("platform open", 0);
            _canPlaySound = false;
        }
        else
        {
            _canPlaySound = true;
        }
    }
    void MoveTowards()
    {
        if (_currentPosition == _nextPosition)
        {
            int dir = Position - _nextPosition;
            int sign = (int)Mathf.Sign(dir);
            if (dir == 0)
                sign = 0;
            _nextPosition += sign;
            _destination = Points[_nextPosition].Position;
        }
        Vector2 distance = _destination - (Vector2)transform.position;
        Vector2 direction = distance.normalized;
        float distanceToDestination = distance.magnitude;
        float distanceToCurrent = (Points[_currentPosition].Position - (Vector2)transform.position).magnitude;
        float distanceBetweenpoints = (_destination - Points[_currentPosition].Position).magnitude;
        if(distanceToDestination < _rb.velocity.magnitude * Time.deltaTime || distanceToDestination < 0.005f)
        {
            Stop();
        }else
        {
            float accDist = Points[_currentPosition].Acc;
            if (accDist > distanceBetweenpoints / 2)
                accDist = distanceBetweenpoints / 2;

            float velocitySpeed = Points[_currentPosition].Speed;
            if (distanceToCurrent < accDist)
                velocitySpeed = distanceToCurrent / accDist * Points[_currentPosition].Speed;
            else if (distanceToDestination < accDist)
                velocitySpeed = distanceToDestination / accDist * Points[_currentPosition].Speed;
            if (velocitySpeed < 0.1f)
                velocitySpeed = 0.1f;

            _rb.velocity = direction * velocitySpeed;
        }
        //if (distanceToDestination >= _rb.velocity.magnitude*Time.deltaTime)
        //{
           
        //}
        //else
        //{
        //    _rb.velocity = Vector2.zero;
        //    transform.position = _destination;
        //    _currentPosition = _nextPosition;
        //}
    }
    void Stop()
    {
        _rb.velocity = Vector2.zero;
        transform.position = _destination;
        _currentPosition = _nextPosition;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.OriginVelocity = _rb.velocity;
        }
    }
    private void OnDrawGizmos()
    {
        if (Points != null && Points.Count > 0)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, Points[Position].Position);
            for (int i = 0; i < Points.Count; i++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(Points[i].Position, 0.25f);
                if(i < Points.Count - 1)
                    Gizmos.DrawLine(Points[i].Position, Points[i+1].Position);
            }
            
        }

    }
    [System.Serializable]
    public class PlatformState
    {
        public Vector2 Position;
        public float Speed;
        public float Acc;
    }
}
