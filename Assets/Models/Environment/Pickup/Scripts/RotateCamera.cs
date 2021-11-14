using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

	[SerializeField] float MoveSpeed = 0.2f;
	[SerializeField] float RotateSpeed = 0.2f;

	Vector2 MousePos;
	void Update () {

		if (Input.GetKey(KeyCode.W)) {
			var target = Vector3.MoveTowards(transform.position, transform.TransformPoint(Vector3.forward),  MoveSpeed * Time.deltaTime);
			target.y = transform.position.y;
			transform.position = target;
		} else if (Input.GetKey(KeyCode.S)) {
			var target = Vector3.MoveTowards(transform.position, transform.TransformPoint(Vector3.back),  MoveSpeed * Time.deltaTime);
			target.y = transform.position.y;
			transform.position = target;
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.position = Vector3.MoveTowards(transform.position, transform.TransformPoint(Vector3.left),  MoveSpeed * Time.deltaTime);
		} else if (Input.GetKey(KeyCode.D)) {
			transform.position = Vector3.MoveTowards(transform.position, transform.TransformPoint(Vector3.right),  MoveSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.Mouse0)) {
			UpdateMouse();
		} else {
			MousePos = Vector2.zero;
		}

		if (Input.touchCount > 0) {
			UpdateTouch();
		}
	}

	void UpdateMouse () {
		if (MousePos == Vector2.zero) {
			MousePos = Input.mousePosition;
		}
		Vector2 delta = (Vector2)Input.mousePosition - MousePos;
		MousePos = (Vector2)Input.mousePosition;
		Rotate(delta);
	}

	void UpdateTouch () {
		Vector2 delta = (Vector2)Input.touches[0].deltaPosition;
		Rotate(delta);
	}

	void Rotate (Vector2 delta) {
		var angles = transform.eulerAngles;
		angles.y += delta.x * RotateSpeed;
		angles.x += delta.y * -RotateSpeed;
		angles.x = Mathf.Clamp(angles.x, 0, 80);
		angles.z = 0;
		transform.eulerAngles = angles;
		//transform.rotation *= Quaternion.AngleAxis(delta.x * Speed, Vector3.up);
		//transform.rotation *= Quaternion.AngleAxis(delta.y * Speed, Vector3.left);
	}
}
