using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputController : MonoBehaviour {
	

	public GameObject stickController;
	Vector3 mouseInputpos;

	// Use this for initialization
	void Awake () {
		stickController = GameObject.Find ("StickController");
		mouseInputpos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			mouseInputpos = Input.mousePosition;
			stickController.transform.position = mouseInputpos;
			stickController.SetActive (true);
			GameObject.Find ("MobileJoystick").GetComponent<Joystick> ();
			Debug.Log (mouseInputpos);
		}

		if (Input.GetMouseButtonUp (1)) {
			stickController.SetActive (false);
		}
	}
}