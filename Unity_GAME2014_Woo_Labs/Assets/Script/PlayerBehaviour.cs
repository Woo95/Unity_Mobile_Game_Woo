using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
	[SerializeField] private Boundaries horizontalBoundaries;
	[SerializeField] private Boundaries verticalBoundaries;

	private Vector2 movement;
	[SerializeField] private float moveSpeed = 3f;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed * Time.deltaTime;

		transform.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, 0);

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
}