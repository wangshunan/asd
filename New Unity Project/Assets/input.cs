using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class input : MonoBehaviour {

	float x;
	float y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		x = CrossPlatformInputManager.GetAxis ("Horizontal");
		y = CrossPlatformInputManager.GetAxis ("Vertical");

		transform.position = new Vector3 (transform.position.x + x * Time.deltaTime, transform.position.y + y * Time.deltaTime, transform.position.z);
	}
}
