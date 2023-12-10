using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
	#region singleton
	public static PlayerManager instance;
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		else
			instance = this;
	}
	#endregion

	public PlayerController m_Controller;
	public UILifeCounter m_UILifeCounter;

	private int m_ObtainedCoinAmount;

	public int m_LifeCount;
	float m_DeadHeight;

	public Transform m_SpawnPoint;

	public void Init()
	{
		m_Controller.SetPosition(m_SpawnPoint.position);

		m_ObtainedCoinAmount = 0;

		m_LifeCount = 3;
		m_DeadHeight = -12.0f;

		m_Controller.Init();
		m_UILifeCounter.Init();
	}

	public void InputHandler()
	{
		m_Controller.Move();

		FallOffMapChecker();
	}

	public bool IsAlive()
	{
		return m_LifeCount > 0;
	}
	public void FallOffMapChecker()
	{
		if (m_Controller.GetPosition().y <= m_DeadHeight)
		{
			LoseLife();
		}	
	}
	public void LoseLife()
	{
		m_LifeCount--;

		m_UILifeCounter.UpdateLife();

		Respawn();
	}
	public void Respawn()
	{
		m_Controller.SetPosition(m_SpawnPoint.position);
	}

	public void ObtainedCoin()
	{
		m_ObtainedCoinAmount++;
	}
	public int GetObtainedCoin()
	{
		return m_ObtainedCoinAmount;
	}

	public bool Finished()
	{
		return m_Controller.CheckGoal();
	}
}
