using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyType { SLIME, BEE, GOBLIN, WOLF }
public class Enemy : MonoBehaviour
{
	public enum eEnemyState { NONE, MOVE, ATTACK }
	public eEnemyState enemyState;

	public float m_health = 100.0f;
	public float m_speed = 2.0f;
	public float m_damage = 2.0f;

	public float m_searchRadius = 5.0f;
	public float m_releaseRadius = 7.0f;
	public float m_attackRadius = 0.5f;
	public float ATTACK_TIME = 2.0f;

	public eEnemyType m_EnemyType;
	SpriteRenderer m_SpriteRenderer;
	Transform trans;

	public Guardian m_target1;
	public GuardianTower m_target2;
	public CentralTower m_target3;
	public LayerMask layerMask;

	public void SetData()
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		trans = transform;
		m_target3 = CentralTower.instance;
	}


	void Start()
	{
		SetData();
		InitMove();
	}

	#region FSM Move
	bool isAttacking;
	public void InitMove()
	{
		enemyState = eEnemyState.MOVE;
		isAttacking = false;
	}
	public void ModifyMove()
	{
		if (isAttacking)
		{
			isAttacking = false;
			InitAttack();
			return;
		}

		float distance;
		if (m_target1 != null)
		{
			distance = Vector3.Distance(trans.position, m_target1.transform.position);
			if (distance > m_releaseRadius)
			{
				m_target1 = null;
			}
			else if (distance < m_attackRadius)
			{
				isAttacking = true;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_target1.transform.position, m_speed * Time.deltaTime);
			}
		}
		else if (m_target2 != null)
		{
			distance = Vector3.Distance(trans.position, m_target2.transform.position);
			if (distance > m_releaseRadius)
			{
				m_target2 = null;
			}
			else if (distance < m_attackRadius)
			{
				isAttacking = true;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_target2.transform.position, m_speed * Time.deltaTime);
			}
		}
		else if (m_target3 != null)
		{
			distance = Vector3.Distance(trans.position, m_target3.transform.position);
			if (distance < m_attackRadius)
			{
				isAttacking = true;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_target3.transform.position, m_speed * Time.deltaTime);
			}
		}

		SearchPlayer();
	}
	#endregion

	#region FSM Attack
	public void InitAttack()
	{
		enemyState = eEnemyState.ATTACK;
	}
	public void ModifyAttack()
	{
		if (m_target1 != null)
		{
			if (Vector3.Distance(trans.position, m_target1.transform.position) > m_attackRadius)
			{
				InitMove();
			}
		}
		else if (m_target2 != null)
		{
			if (Vector3.Distance(trans.position, m_target2.transform.position) > m_attackRadius)
			{
				InitMove();
			}
		}
		else if (m_target3 != null)
		{
			if (Vector3.Distance(trans.position, m_target3.transform.position) > m_attackRadius)
			{
				InitMove();
			}
		}
		Debug.Log("@@Attack@@");
	}
	#endregion

	float checkTime = 0.0f;
	float CONST_CHECKTIME = 0.5f;
	void SearchPlayer()
	{
		if (Time.time < checkTime) return;
		checkTime = Time.time + CONST_CHECKTIME;
		Debug.Log("1");
		Collider[] collider = Physics.OverlapSphere(trans.position, m_searchRadius, layerMask);

		for (int i = 0; i < collider.Length; i++)
		{
			Guardian target1 = collider[i].GetComponent<Guardian>();
			if (target1 != null)
			{
				m_target1 = target1;
				return;
			}

			GuardianTower target2 = collider[i].GetComponent<GuardianTower>();
			if (target2 != null)
			{
				m_target2 = target2;
				return;
			}
		}
	}

	void Update()
	{
		switch (enemyState)
		{
			case eEnemyState.MOVE:
				ModifyMove();
				break;

			case eEnemyState.ATTACK:
				ModifyAttack();
				break;
		}
		m_SpriteRenderer.sortingOrder = (int)(trans.position.y * -100.0f);
	}

	private void OnDrawGizmos()
	{
		Gizmos2.DrawCircle2(transform.position, Color.grey, m_releaseRadius);
		Gizmos2.DrawCircle2(transform.position, Color.yellow, m_searchRadius);
		Gizmos2.DrawCircle2(transform.position, Color.red, m_attackRadius);
	}
	/**/
}