using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public float Speed = 4;
    public float Accelaration = 1;
    private Rigidbody2D _rb;
    private PathFinder _pf;
    private List<Vector2> _waypoints = new List<Vector2>();
    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pf = GetComponent<PathFinder>();
        _pf.Subcribe(GetWaypoints);
    }

    // Update is called once per frame
    void Update()
    {
        //_waypoints = _pf.GetWaypoints();
        FollowPath();
    }
    void FollowPath()
    {
        if (_waypoints.Count > 0)
        {
            Vector2 direction = (_waypoints[0] - (Vector2)transform.position).normalized;
            
            if(_rb.velocity.magnitude < Speed)
            {
                _rb.velocity += direction * Accelaration;
            }
            //transform.position = Vector2.MoveTowards(transform.position, _waypoints[0], Time.deltaTime * Speed);
            if (Vector2.Distance(RoundVector(transform.position), _waypoints[0]) <= 0.5f)
                _waypoints.RemoveAt(0);
            else if(Vector2.Distance(transform.position, _waypoints[0]) <= 0.5f)
                _waypoints.RemoveAt(0);
        }
        _rb.velocity *= 0.9f;
    }

    Vector2 RoundVector(Vector2 vector)
    {
        return new Vector2(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }
    private void GetWaypoints(List<Vector2> waypoints)
    {
        _waypoints = waypoints;
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < _waypoints.Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(_waypoints[i], 0.2f);

            if (i > 0)
                Debug.DrawLine(_waypoints[i - 1], _waypoints[i]);
            else
                Debug.DrawLine(transform.position, _waypoints[0]);
        }

    }
}
