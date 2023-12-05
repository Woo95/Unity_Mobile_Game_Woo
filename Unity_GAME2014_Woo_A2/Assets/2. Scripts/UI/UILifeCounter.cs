using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILifeCounter : MonoBehaviour
{
	public GameObject[] m_GameObjectLife = new GameObject[3];

	public void Init()
	{
		int lifeCount = PlayerManager.instance.m_LifeCount;
		for (int i=0; i < lifeCount; i++)
		{
			m_GameObjectLife[i].SetActive(true);
		}
	}

	public void UpdateLife()
	{
		int lifeCount = PlayerManager.instance.m_LifeCount;
		m_GameObjectLife[lifeCount].SetActive(false);
	}
}
