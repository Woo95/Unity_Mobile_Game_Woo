using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlayScene : MonoBehaviour
{
	public GameObject m_GamePlayUI;
	public GameObject m_GameOverUI;

	// Start is called before the first frame update
	void Start()
    {
		if (!m_GamePlayUI.activeInHierarchy)	// make sure gamePlay UI is always on
			m_GamePlayUI.SetActive(true);

		if (m_GameOverUI.activeInHierarchy)		// make sure gameOver UI is off at the start
			m_GameOverUI.SetActive(false);
	}

	public void GameOver()
	{
		m_GameOverUI.SetActive(true);
	}

	/////////////////////////////// For buttons ///////////////////////////////
	public void Invoke_Replay()
	{
		SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
	}

	public void Invoke_MainMenu_Return()
	{
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}
}
