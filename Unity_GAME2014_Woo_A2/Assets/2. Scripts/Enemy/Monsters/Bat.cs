using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
	public override void Init()
	{
		m_Target = null;

		m_MoveSpeed = 1.5f;

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
			FlipBat(m_Target.transform.position.x);
			transform.position = Vector3.MoveTowards(transform.position, m_Target.transform.position, m_MoveSpeed * Time.deltaTime);
		}
	}
	#endregion

	private void FlipBat(float targetXPosition)
	{
		if (targetXPosition < transform.position.x)
		{
			transform.right = Vector3.right;
		}
		else
		{
			transform.right = Vector3.left;
		}
	}
}
