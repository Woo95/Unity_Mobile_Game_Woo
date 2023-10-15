using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public enum eEnemyType { SLIME, BEE, GOBLIN, WOLF }
public class Enemy : MonoBehaviour
{
	public EnemyData m_EnemyData;
	private SpriteRenderer m_SpriteRenderer;
	Transform trans;

	public enum eEnemyState { NONE, MOVE, ATTACK }
	public eEnemyState enemyState;


	private Guardian m_target1;
	private GuardianTower m_target2;
	private CentralTower m_target3;
	public LayerMask layerMask;

	public void SetData(Transform parent = null)
	{
		transform.SetParent(parent);
		gameObject.SetActive(true);
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		trans = transform;
		m_target3 = CentralTower.instance;

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
			if (distance > m_EnemyData.releaseRadius)
			{
				m_target1 = null;
			}
			else if (distance < m_EnemyData.attackRadius)
			{
				isAttacking = true;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_target1.transform.position, m_EnemyData.speed * Time.deltaTime);
			}
		}
		else if (m_target2 != null)
		{
			distance = Vector3.Distance(trans.position, m_target2.transform.position);
			if (distance > m_EnemyData.releaseRadius)
			{
				m_target2 = null;
			}
			else if (distance < m_EnemyData.attackRadius)
			{
				isAttacking = true;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_target2.transform.position, m_EnemyData.speed * Time.deltaTime);
			}
		}
		else if (m_target3 != null)
		{
			distance = Vector3.Distance(trans.position, m_target3.transform.position);
			if (distance < m_EnemyData.attackRadius)
			{
				isAttacking = true;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_target3.transform.position, m_EnemyData.speed * Time.deltaTime);
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
			if (Vector3.Distance(trans.position, m_target1.transform.position) > m_EnemyData.attackRadius)
			{
				InitMove();
			}
		}
		else if (m_target2 != null)
		{
			if (Vector3.Distance(trans.position, m_target2.transform.position) > m_EnemyData.attackRadius)
			{
				InitMove();
			}
		}
		else if (m_target3 != null)
		{
			if (Vector3.Distance(trans.position, m_target3.transform.position) > m_EnemyData.attackRadius)
			{
				InitMove();
			}
		}
		Debug.Log("@@Attack@@");
	}
	#endregion

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
		Gizmos2.DrawCircle2(transform.position, Color.grey, m_EnemyData.releaseRadius);
		Gizmos2.DrawCircle2(transform.position, Color.yellow, m_EnemyData.searchRadius);
		Gizmos2.DrawCircle2(transform.position, Color.red, m_EnemyData.attackRadius);
	}

	#region SearchPlayer Function
	float checkTime = 0.0f;
	float CONST_CHECKTIME = 0.5f;
	void SearchPlayer()
	{
		if (Time.time < checkTime) return; 
		if (m_target1) return;
		if (m_target1 == null && m_target2) return;
		checkTime = Time.time + CONST_CHECKTIME;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(trans.position, m_EnemyData.searchRadius, layerMask);

		for (int i = 0; i < colliders.Length; i++)
		{
			Guardian target1 = colliders[i].GetComponent<Guardian>();
			if (target1 != null)
			{
				m_target1 = target1;
				return;
			}

			GuardianTower target2 = colliders[i].GetComponent<GuardianTower>();
			if (target2 != null)
			{
				m_target2 = target2;
				return;
			}
		}
	}
	#endregion
}

[System.Serializable]
public class EnemyData
{
	public eEnemyType EnemyType;

	public float health;
	public float speed;
	public float damage;
	public int gold;
	public float searchRadius;
	public float releaseRadius;
	public float attackRadius;
	public float ATTACK_TIME;

	/*
	eEnemyType.SLIME:
				m_health = 50.0f;
				m_speed = 0.25f;
				m_damage = 1.0f;
				m_gold = 1;
				m_searchRadius = 3.0f;
				m_releaseRadius = 4.0f;
				m_attackRadius = 0.5f;
				m_ATTACK_TIME = 1.5f;
	eEnemyType.BEE:
				m_health = 70.0f;
				m_speed = 1.0f;
				m_damage = 3.0f;
				m_gold = 2;
				m_searchRadius = 7.0f;
				m_releaseRadius = 9.0f;
				m_attackRadius = 0.7f;
				m_ATTACK_TIME = 2.5f;
	eEnemyType.GOBLIN:
				m_health = 120.0f;
				m_speed = 0.5f;
				m_damage = 5.0f;
				m_gold = 3;
				m_searchRadius = 4.0f;
				m_releaseRadius = 6.0f;
				m_attackRadius = 0.6f;
				m_ATTACK_TIME = 2.0f;
	eEnemyType.WOLF:
				m_health = 90.0f;
				m_speed = 0.75f;
				m_damage = 4.0f;
				m_gold = 4;
				m_searchRadius = 6.0f;
				m_releaseRadius = 8.0f;
				m_attackRadius = 0.8f;
				m_ATTACK_TIME = 2.0f;
	 */
}