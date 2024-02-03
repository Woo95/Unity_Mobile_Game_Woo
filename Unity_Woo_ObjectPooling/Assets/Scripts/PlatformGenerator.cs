using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
	public GameObject m_Player;

	public GameObject[] m_MapPrefabs;

	public int m_MapPoolAmount;
	private GameObject[] m_MapPool;
	private int[] m_OffMapIndices;

	public int m_SpawnAmount;

	private const float m_GroundLength = 10.0f;
	private const float m_InitialSpawnOffsetY = -1.0f;
	private Vector3 m_NextSpawnPoint;

	private void Start()
	{
		Init();
	}

	#region SetUp - Run Only Once
	private void Init()
	{
		m_NextSpawnPoint = m_Player.transform.position + new Vector3(0.0f, m_InitialSpawnOffsetY, 0.0f);

		transform.position = m_NextSpawnPoint + new Vector3(0.0f, 0.0f, -m_GroundLength);   // locating this.gameObject


		CreateMapPool();

		InitialMapSpawn();
	}

	// Create the initial map pool from the map prefabs
	private void CreateMapPool()
	{
		m_MapPool = new GameObject[m_MapPoolAmount];

		for (int i = 0; i < m_MapPoolAmount; i++)
		{
			GameObject mapInstance = Instantiate(m_MapPrefabs[i % m_MapPrefabs.Length], Vector3.zero, Quaternion.identity, transform);
			mapInstance.SetActive(false);

			m_MapPool[i] = mapInstance;
		}
	}

	// Spawn the initial maps at the start of the game
	private void InitialMapSpawn()
	{
		if (m_SpawnAmount > m_MapPoolAmount) // error handler
		{
			Debug.LogError("Initial spawn amount is greater than the map pool amount");
			return;
		}

		// Create List of all map with index
		List<int> mapList = new List<int>();
		for (int i = 0; i < m_MapPoolAmount; i++)
		{
			mapList.Add(i);
		}

		// Update map list as it spawns
		for (int i = 0; i < m_SpawnAmount; i++)
		{
			int rand = mapList[Random.Range(0, mapList.Count)];
			mapList.Remove(rand);

			m_MapPool[rand].transform.position = m_NextSpawnPoint;
			m_MapPool[rand].SetActive(true);

			m_NextSpawnPoint += new Vector3(0.0f, 0.0f, m_GroundLength);
		}

		CreateOffMapIndices(mapList);
	}

	// Create an array of indices for maps that are turned off
	private void CreateOffMapIndices(List<int> offMapList)
	{
		m_OffMapIndices = new int[offMapList.Count];

		for (int i=0; i < offMapList.Count; i++)
		{
			m_OffMapIndices[i] = offMapList[i];
		}
	}
	#endregion

	// Swap an old map with a randomly selected inactive(new) map
	private void SwapMap(int oldIndex)
	{
		int randIndex = Random.Range(0, m_OffMapIndices.Length);
		int newIndex = m_OffMapIndices[randIndex];
		m_OffMapIndices[randIndex] = oldIndex;

		m_NextSpawnPoint = m_MapPool[oldIndex].transform.position + new Vector3(0.0f, 0.0f, m_GroundLength * m_SpawnAmount);
		m_MapPool[newIndex].transform.position = m_NextSpawnPoint;

		m_MapPool[newIndex].SetActive(true);
		m_MapPool[oldIndex].SetActive(false);
	}

	// Handle trigger events (when the last map has to be deactivated on touching the map generator at the back of the player)
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Map"))
		{
			int index = System.Array.IndexOf(m_MapPool, other.gameObject);  // index of the last map that is about to deactivate 
			if (index != -1)    // if found object from the map pool
			{
				SwapMap(index);
			}
		}
	}
}