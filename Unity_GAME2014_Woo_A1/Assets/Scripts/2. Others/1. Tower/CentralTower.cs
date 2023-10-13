using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralTower : Tower
{
	#region singletone;
	public static CentralTower instance;
	public void Awake()
	{
		instance = this;
	}
	#endregion

	public float m_playTime;
	public int m_Score;
	public int m_Gold;
	public int m_Resource;
}
