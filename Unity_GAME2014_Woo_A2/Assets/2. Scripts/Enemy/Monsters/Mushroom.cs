using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
	public LayerMask m_LayerMask;

	Rigidbody2D m_Rb;
	public Transform m_PatrolChecker;
	public float m_PatrolCheckerRadius = 0.2f;

	public override void Init()
	{
		m_Target = null;

		m_Rb = GetComponent<Rigidbody2D>();
		m_MoveSpeed = 2.0f;

		if (Random.Range(0, 2) == 0)
		{
			Flip();
		}

		InPatrol();
	}

	#region FSM Patrol
	public override void InPatrol()
	{
		m_EnemyState = eEnemyState.PATROL;
	}
	public override void ModifyPatrol()
	{
		m_Rb.velocity = new Vector2(-m_MoveSpeed, m_Rb.velocity.y);

		if (!Physics2D.OverlapCircle(m_PatrolChecker.position, m_PatrolCheckerRadius, m_LayerMask))
		{
			Flip();
		}
	}


	private void Flip()
	{
		m_MoveSpeed *= -1.0f;
		transform.right *= -1.0f;
	}
	#endregion

	#region Gizmo Drawing
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(m_PatrolChecker.position, m_PatrolCheckerRadius);
	}
	#endregion
}
