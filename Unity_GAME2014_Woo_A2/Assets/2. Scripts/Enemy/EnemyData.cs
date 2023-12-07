using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy/New EnemyData")]
public class EnemyData : ScriptableObject
{
	public float m_SpawnTime;
	public float m_MoveSpeed;
}
