using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	enum eGameState { PLAY, GAMEOVER, PAUSE };
	eGameState m_GameState;
	enum eResultStatus { NONE, WON, LOST }
	eResultStatus m_ResultStatus;

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

				StartCoroutine(Co_GameOver(false));
				return;
			}

			if (PlayerManager.instance.Finished())
			{
				m_ResultStatus = eResultStatus.WON;

				SoundManager.instance.StopBGM("GamePlayBGM");
				SoundManager.instance.PlayBGM("GoalBGM", 0.2f, false);
				EnemyManager.instance.DestroyAllEnemies();
				StartCoroutine(Co_GameOver(true));
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

		UIPause.instance.Invoke_Pause();
		SoundManager.instance.PauseBGM();
	}
	public void UnPause()
	{
		Debug.Log("UnPause");
		m_GameState = eGameState.PLAY;

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
		m_GameState = eGameState.GAMEOVER;

		GameOverData.SetData(PlayerManager.instance.GetScore(), Timer.instance.GetTimer(), isWon);

		SceneManager.LoadScene("GameOverScene");
	}
	#endregion

	void Update()
	{
		switch (m_GameState)
		{
			case eGameState.PLAY:
				ModifyPlay();
				break;
		}
	}
}

