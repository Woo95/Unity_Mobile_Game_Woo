using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[Header("Behaviour Set-Up")]
	public bool m_UpDown;
	public bool m_LeftRight;
	[Header("Others")]
	public float m_MoveSpeed      = 1.0f;
    public float m_MoveDistance   = 5.0f;

    private Vector3 m_StartPos, m_EndPos;
	bool m_ChangeDirection;
	void Start()
	{
		m_StartPos = transform.position;
		m_EndPos = transform.position;
		if (m_LeftRight && !m_UpDown)
		{
			m_StartPos.x -= m_MoveDistance;
			m_EndPos.x += m_MoveDistance;
		}
		else if (m_UpDown && !m_LeftRight)
		{
			m_StartPos.y -= m_MoveDistance;
			m_EndPos.y += m_MoveDistance;
		}

		m_ChangeDirection = true;

		transform.position = Vector3.Lerp(m_StartPos, m_EndPos, Random.Range(0.0f, 1.0f));
	}

	void Update()
	{
		if (m_ChangeDirection)
		{
			transform.position = Vector3.MoveTowards(transform.position, m_StartPos, m_MoveSpeed * Time.deltaTime);
			if (Vector3.Distance(transform.position, m_StartPos) <= 0.01f)
				m_ChangeDirection = !m_ChangeDirection;
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, m_EndPos, m_MoveSpeed * Time.deltaTime);
			if (Vector3.Distance(transform.position, m_EndPos) <= 0.01f)
				m_ChangeDirection = !m_ChangeDirection;
		}	
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController player = other.GetComponent<PlayerController>();
			if (player != null)
			{
				player.OnPlatform(true, transform);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController player = other.GetComponent<PlayerController>();
			if (player != null)
			{
				player.OnPlatform(false, transform);
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
		{
			if (m_LeftRight && !m_UpDown)
			{
				Vector3 startPos = new Vector3(transform.position.x - m_MoveDistance, transform.position.y, transform.position.z);
				Vector3 endPos = new Vector3(transform.position.x + m_MoveDistance, transform.position.y, transform.position.z);

				Gizmos.DrawLine(startPos, endPos);
			}
			else if (m_UpDown && !m_LeftRight)
			{
				Vector3 startPos = new Vector3(transform.position.x, transform.position.y - m_MoveDistance, transform.position.z);
				Vector3 endPos = new Vector3(transform.position.x, transform.position.y + m_MoveDistance, transform.position.z);

				Gizmos.DrawLine(startPos, endPos);
			}
		}
		else
		{
			Gizmos.DrawLine(m_StartPos, m_EndPos);
		}
	}
}
