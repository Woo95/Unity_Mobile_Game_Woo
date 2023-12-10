using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	float m_MoveSpeed = 3.0f;
	float m_Distance = 0.2f;

	private Vector3 m_StartPos;

	private void Start()
	{
		m_StartPos = transform.position;
	}

	private void Update()
	{
		float newY = Mathf.Sin(Time.time * m_MoveSpeed) * m_Distance;
		transform.position = m_StartPos + new Vector3(0, newY, 0);
	}
}
