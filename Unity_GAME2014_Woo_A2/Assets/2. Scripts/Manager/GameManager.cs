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
			StartCoroutine(Co_GameOver(false));
			return;
		}

		if (PlayerManager.instance.Finished())
		{
			SoundManager.instance.StopBGM("GamePlayBGM");
			SoundManager.instance.PlayBGM("GoalBGM", 0.2f, false);
			StartCoroutine(Co_GameOver(true));
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
	IEnumerator Co_GameOver(bool isWon)
	{
		if (isWon)
			yield return new WaitForSeconds(4.0f);
		else
			yield return new WaitForSeconds(1.0f);

		InGameOver(isWon);
	}
	void InGameOver(bool isWon)
	{
		Debug.Log("InGameOver");
		gameState = eGameState.GAMEOVER;

		GameOverData.SetData(PlayerManager.instance.GetScore(), Timer.instance.GetTimer(), isWon);

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

