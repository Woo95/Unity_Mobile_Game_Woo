using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyType { Bat, Small_MushRoom, Goblin_Range }

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy/New EnemyData")]
public class EnemyData : ScriptableObject
{
	public Sprite m_Sprite;
	public eEnemyType m_EnemyType;

	public float m_SpawnTime;
	public float m_Speed;
}
