using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public bool DrawGizmos = false;
    public GameObject Target;
    public float MaxSpeed = 4;
    public float AccSpeed = 0.5f;
    public float Radius = 5;
    public List<string> UnwalkableTags;
    public List<Node> Path = new List<Node>();

    private Rigidbody2D _rb;
    private Dictionary<Vector2, Node> _nodes = new Dictionary<Vector2, Node>();
    private Node _targetNode;

    private float _searchInterval = 1;
    private float _searchPathInterpolation = 50;
    private List<Vector2> _waypoints = new List<Vector2>();
    private Action<List<Vector2>> _waypointSubsctiption;
    private Vector2 _center;
    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        InitializeNodes();
    }
    private void Update()
    {
        _targetNode = GetTargetNode();
        if (RoundVector(transform.position) != _center || _searchInterval <= 0)
        {
            InitializeNodes();
            UpdatePath();

            _searchInterval = 1;
        }
        if (Path.Count == 0 || _targetNode != null && Path.Count > 0 && Path[Path.Count - 1].Position != RoundVector(_targetNode.Position))
        {
            InitializeNodes();
            UpdatePath();
        }
        _searchInterval -= Time.deltaTime;

    }
    void UpdatePath()
    {
        if (_targetNode != null && _targetNode.Walkable)
        {
            FindPath(transform.position, Target.transform.position);
            _waypoints = SimplifyPath(Path);
            if (_waypointSubsctiption != null && Path.Count > 0 && Path[Path.Count - 1].Position == RoundVector(Target.transform.position))
                _waypointSubsctiption(_waypoints);
        }
    }
    public void Subcribe(Action<List<Vector2>> callback)
    {
        _waypointSubsctiption = callback;
    }
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = (int)Mathf.Abs(nodeA.Position.x - nodeB.Position.x);
        int distY = (int)Mathf.Abs(nodeA.Position.y - nodeB.Position.y);
        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
        //return distX + distY;
    }
    void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        if(!_nodes.ContainsKey(RoundVector(startPos)) || !_nodes.ContainsKey(RoundVector(targetPos)))
        {
            return;
        }
        Node startNode = _nodes[RoundVector(startPos)];
        Node targetNode = _nodes[RoundVector(targetPos)];

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            //Find node with lowest value
            Node currentNode = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            //If target node
            if (currentNode == targetNode)
            {
                
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (var neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }

        }
    }
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Add(startNode);
        path.Reverse();

        Path = path;
    }
    List<Vector2> SimplifyPath(List<Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;
        if (path.Count >= 2)
            directionOld = new Vector2(path[0].Position.x - path[1].Position.x, path[0].Position.y - path[1].Position.y);
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].Position.x - path[i].Position.x, path[i - 1].Position.y - path[i].Position.y);
            if (directionNew != directionOld)
                waypoints.Add(path[i-1].Position);
            
            directionOld = directionNew;
        }
        if(Path.Count > 0 && path[Path.Count - 1].Position == RoundVector(Target.transform.position))
            waypoints.Add(Target.transform.position);
        return waypoints;
    }
    public List<Vector2> GetWaypoints()
    {
        return _waypoints;
    }
    Node GetTargetNode()
    {
        if (Target == null)
            return null;
        if (_nodes.ContainsKey(RoundVector(Target.transform.position)))
        {
            Node targetNode = _nodes[RoundVector(Target.transform.position)];
            return targetNode;
        }
        return null;
    }
    Node GetNode(Vector2 position)
    {
        if (_nodes.ContainsKey(RoundVector(position)))
        {
            Node node = _nodes[RoundVector(position)];
            return node;
        }
        return null;
    }
    List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int i = -1; i <= 1; i++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (i == 0 && y == 0)
                    continue;
                Node neighbouringNode = GetNode(node.Position + new Vector2(i, y));
                if (neighbouringNode != null)
                    neighbours.Add(neighbouringNode);
            }
        }
        return neighbours;
    }
    void InitializeNodes()
    {
        _nodes = new Dictionary<Vector2, Node>();
        float radius = Radius;
        float targetDirectionX = Mathf.Sign(Target.transform.position.x - transform.position.x);
        _center = RoundVector(transform.position);
        _center.x += targetDirectionX * radius;
        for (int y = 0; y <= radius * 2; y++)
        {
            for (int x = 0; x <= radius * 2; x++)
            {
                Vector2 position = RoundVector(new Vector2(
                    _center.x + x - radius,
                    _center.y + y - radius
                    ));
                Node node = new Node(position);
                var hits = Physics2D.OverlapCircleAll(position, 0.5f);
                foreach (var item in hits)
                {
                    if (item.transform.gameObject == gameObject) continue;
                    foreach (var tag in UnwalkableTags)
                    {
                        if (item.transform.tag == tag)
                            node.Walkable = false;
                    }
                   
                }
                if (!_nodes.ContainsKey(position))
                    _nodes.Add(position, node);
            }
            //yield return null;
        }
    }
    Vector2 RoundVector(Vector2 position)
    {
        int xPosition = Mathf.RoundToInt(position.x);
        int yPosition = Mathf.RoundToInt(position.y);
        return new Vector2(xPosition, yPosition);
    }
    private void OnDrawGizmos()
    {
        if (!DrawGizmos)
            return;
        foreach (var item in _nodes)
        {

            Node node = item.Value;

            if (Path != null)
            {
                if (Path.Contains(node))
                {
                    Gizmos.color = new Color(0, 1, 0, 0.25f);
                    Gizmos.DrawCube(node.Position, new Vector3(1, 1, 0.2f));
                }
            }

            if (node.Walkable)
                Gizmos.color = Color.black;
            else
                Gizmos.color = Color.red;

            Gizmos.DrawWireCube(node.Position, new Vector3(1, 1, 0.2f));
            if (_targetNode != null && _targetNode.Position == node.Position)
            {
                Gizmos.color = new Color(0, 0, 1, 0.25f);
                Gizmos.DrawCube(node.Position, new Vector3(1, 1, 0.2f));
            }

            //UnityEditor.Handles.color = Color.black;
            //UnityEditor.Handles.Label(node.Position + new Vector2(-0.25f, 0.3f), node.gCost.ToString());
            //UnityEditor.Handles.Label(node.Position + new Vector2(0.25f, 0.3f), node.hCost.ToString());
            //UnityEditor.Handles.Label(node.Position + new Vector2(0, 0f), node.fCost.ToString());
        }
    }



    public class Node
    {
        public bool Walkable;
        public int gCost;
        public int hCost;
        public int fCost { get { return gCost + hCost; } }
        public Vector2 Position;
        public Node Parent;

        public Node(Vector2 position, bool walkable = true)
        {
            Walkable = walkable;
            Position = position;
        }


    }
}
