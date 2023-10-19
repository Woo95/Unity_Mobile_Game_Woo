using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CentralTower : Tower
{
	#region singletone;
	public static CentralTower instance;
	public void Awake()
	{
		instance = this;
	}
	#endregion

	public int m_Wave;
	public float m_playTime;
	public int m_Score;
	public int m_Gold;
	public int m_Resource;

	public Text m_WaveText;
	public Text m_ScoreText;
	public Text m_HealthText;
	public Text m_GoldText;
	public Text m_ResourceText;

	public void Init()
	{
		m_WaveText.text = "Wave " + m_Wave.ToString();
		m_ScoreText.text = "Score: " + m_Score.ToString();
		m_HealthText.text = m_Health.ToString();
		m_GoldText.text = m_Gold.ToString();
		m_ResourceText.text = m_Resource.ToString();
	}

	private void UpdateWaveText()
	{
		if (m_WaveText != null)
		{
			m_WaveText.text = "Wave " + m_Wave.ToString();
		}
	}

	private void UpdateScoreText()
	{
		if (m_ScoreText != null)
		{
			m_ScoreText.text = "Score: " + m_Score.ToString();
		}
	}
	private void UpdateGoldText()
	{
		if (m_GoldText != null)
		{
			m_GoldText.text = m_Gold.ToString();
		}
	}

	private void UpdateResourceText()
	{
		if (m_ResourceText != null)
		{
			m_ResourceText.text = m_Resource.ToString();
		}
	}

	private void UpdateHealthText()
	{
		if (m_HealthText != null)
		{
			m_HealthText.text = m_Health.ToString();
		}
	}


	public void AddWave(int wave)
	{
		m_Wave = wave;
		UpdateWaveText();
	}

	public void AddScore()
	{
		m_Score += 1;
		UpdateScoreText();
	}

	public void AddGold(int gold)
	{
		m_Gold += gold;
		UpdateGoldText();
	}

	public void AddResource(int resource)
	{
		m_Resource += resource;
		UpdateResourceText();
	}

	public void TakeDamage(float damaged)
	{
		if (damaged > 0)
		{
			m_Health -= damaged;
			if (m_Health <= 0)
			{
				m_Health = 0;
				UpdateHealthText();
				Destroy(gameObject);
			}
		}
	}
}
