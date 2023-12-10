using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameOverData
{
	static public int m_Score;
	static public float m_LeftTime;
	static public bool m_Won;

	static public void Init()
	{
		m_Score = 0;
		m_LeftTime = 0;
		m_Won = false;
	}

	static public void SetData(int obtainedCoin, float leftTime, bool isWon)
	{
		m_Score = obtainedCoin;
		m_LeftTime = leftTime;
		m_Won = isWon;
	}
}