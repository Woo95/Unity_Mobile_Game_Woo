using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	[SerializeField] private Boundaries vertical, horizontal;
	[SerializeField] Vector2 speedRange;

	[SerializeField] float verticalSpeed;
	[SerializeField] float horizontalSpeed;

	// Start is called before the first frame update
	void Start()
	{
		ResetPos();
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector2(Mathf.PingPong(horizontalSpeed * Time.time, horizontal.max - horizontal.min) + horizontal.min, transform.position.y - (verticalSpeed * Time.deltaTime));

		ResetPos();
	}

	void ResetPos()
	{
		if (transform.position.y < vertical.min)
		{
			verticalSpeed = Random.Range(speedRange.x, speedRange.y);
			horizontalSpeed = Random.Range(speedRange.x, speedRange.y);
			transform.position = new Vector2(Random.Range(horizontal.min, horizontal.max), vertical.max);
		}
	}
}