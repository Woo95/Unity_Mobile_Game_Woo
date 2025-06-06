using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eGameState
{
	None,
	Menu,
	Play,
	GameOver
}
public enum eTypeResult 
{ 
	None, 
	Won, 
	Lost 
}

public class GameManager : MonoBehaviour
{
	private eTypeResult typeResult = eTypeResult.None;
	public eGameState gameState = eGameState.None;
	public BuildManager m_BuildManager;
	public EnemyManager m_EnemyManager;
	public FieldManager m_FieldManager;
	public CameraManager m_CameraManager;
	public UnitManager m_UnitManager;
	public UIPlayScene m_UIPlayScene;

	public Text Text_GameOverStatus;

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

		m_BuildManager.Init();
		m_EnemyManager.Init();
		m_FieldManager.Init();
		m_CameraManager.Init();
		m_UnitManager.Init();

		CentralTower.instance.Init();

		SoundManager.instance.PlayBGM("GamePlayBGM", 0.25f);
	}
	void ModifyPlay()
	{
		Debug.Log("ModifyPlay");

		m_BuildManager.PlaceTower();
		m_FieldManager.PlaceObject();
		m_EnemyManager.Run();
		m_CameraManager.Play();
		m_UnitManager.Play();

		if (CentralTower.instance == null)
		{
			typeResult = eTypeResult.Lost;
			InGameOver();
			return;
		}
		if (EnemyManager.instance != null && EnemyManager.instance.enemyManagerState == EnemyManager.eEnemyManagerState.END)
		{
			typeResult = eTypeResult.Won;
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

		m_FieldManager.End();
		CentralTower.instance.End();
		m_UIPlayScene.GameOver();

		SoundManager.instance.StopBGM();
	}
	void ModifyGameOver()
	{
		if (typeResult == eTypeResult.Lost)
		{
			Text_GameOverStatus.text = "YOU LOST!";
		}
		else if (typeResult == eTypeResult.Won)
		{
			Text_GameOverStatus.text = "YOU WON!";
		}
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