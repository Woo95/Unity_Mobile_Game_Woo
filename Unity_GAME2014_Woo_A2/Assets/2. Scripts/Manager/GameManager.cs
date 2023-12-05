using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eGameState
{
	None,
	Play,
	GameOver,
	Pause,
}

public class GameManager : MonoBehaviour
{
	public eGameState gameState = eGameState.None;

	void Start()
	{
		InPlay();
	}

	#region FSM Play
	void InPlay()
	{
		Debug.Log("InPlay");
		gameState = eGameState.Play;

		Time.timeScale = 1.0f;

		Player.instance.Init();
		UIGamePlay.instance.Init();
		UIPause.instance.Init();
		SoundManager.instance.Init();
	}
	void ModifyPlay()
	{
		Debug.Log("ModifyPlay");

		if (!Timer.instance.UpdateTimer())
		{
			InGameOver();
			return;
		}

		//Player.instance.Move();
		Player.instance.MoveWithKeyboard();
	}
	#endregion

	#region FSM Pause
	public void InPause()
	{
		Debug.Log("InPause");
		gameState = eGameState.Pause;

		Time.timeScale = 0f;

		UIPause.instance.Invoke_Pause();
		SoundManager.instance.PauseBGM();
	}
	public void UnPause()
	{
		Debug.Log("UnPause");
		gameState = eGameState.Play;

		Time.timeScale = 1.0f;

		UIPause.instance.Invoke_Pause();
		SoundManager.instance.UnPauseBGM();
	}
	#endregion

	#region FSM GameOver
	void InGameOver()
	{
		Debug.Log("InGameOver");
		gameState = eGameState.GameOver;

		SceneManager.LoadScene("GameOverScene");
	}
	#endregion


	void Update()
	{
		switch (gameState)
		{
			case eGameState.Play:
				ModifyPlay();
				break;
		}

	}
}