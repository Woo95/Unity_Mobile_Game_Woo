using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
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
