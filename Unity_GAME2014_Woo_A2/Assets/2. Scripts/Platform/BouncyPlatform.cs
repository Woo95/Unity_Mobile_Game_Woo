using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
	public float m_Power = 100.0f;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Rigidbody2D>().AddForce(Vector3.up * m_Power);
		}
	}
}
