using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIPause : MonoBehaviour
{
	#region singleton
	public static UIPause instance;
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

	public void Invoke_Pause(bool isActive)
	{
		if (isActive)
		{
			m_PauseBody.text = "Game Paused!";
			m_PauseUI.SetActive(isActive);
		}
		else
			m_PauseUI.SetActive(isActive);
	}

	public void Invoke_Menu()
	{
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}
}