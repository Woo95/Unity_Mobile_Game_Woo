using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	Camera camera;
	Transform cameraTrans;

	Vector3 touchStart;
	public float zoomOutMin = 3.0f;
	public float zoomOutMax = 10.0f;

	public Transform topLeft, bottomRight;
	Vector3 p00, p01, p11, p10;


	public void Init()
	{
		camera = Camera.main;
		cameraTrans = camera.transform;

		p01 = topLeft.position;
		p10 = bottomRight.position;
	}
	public void Play()
	{
		if (Input.GetMouseButtonDown(0))
		{
			touchStart = camera.ScreenToWorldPoint(Input.mousePosition);
		}

		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

			float difference = currentMagnitude - prevMagnitude;

			Zoom(difference * 0.01f);
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 direction = touchStart - camera.ScreenToWorldPoint(Input.mousePosition);
			cameraTrans.position += direction;
		}
		Zoom(Input.GetAxis("Mouse ScrollWheel"));

		Vector3 pos = cameraTrans.position;
		if (pos.x < p00.x) pos.x = p00.x;
		if (pos.x > p11.x) pos.x = p11.x;
		if (pos.y < p00.y) pos.y = p00.y;
		if (pos.y > p11.y) pos.y = p11.y;
		cameraTrans.position = pos;
	}

	void Zoom(float zoomVal)
	{
		camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - zoomVal, zoomOutMin, zoomOutMax);
		ZoomResize();
	}

	void ZoomResize()
	{
		float dy = camera.orthographicSize;
		float dx = camera.orthographicSize * camera.aspect;
		p00 = new Vector3(p01.x + dx, p10.y + dy);
		p11 = new Vector3(p10.x - dx, p01.y - dy);
	}
}
