using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class StickController : MonoBehaviour {

	public Vector3 m_StartPos;
	string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
	string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

	public int MovementRange = 100;

	CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
	CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

	//アクティブ時初期化
	void OnEnable()
	{
		CreateVirtualAxes();
		transform.position = GameObject.Find("Stick").transform.position;
		m_StartPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick() {
		transform.position = m_StartPos;
		UpdateVirtualAxes(m_StartPos);
	}

	void CreateVirtualAxes()
	{
		m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
		CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);

		m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
		CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
	}

	void UpdateVirtualAxes(Vector3 value) {
		var delta = m_StartPos - value;
		delta.y = -delta.y;
		delta /= MovementRange;

		m_HorizontalVirtualAxis.Update(-delta.x);
		m_VerticalVirtualAxis.Update(delta.y);
	}

	public void StickMoveController()
	{
		var delta = Vector2.Distance ((Vector2)m_StartPos, transform.position);
		Vector3 vector = (Vector3)transform.position - m_StartPos;
		//Debug.Log (delta);
		if (delta < MovementRange) {
			transform.position = transform.position;
		} else if (delta >= MovementRange) {
			transform.position = m_StartPos + vector.normalized * MovementRange;
		}

		UpdateVirtualAxes(transform.position);
	}
		
}
