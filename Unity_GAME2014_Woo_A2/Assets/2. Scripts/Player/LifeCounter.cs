using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
	public GameObject[] m_GameObjectLife = new GameObject[3];
	private int m_LifeCount = 3;

	public void LoseLife()
	{
		if (m_LifeCount <= 0)
			return;

		Destroy(m_GameObjectLife[m_LifeCount]);

		m_LifeCount--;
	}
}
