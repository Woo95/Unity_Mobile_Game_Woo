using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	#region singleton
	public static Timer instance;
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

	public Text m_TimerText;
	private float m_Timer = 300.0f; // 5 minutes in seconds

	public void UpdateTimer()
	{
		if (m_Timer > 0)
		{
			m_Timer -= Time.deltaTime;
			UpdateTimerDisplay();
		}
		else	// timer reaches 0
		{
			m_Timer = 0.0f;
			UpdateTimerDisplay();
		}
	}

	void UpdateTimerDisplay()
	{
		int minutes = (int)(m_Timer / 60.0f);
		int seconds = (int)(m_Timer % 60.0f);

		m_TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}