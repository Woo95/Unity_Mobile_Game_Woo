using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIGamePlay : MonoBehaviour
{
	#region singleton
	public static UIGamePlay instance;
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

	public GameObject m_PauseUI;
	public Text m_PauseBody;

	public void Init()
	{
		if (m_PauseUI.activeInHierarchy)
			m_PauseUI.SetActive(false);
	}

	public void Invoke_Pause()
	{
		m_PauseBody.text = "Game Paused!";

		if (m_PauseUI.activeInHierarchy)
			m_PauseUI.SetActive(false);

		else if (!m_PauseUI.activeInHierarchy)
			m_PauseUI.SetActive(true);
	}

	public void Invoke_Menu()
	{
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}

	public void Invoke_Save()
	{
		m_PauseBody.text = "Game Saved!";
	}
}