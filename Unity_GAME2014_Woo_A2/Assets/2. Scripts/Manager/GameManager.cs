using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	enum eGameState { PLAY, GAMEOVER, PAUSE };
	eGameState m_GameState;
	enum eResultStatus { NONE, WON, LOST }
	eResultStatus m_ResultStatus;

	public float m_GameOverTimeDelay;

	void Start()
	{
		InPlay();
	}

	#region FSM Play
	void InPlay()
	{
		Debug.Log("InPlay");
		m_GameState = eGameState.PLAY;
		m_ResultStatus = eResultStatus.NONE;

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

		if (m_ResultStatus == eResultStatus.NONE)
		{
			if (!Timer.instance.UpdateTimer() || !PlayerManager.instance.IsAlive())
			{
				m_ResultStatus = eResultStatus.LOST;

				InGameOver();
				return;
			}

			if (PlayerManager.instance.Finished())
			{
				m_ResultStatus = eResultStatus.WON;

				SoundManager.instance.PlaySFX("Goal");
				EnemyManager.instance.DestroyAllEnemies();
				InGameOver();
				return;
			}
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
		m_GameState = eGameState.PAUSE;

		Time.timeScale = 0f;

		UIPause.instance.Invoke_Pause(true);
		SoundManager.instance.PauseBGM();
	}
	public void UnPause()
	{
		Debug.Log("UnPause");
		m_GameState = eGameState.PLAY;

		Time.timeScale = 1.0f;

		UIPause.instance.Invoke_Pause(false);
		SoundManager.instance.UnPauseBGM();
	}
	#endregion
	#region FSM GameOver
	void InGameOver()
	{
		Debug.Log("InGameOver");
		m_GameState = eGameState.GAMEOVER;

		bool isWon = m_ResultStatus == eResultStatus.WON;

		m_GameOverTimeDelay = isWon ? Time.time + 4.0f : Time.time + 1.0f;
		GameOverData.SetData(PlayerManager.instance.GetScore(), Timer.instance.GetTimer(), isWon);
	}
	void ModifyGameOver()
	{
		Debug.Log("ModifyGameOver");
		if (Time.time >= m_GameOverTimeDelay)
		{
			SceneManager.LoadScene("GameOverScene");
		}
	}
	#endregion

	void Update()
	{
		switch (m_GameState)
		{
			case eGameState.PLAY:
				ModifyPlay();
				break;
			case eGameState.GAMEOVER:
				ModifyGameOver();
				break;
		}
	}
}

