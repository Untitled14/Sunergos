using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessage : MonoBehaviour {
	private TriggerConnection _trigger;
	private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
		_trigger = GetComponent<TriggerConnection> ();
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_trigger.Triggered) {
			//gameObject.SetActive (true);
			Color newColor = _spriteRenderer.color;
			newColor.a = 1;
			_spriteRenderer.color = newColor;
		} else {
			//gameObject.SetActive (false);
			Color newColor = _spriteRenderer.color;
			newColor.a = 0;
			_spriteRenderer.color = newColor;
		}
	}
}
