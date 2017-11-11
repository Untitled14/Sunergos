using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{

    Platform _ts;

    private void OnEnable()
    {
        _ts = (Platform)target;
    }

    public override void OnInspectorGUI()
    {
        if (_ts.Points != null)
        {
            if (_ts.Points.Count <= _ts.Position)
                _ts.Position = _ts.Points.Count - 1;
            if (_ts.Position < 0)
                _ts.Position = 0;
        }

        DrawDefaultInspector();

        DrawOnOffPositions();

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Position");
            GUI.enabled = false;
            EditorGUILayout.IntField(_ts.Position);
            GUI.enabled = true;
        }
        GUILayout.EndHorizontal();

        PrintPointsList();
    }
    void DrawOnOffPositions()
    {
        List<string> points = new List<string>();
        for (int i = 0; i < _ts.Points.Count; i++)
            points.Add("Point " + i);

        GUILayout.BeginHorizontal();
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.Label("Off position");
            int offPosittion = EditorGUILayout.Popup(_ts.OffPosition, points.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_ts, "Changed off position");
                _ts.OffPosition = offPosittion;
                _ts.transform.position = _ts.Points[offPosittion].Position;
                EditorUtility.SetDirty(_ts);
            }

        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("On position");
            EditorGUI.BeginChangeCheck();
            int onPosittion = EditorGUILayout.Popup(_ts.OnPosition, points.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_ts, "Changed On position");
                _ts.OnPosition = onPosittion;
                _ts.transform.position = _ts.Points[onPosittion].Position;
                EditorUtility.SetDirty(_ts);
            }

        }
        GUILayout.EndHorizontal();
    }
    void PrintPointsList()
    {
        GUILayout.Label("Points", EditorStyles.boldLabel);
        for (int i = 0; i < _ts.Points.Count; i++)
        {
            PrintPoint(i);
        }
        if (GUILayout.Button("Add point"))
        {
            Undo.RecordObject(target, "Added Position");
            Platform.PlatformState newState = new Platform.PlatformState();
            newState.Position = _ts.transform.position;
            newState.Speed = _ts.Speed;
            newState.Acc = _ts.AccDistance;
            _ts.Points.Add(newState);
            EditorUtility.SetDirty(_ts);
        }

    }
    void PrintPoint(int i)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Point " + i);

            EditorGUI.BeginChangeCheck();
            Vector2 point = EditorGUILayout.Vector2Field("", _ts.Points[i].Position, GUILayout.Width(100));
            GUILayout.Label("S");
            float speed = EditorGUILayout.FloatField( _ts.Points[i].Speed);
            GUILayout.Label("A");
            float acc = EditorGUILayout.FloatField(_ts.Points[i].Acc);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_ts, "Changed points");
                _ts.Points[i].Position = point;
                _ts.Points[i].Speed = speed;
                _ts.Points[i].Acc = acc;
                EditorUtility.SetDirty(_ts);
            }


            if (GUILayout.Button("Set"))
            {
                Undo.RecordObject(_ts, "Set position");
                _ts.Position = i;
                _ts.Points[i].Position = _ts.transform.position;
                EditorUtility.SetDirty(_ts);
            }

            if (GUILayout.Button("-"))
            {
                Undo.RecordObject(_ts, "Removed point");
                _ts.Points.RemoveAt(i);
                if (_ts.OnPosition >= _ts.Points.Count)
                    _ts.OnPosition = _ts.Points.Count - 1;
                if (_ts.OffPosition >= _ts.Points.Count)
                    _ts.OffPosition = _ts.Points.Count - 1;
                EditorUtility.SetDirty(_ts);
            }
        }
        GUILayout.EndHorizontal();
    }

    private void OnSceneGUI()
    {
        if (_ts.Points != null && _ts.Points.Count > 0)
        {
            for (int i = 0; i < _ts.Points.Count; i++)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 position = Handles.PositionHandle(_ts.Points[i].Position, Quaternion.identity);
                Handles.Label(position + Vector3.up, "Point " + i);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Changed Position");
                    _ts.Points[i].Position = position;
                }
            }
        }
    }
}
