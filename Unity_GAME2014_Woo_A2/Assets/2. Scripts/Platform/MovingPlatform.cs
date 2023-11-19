using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[Header("Behaviour Set-Up")]
	public bool UpDown;
	public bool LeftRight;
	[Header("Others")]
	public float moveSpeed      = 1.0f;
    public float moveDistance   = 5.0f;

    private Vector3 initialPos;

	void Start()
	{
		initialPos = transform.position;
	}

	void Update()
	{
		float newPosX = transform.position.x;
		float newPosY = transform.position.y;

		if (LeftRight && !UpDown)
		{
			// Move left and right
			newPosX = initialPos.x + Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
		}
		else if (UpDown && !LeftRight)
		{
			// Move up and down
			newPosY = initialPos.y + Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
		}

		transform.position = new Vector3(newPosX, newPosY, transform.position.z);
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
		{
			if (LeftRight && !UpDown)
			{
				Vector3 startPos = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
				Vector3 endPos = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);

				Gizmos.DrawLine(startPos, endPos);
			}
			else if (UpDown && !LeftRight)
			{
				Vector3 startPos = new Vector3(transform.position.x, transform.position.y - moveDistance, transform.position.z);
				Vector3 endPos = new Vector3(transform.position.x, transform.position.y + moveDistance, transform.position.z);

				Gizmos.DrawLine(startPos, endPos);
			}
		}
		else
		{
			if (LeftRight && !UpDown)
			{
				Vector3 startPos = new Vector3(initialPos.x - moveDistance, initialPos.y, initialPos.z);
				Vector3 endPos = new Vector3(initialPos.x + moveDistance, initialPos.y, initialPos.z);

				Gizmos.DrawLine(startPos, endPos);
			}
			else if (UpDown && !LeftRight)
			{
				Vector3 startPos = new Vector3(initialPos.x, initialPos.y - moveDistance, initialPos.z);
				Vector3 endPos = new Vector3(initialPos.x, initialPos.y + moveDistance, initialPos.z);

				Gizmos.DrawLine(startPos, endPos);
			}
		}
	}
}
