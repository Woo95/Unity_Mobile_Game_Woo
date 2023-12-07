using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
	private void Start()
	{
		Init();
	}
	public override void Init()
	{
		m_Target = null;

		m_MoveSpeed = 1.0f;

		InChase();
	}

	#region FSM CHASE
	public override void InChase()
	{
		m_EnemyState = eEnemyState.CHASE;

		m_Target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	public override void ModifyChase()
	{
		if (m_Target)
		{
			transform.position = Vector3.MoveTowards(transform.position, m_Target.transform.position, m_MoveSpeed * Time.deltaTime);
		}
	}
	#endregion
}
