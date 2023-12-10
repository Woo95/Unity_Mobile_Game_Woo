using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public PlayerController m_Target;
	protected float m_MoveSpeed;

	public enum eEnemyState { NONE, PATROL, CHASE, ATTACK };

	public eEnemyState m_EnemyState;

	public abstract void Init();

	#region FSM Patrol
	public virtual void InPatrol() { }
	public virtual void ModifyPatrol() { }
	#endregion

	#region FSM CHASE
	public virtual void InChase() { }
	public virtual void ModifyChase() { }
	#endregion

	#region FSM ATTACK
	public virtual void InAttack() { }
	public virtual void ModifyAttack() { }
	#endregion

	public void Update()
	{
		switch (m_EnemyState)
		{
			case eEnemyState.PATROL:
				ModifyPatrol(); 
				break;
			case eEnemyState.CHASE:
				ModifyChase();
				break;
			case eEnemyState.ATTACK:
				ModifyAttack();
				break;
		}
	}

	private void OnDestroy()
	{
		EnemyManager.instance.Remove(this);
	}
}
