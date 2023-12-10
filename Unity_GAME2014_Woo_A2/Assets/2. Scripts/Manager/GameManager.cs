using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public enum eGameState { NONE, PLAY, GAMEOVER, PAUSE };

	public eGameState gameState = eGameState.NONE;

	void Start()
	{
		InPlay();
	}

	#region FSM Play
	void InPlay()
	{
		Debug.Log("InPlay");
		gameState = eGameState.PLAY;

		Time.timeScale = 1.0f;

		PlayerManager.instance.Init();
		EnemyManager.instance.Init();
		CameraControl.instance.Init();
		UIGamePlay.instance.Init();
		UIPause.instance.Init();
		SoundManager.instance.Init();

		GameOverData.Init();
	}
	void ModifyPlay()
	{
		Debug.Log("ModifyPlay");

		if (!Timer.instance.UpdateTimer() || !PlayerManager.instance.IsAlive())
		{
			InGameOver(false);
			return;
		}

		if (PlayerManager.instance.Finished())
		{
			InGameOver(true);
			return;
		}

		PlayerManager.instance.InputHandler();

		CameraControl.instance.TrackPlayer();

		EnemyManager.instance.Run();
	}
	#endregion

	#region FSM Pause
	public void InPause()
	{
		Debug.Log("InPause");
		gameState = eGameState.PAUSE;

		Time.timeScale = 0f;

		UIPause.instance.Invoke_Pause();
		SoundManager.instance.PauseBGM();
	}
	public void UnPause()
	{
		Debug.Log("UnPause");
		gameState = eGameState.PLAY;

		Time.timeScale = 1.0f;

		UIPause.instance.Invoke_Pause();
		SoundManager.instance.UnPauseBGM();
	}
	#endregion

	#region FSM GameOver
	void InGameOver(bool isWon)
	{
		Debug.Log("InGameOver");
		gameState = eGameState.GAMEOVER;

		GameOverData.SetData(PlayerManager.instance.GetObtainedCoin(), Timer.instance.GetTimer(), isWon);

		SceneManager.LoadScene("GameOverScene");
	}
	#endregion


	void Update()
	{
		switch (gameState)
		{
			case eGameState.PLAY:
				ModifyPlay();
				break;
		}

	}
}