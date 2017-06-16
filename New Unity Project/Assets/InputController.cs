using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour {

	[SerializeField]
	StickController inPutUpdata;

	public GameObject stick;
	Vector3 mouseInputpos;
	bool onClicked;

	// Use this for initialization
	void Awake () {
		stick = GameObject.Find ("Stick");
		inPutUpdata = GameObject.Find ("MobileJoystick").GetComponent<StickController> ();
		mouseInputpos = Vector3.zero;
		onClicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			mouseInputpos = Input.mousePosition;
			stick.transform.position = mouseInputpos;
			stick.SetActive (true);
			inPutUpdata.m_StartPos = mouseInputpos;
			inPutUpdata.OnClick ();
			onClicked = true;
			//Debug.Log (mouseInputpos);
		}

		if (onClicked) {
			inPutUpdata.StickMoveController ();
		}

		if (Input.GetMouseButtonUp (0)) {
			onClicked = false;
		}

	}
}