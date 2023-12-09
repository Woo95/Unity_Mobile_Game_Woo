using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroundMobData
{
	public Transform m_GroundMobSpawnPoint;
	public Enemy m_GroundMob;

	[HideInInspector]
	public float m_NextGroundMobSpawnTime;
}

public class EnemyManager : MonoBehaviour
{
	#region singletone;
	public static EnemyManager instance;
	public void Awake()
	{
		instance = this;
	}
	#endregion
	public enum eEnemyManagerState { NONE, SPAWNING, END }
	public eEnemyManagerState enemyManagerState;

	[Header("Sky Mob - Settings")]
	float SPAWN_BAT_INTERVAL = 20.0f;
	float m_NextSkyMobSpawnTime;
	public List<Enemy>			m_SkyMobPrefabList = new List<Enemy>();

	[Header("Ground Mob - Settings")]
	float SPAWN_GROUND_MOB_INTERVAL = 10.0f;
	public List<Enemy>			m_GroundMobPrefabList = new List<Enemy>();
	public List<GroundMobData>	m_GroundMobList = new List<GroundMobData>();

	[Header("Spawned Enemies")]
	public List<Enemy> m_EnemySpawnedList = new List<Enemy>();

	Vector2 p00, p01, p11, p10;
	/*	p01		p11
	 	p00		p10
	*/
	public void Init()
	{
		InitSpawning();
	}

	#region FSM Spawning
	public void InitSpawning()
	{
		enemyManagerState = eEnemyManagerState.SPAWNING;

		m_NextSkyMobSpawnTime = Time.time;
	}
	public void ModifySpawning()
	{
		if (Time.time >= m_NextSkyMobSpawnTime)
		{
			SpawnSkyMob();
		}

		for (int i=0; i < m_GroundMobList.Count; i++)
		{
			if (m_GroundMobList[i].m_GroundMob == null)
			{
				if (Time.time >= m_GroundMobList[i].m_NextGroundMobSpawnTime)
				{
					SpawnGroundMob(m_GroundMobList[i]);
				}
			}
		}
	}
	#endregion

	public void Run()
	{
		switch (enemyManagerState)
		{
			case eEnemyManagerState.SPAWNING:
				ModifySpawning();
				break;
		}
	}

	#region SpawnEnemy
	#region Sky Mob
	public void SpawnSkyMob()
	{
		if (m_SkyMobPrefabList.Count <= 0)
		{
			Debug.LogError("No Sky Mob Prefab Found"); 
			return;
		}	

		int enemyIndex = Random.Range(0, m_SkyMobPrefabList.Count);
		Vector3 pos = GetSkyMobSpawnPosition();
		Quaternion rot = Quaternion.identity;

		Enemy enemy = Instantiate(m_SkyMobPrefabList[enemyIndex], pos, rot, transform);
		enemy.Init();

		m_EnemySpawnedList.Add(enemy);

		m_NextSkyMobSpawnTime = Time.time + SPAWN_BAT_INTERVAL;
			

	}
	public Vector3 GetSkyMobSpawnPosition()
	{
		float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
		float halfHeight = Camera.main.orthographicSize;

		Vector3 cameraPos = Camera.main.transform.position;
		p00 = new Vector2(cameraPos.x - halfWidth, cameraPos.y - halfHeight);
		p01 = new Vector2(cameraPos.x - halfWidth, cameraPos.y + halfHeight);
		p11 = new Vector2(cameraPos.x + halfWidth, cameraPos.y + halfHeight);
		p10 = new Vector2(cameraPos.x + halfWidth, cameraPos.y - halfHeight);


		Vector3 spawnPosition = Vector3.zero;
		int randomSide = Random.Range(0, 3);
		float interval = Random.Range(0f, 1f);
		switch (randomSide)
		{
			case 0: spawnPosition = Vector3.Lerp(p00, p01, interval); break;
			case 1: spawnPosition = Vector3.Lerp(p01, p11, interval); break;
			case 2: spawnPosition = Vector3.Lerp(p10, p11, interval); break;
		}
		return spawnPosition;
	}
	#endregion

	#region Ground Mob
	public void SpawnGroundMob(GroundMobData spawnGroundData)
	{
		if (m_GroundMobPrefabList.Count <= 0)
		{
			Debug.LogError("No Ground Mob Prefab Found");
			return;
		}

		int enemyIndex = Random.Range(0, m_GroundMobPrefabList.Count);
		Vector3 pos = spawnGroundData.m_GroundMobSpawnPoint.position;
		Quaternion rot = Quaternion.identity;

		Enemy enemy = Instantiate(m_GroundMobPrefabList[enemyIndex], pos, rot, transform);
		spawnGroundData.m_GroundMob = enemy;
		enemy.Init();

		m_EnemySpawnedList.Add(enemy);
	}
	#endregion

	public void Remove(Enemy enemy)
	{
		if (m_EnemySpawnedList.Contains(enemy))
			m_EnemySpawnedList.Remove(enemy);

		for (int i=0; i< m_GroundMobList.Count; i++)
		{
			if (m_GroundMobList[i].m_GroundMob == enemy)
			{
				m_GroundMobList[i].m_NextGroundMobSpawnTime = Time.time + SPAWN_GROUND_MOB_INTERVAL;
				break;
			}
		}
	}
	#endregion

	#region Gizmo Drawing
	//private void OnDrawGizmos()
	//{
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawLine(p00, p01);
	//	Gizmos.DrawLine(p01, p11);
	//	Gizmos.DrawLine(p10, p11);
	//}
	#endregion
}