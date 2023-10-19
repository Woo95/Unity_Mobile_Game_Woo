using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTower : Tower
{
	public int m_BuyResource;

	public void TakeDamage(float damaged)
	{
		if (damaged > 0)
		{
			m_Health -= damaged;
			if (m_Health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
