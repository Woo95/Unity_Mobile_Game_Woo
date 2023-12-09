using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	float m_Speed = 3.0f;
	float m_Distance = 0.2f;

	private Vector3 m_StartPos;

	private void Start()
	{
		m_StartPos = transform.position;
	}

	private void Update()
	{
		float verticalMovement = Mathf.Sin(Time.time * m_Speed) * m_Distance;

		transform.position = m_StartPos + new Vector3(0f, verticalMovement, 0f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			PlayerManager.instance.ObtainCoin();
			Destroy(gameObject);
		}
	}
}
