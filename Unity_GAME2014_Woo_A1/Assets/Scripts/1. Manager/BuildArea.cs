using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    public bool m_Collision;

	private void OnTriggerEnter(Collider other)
	{
		m_Collision = true;
	}

	private void OnTriggerStay(Collider other)
	{
		m_Collision = true;
	}

	private void OnTriggerExit(Collider other)
	{
		m_Collision = false;
	}
}
