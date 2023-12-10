using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
	public Text m_ScoreText;
	public Text m_LeftTimeText;
	public Text m_WonText;

	private void Start()
	{
		DisplayGameOverData();
	}

	public void Invoke_PlayButton()
	{
		SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
	}

	public void Invoke_MenuButton()
	{
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}

	private void DisplayGameOverData()
	{
		m_ScoreText.text = GameOverData.m_Score.ToString();
		m_LeftTimeText.text = FormatTime(GameOverData.m_LeftTime);
		m_WonText.text = GameOverData.m_Won ? "Won" : "Lost";
	}

	private string FormatTime(float timeInSeconds)
	{
		int minutes = (int)(timeInSeconds / 60.0f);
		int seconds = (int)(timeInSeconds % 60.0f);
		return string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}