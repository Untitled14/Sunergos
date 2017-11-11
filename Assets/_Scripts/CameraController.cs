using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;
    public LevelController LevelController;
    
    [HideInInspector]
    public float Height;
    [HideInInspector]
    public float Width;

    public float MinDistance = 5;
    public float MaxDistance = 10;

    public Vector2 CameraPosition;

    private Camera _cam;
    private float _initialSize;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        _cam = GetComponent<Camera>();
        _initialSize = _cam.orthographicSize;
        MinDistance = _initialSize;

        UpdateVariables();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateVariables();
        if (LevelController != null && LevelController.Alive)
            UpdateCamera();	
	}
    void UpdateVariables()
    {
        Height = 2f * _cam.orthographicSize;
        Width = Height * _cam.aspect;

    }

    void UpdateCamera()
    {
        float distance = (LevelController.Player_2.transform.position - LevelController.Player_1.transform.position).magnitude;

        if (distance > MinDistance && distance < MaxDistance)
        {
            _cam.orthographicSize = _initialSize + (distance - MinDistance)/4;
        }
        
        Vector3 newCameraPosition = Vector2.Lerp(LevelController.Player_1.transform.position, LevelController.Player_2.transform.position, 0.5f);
        newCameraPosition.z = -10;
        //newCameraPosition += (Vector3)CameraPosition;
        transform.position = newCameraPosition;
    }
}
