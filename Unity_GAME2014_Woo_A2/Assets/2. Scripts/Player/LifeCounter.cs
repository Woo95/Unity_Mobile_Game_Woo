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

		Destroy(m_GameObjectLife[m_LifeCount-1]);

		m_LifeCount--;
	}

	public void FallOffMapChecker()
	{
		if (transform.position.y <= -12.0f)
			LoseLife();
	}

	private void Update()
	{
		FallOffMapChecker();
	}
}
