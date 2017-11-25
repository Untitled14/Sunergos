using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGameObject : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (!LevelController.Instance.Paused)
            UpdateGame();
	}

    public abstract void UpdateGame();
}
