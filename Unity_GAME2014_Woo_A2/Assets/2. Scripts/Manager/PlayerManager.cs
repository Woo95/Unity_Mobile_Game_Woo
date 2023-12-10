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
	private int m_KilledEnemyAmount;

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

	#region Player Life Related
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

		GO_NORMAL();

		SoundManager.instance.PlaySFX("PlayerDeath");

		if (IsAlive())
			Respawn();
		else
			m_Controller.gameObject.SetActive(false);
	}
	public void Respawn()
	{
		m_Controller.m_FaceRight = true;
		m_Controller.Flip(m_Controller.m_FaceRight);

		m_Controller.SetPosition(m_SpawnPoint.position);
	}
	#endregion

	#region Point Related
	public void ObtainedCoin()
	{
		m_ObtainedCoinAmount++;
	}
	public void KilledEnemy()
	{
		m_KilledEnemyAmount++;
	}
	public int GetScore()
	{
		return m_ObtainedCoinAmount + m_KilledEnemyAmount;
	}
	public bool Finished()
	{
		return m_Controller.CheckGoal();
	}
	#endregion

	#region Event
	public void GO_BIG()
	{
		m_Controller.transform.position += Vector3.up * 2.5f;

		float scaleFactor = (m_Controller.transform.localScale.x < 0) ? -5.0f : 5.0f;
		m_Controller.transform.localScale = new Vector3(scaleFactor, 5.0f, 5.0f);
	}
	void GO_NORMAL()
	{
		m_Controller.transform.localScale = Vector3.one;
	}
	public void GO_SMALL()
	{
		float scaleFactor = (m_Controller.transform.localScale.x < 0) ? -0.5f : 0.5f;
		m_Controller.transform.localScale = new Vector3(scaleFactor, 0.5f, 0.5f);
	}
	#endregion
}
