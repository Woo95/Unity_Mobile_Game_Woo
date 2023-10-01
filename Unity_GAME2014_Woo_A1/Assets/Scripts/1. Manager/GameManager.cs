/*
FileName: GameManager.cs
Name: Chaewan Woo
Student #101354291
Last Modified: Sept 30

About Script:
This is a game manager.

Revision History:
- Added FSM of the game (game manager).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGameState
{
	None,
	Menu,
	Play,
	GameOver
}

public class GameManager : MonoBehaviour
{
	public eGameState gameState = eGameState.None;
	public UIPlayScene m_UIPlayScene;

	void Start()
	{
		InMenu();
	}

	#region FSM Menu
	void InMenu() // calls only once.
	{
		Debug.Log("InMenu");
		gameState = eGameState.Menu;
	}
	void ModifyMenu() // loops forever unless it meets any condition
	{
		Debug.Log("ModifyMenu");
		InPlay();
		return;
	}
	#endregion

	#region FSM Play
	void InPlay()
	{
		Debug.Log("InPlay");
		gameState = eGameState.Play;

		SoundManager.instance.PlayBGM("GamePlayBGM", 0.25f);
	}
	void ModifyPlay()
	{
		Debug.Log("ModifyPlay");

		if (Input.GetKeyUp(KeyCode.Alpha0))
		{
			InGameOver();
			return;
		}

	}
	#endregion

	#region FSM GameOver
	void InGameOver()
	{
		Debug.Log("InGameOver");
		gameState = eGameState.GameOver;

		m_UIPlayScene.GameOver();
		SoundManager.instance.StopBGM();
	}
	void ModifyGameOver()
	{
		Debug.Log("ModifyGameOver");
		return;
	}
	#endregion


	void Update()
	{
		switch (gameState)
		{
			case eGameState.Menu:
				ModifyMenu();
				break;
			case eGameState.Play:
				ModifyPlay();
				break;
			case eGameState.GameOver:
				ModifyGameOver();
				break;
		}

	}
}