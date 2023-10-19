using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	#region singletone;
	public static EnemyManager instance;
	public void Awake()
	{
		instance = this;
	}
	#endregion
	public enum eEnemyManagerState { NONE, WAIT, SPAWNING, END }
	public eEnemyManagerState enemyManagerState;

	
	public float EACH_WAVE_TIME = 30.0f;
	public float END_WAVE_TIME = 300.0f;
	public float SPAWN_INTERVAL = 5.0f;

	public float nextSpawnTime;
	public int currentWave;

	public List<Enemy> enemyPrefabList = new List<Enemy>();
	public List<Enemy> enemySpawnedList = new List<Enemy>();

	public Transform topLeft, bottomRight;
	Vector3 p00, p01, p11, p10;
	/*	p01		p11
	 	p00		p10
	*/
	public void Init()
	{
		p01 = topLeft.position;
		p10 = bottomRight.position;
		p00 = new Vector3(p01.x, p10.y);
		p11 = new Vector3(p10.x, p01.y);
		InitWait();
	}

	#region FSM Wait
	public void InitWait()
	{
		enemyManagerState = eEnemyManagerState.WAIT;
	}
	public void ModifyWait()
	{
		InitSpawning();
		return;
	}
	#endregion

	#region FSM Spawning
	public void InitSpawning()
	{
		enemyManagerState = eEnemyManagerState.SPAWNING;

		currentWave = 1;
		CentralTower.instance.AddWave(currentWave);
		nextSpawnTime = Time.time;
}
	public void ModifySpawning()
	{
		if (Time.time >= nextSpawnTime)
		{
			SpawnEnemy();
			nextSpawnTime = Time.time + SPAWN_INTERVAL;
		}
		
		if (Time.time > EACH_WAVE_TIME)
		{
			CentralTower.instance.AddWave(++currentWave);
			SPAWN_INTERVAL -= 0.3f;

			EACH_WAVE_TIME = Time.time + 60.0f;

			if (currentWave > 10)
			{
				InitEnd();
				return;
			}
		}
	}
	#endregion

	#region FSM End
	public void InitEnd()
	{
		enemyManagerState = eEnemyManagerState.END;
	}
	public void ModifyEnd()
	{
	}
	#endregion

	public void Run()
	{
		switch (enemyManagerState)
		{
			case eEnemyManagerState.WAIT:
				ModifyWait();
				break;

			case eEnemyManagerState.SPAWNING:
				ModifySpawning();
				break;

			case eEnemyManagerState.END:
				ModifyEnd();
				break;
		}
	}

	#region SpawnEnemy
	public void SpawnEnemy()
	{
		if (enemyPrefabList.Count > 0)
		{
			int enemyIndex = Random.Range(0, enemyPrefabList.Count);
			Vector3 pos = GetPosition();
			Quaternion rotation = Quaternion.identity;

			if (enemyPrefabList[enemyIndex] != null)
			{
				Enemy enemy = Instantiate(enemyPrefabList[enemyIndex], pos, rotation, transform);
				enemy.SetData();
				enemySpawnedList.Add(enemy);
			}
		}
	}

	public void Remove(Enemy enemy)
	{
		if (enemySpawnedList.Contains(enemy))
			enemySpawnedList.Remove(enemy);
	}

	public Vector3 GetPosition()
	{
		Vector3 spawnPosition = Vector3.zero;
		int randomSide = Random.Range(0, 4);
		float interval = Random.Range(0f, 1f);
		switch (randomSide)
		{
			case 0: spawnPosition = Vector3.Lerp(p00, p10, interval); break;
			case 1: spawnPosition = Vector3.Lerp(p01, p11, interval); break;
			case 2: spawnPosition = Vector3.Lerp(p00, p01, interval); break;
			case 3: spawnPosition = Vector3.Lerp(p10, p11, interval); break;
		}
		return spawnPosition;
	}
	#endregion
}
