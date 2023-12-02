using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyType { Bat, Small_MushRoom, Goblin_Range }

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/New Enemy")]
public class Enemy : ScriptableObject
{
	public Sprite m_Sprite;
	public eEnemyType m_EnemyType;
}
