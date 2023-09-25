using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	[SerializeField] private Boundaries horizontalBoundaries;
	[SerializeField] private Boundaries verticalBoundaries;

	Camera cameras;
	Vector2 destination;
	[SerializeField] bool isMobilePlatform;

	private Vector2 movement;
	[SerializeField] private float moveSpeed = 3f;
	// Start is called before the first frame update
	void Start()
	{
		cameras = Camera.main;
		CheckPlatform();
	}

	// Update is called once per frame
	void Update()
	{
		if (isMobilePlatform)
		{
			TouchScreenInput();
		}
		else
		{
			PCInput();
		}

		CheckBoundaries();
	}

	void TouchScreenInput()
	{
		foreach (Touch touch in Input.touches)
		{
			destination = cameras.ScreenToWorldPoint(touch.position);
			transform.position = Vector2.Lerp(transform.position, destination, moveSpeed * Time.deltaTime);
		}
	}

	void PCInput()
	{
		movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed * Time.deltaTime;
		transform.position = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
	}

	void CheckBoundaries()
	{
		#region Boundaries
		if (transform.position.x > horizontalBoundaries.max)
		{
			transform.position = new Vector3(horizontalBoundaries.min, transform.position.y, transform.position.z);
		}
		else if (transform.position.x < horizontalBoundaries.min)
		{
			transform.position = new Vector3(horizontalBoundaries.max, transform.position.y, transform.position.z);
		}
		else if (transform.position.y > verticalBoundaries.max)
		{
			transform.position = new Vector3(transform.position.x, verticalBoundaries.max, transform.position.z);
		}
		else if (transform.position.y < verticalBoundaries.min)
		{
			transform.position = new Vector3(transform.position.x, verticalBoundaries.min, transform.position.z);
		}
		#endregion
	}

	void CheckPlatform()
	{
		if (Application.platform == RuntimePlatform.Android ||
			Application.platform == RuntimePlatform.IPhonePlayer)
		{
			isMobilePlatform = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("Got hit");
		}
	}
}