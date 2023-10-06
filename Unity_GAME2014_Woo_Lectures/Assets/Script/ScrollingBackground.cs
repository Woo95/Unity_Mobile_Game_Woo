using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
	[SerializeField] private Boundaries boundaries;

	[SerializeField] private float scrollSpeed = 3.0f;
	private Vector3 spawnPoint;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y - scrollSpeed * Time.deltaTime, transform.position.z);

		if (transform.position.y <= boundaries.min)
		{
			transform.position = new Vector3(transform.position.x, boundaries.max, transform.position.z);
		}
	}
}