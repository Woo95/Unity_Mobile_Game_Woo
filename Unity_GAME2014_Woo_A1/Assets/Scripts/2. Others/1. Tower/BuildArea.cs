using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    public bool m_Collision;

	private void OnTriggerEnter2D(Collider2D other)
	{
		m_Collision = true;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		m_Collision = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		m_Collision = false;
	}
}
