using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver
{
	public void Invoke_PlayButton()
	{
		SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
	}

	public void Invoke_MenuButton()
	{
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}
}
