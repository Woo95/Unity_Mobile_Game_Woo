using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public PlayerController m_Target;
	public EnemyData m_EnemyData;

	public enum eEnemyState { NONE, IDLE, CHASE, ATTACK };

	public eEnemyState m_EnemyState;

	public abstract void Init();

	#region FSM Idle
	public abstract void InIdle();
	public abstract void ModifyIdle();
	#endregion

	#region FSM CHASE
	public abstract void InChase();
	public abstract void ModifyChase();
	#endregion

	#region FSM ATTACK
	public abstract void InAttack();
	public abstract void ModifyAttack();
	#endregion

	public void Update()
	{
		switch (m_EnemyState)
		{
			case eEnemyState.IDLE:
				ModifyIdle(); 
				break;
			case eEnemyState.CHASE:
				ModifyChase();
				break;
			case eEnemyState.ATTACK:
				ModifyAttack();
				break;
		}
	}
}
