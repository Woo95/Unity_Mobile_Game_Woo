using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum eEnemyType { SLIME, BEE, GOBLIN, WOLF }
public class Enemy : MonoBehaviour
{
	public EnemyData m_EnemyData;
	private SpriteRenderer m_SpriteRenderer;
	public Transform centerTrans;
	Transform trans;

	public enum eEnemyState { NONE, MOVE, ATTACK }
	public eEnemyState enemyState;

	public Guardian m_Target1;
	public GuardianTower m_Target2;
	public CentralTower m_Target3;
	public LayerMask layerMask;

	private float m_damaged;

	public void SetData(Transform parent = null)
	{
		transform.SetParent(parent);
		gameObject.SetActive(true);
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		trans = transform;
		m_Target3 = CentralTower.instance;

		InitMove();
	}

	#region FSM Move
	public void InitMove()
	{
		enemyState = eEnemyState.MOVE;
	}
	public void ModifyMove()
	{
		float distance;
		if (m_Target1 != null)
		{
			distance = Vector3.Distance(centerTrans.position, m_Target1.centerTrans.position);
			if (distance > m_EnemyData.releaseRadius)
			{
				m_Target1 = null;
			}
			else if (distance < m_EnemyData.attackRadius)
			{
				InitAttack();
				return;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_Target1.centerTrans.position, m_EnemyData.speed * Time.deltaTime);
			}
		}
		else if (m_Target2 != null)
		{
			distance = Vector3.Distance(centerTrans.position, m_Target2.transform.position);
			if (distance > m_EnemyData.releaseRadius)
			{
				m_Target2 = null;
			}
			else if (distance < m_EnemyData.attackRadius)
			{
				InitAttack();
				return;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_Target2.transform.position, m_EnemyData.speed * Time.deltaTime);
			}
		}
		else if (m_Target3 != null)
		{
			distance = Vector3.Distance(centerTrans.position, m_Target3.transform.position);
			if (distance < m_EnemyData.attackRadius)
			{
				InitAttack();
				return;
			}
			else
			{
				trans.position =
				Vector3.MoveTowards(trans.position, m_Target3.transform.position, m_EnemyData.speed * Time.deltaTime);
			}
		}
		SearchPlayer();
	}
	#endregion

	#region FSM Attack
	float attackTrackTime;
	public void InitAttack()
	{
		enemyState = eEnemyState.ATTACK;
		attackTrackTime = Time.time;
	}
	public void ModifyAttack()
	{
		if (m_Target1 != null)
		{
			if (Vector3.Distance(centerTrans.position, m_Target1.centerTrans.position) > m_EnemyData.attackRadius)
			{
				InitMove();
				return;
			}
		}
		else if (m_Target2 != null)
		{
			if (Vector3.Distance(trans.position, m_Target2.transform.position) > m_EnemyData.attackRadius)
			{
				InitMove();
				return;
			}
		}
		else if (m_Target3 != null)
		{
			if (Vector3.Distance(trans.position, m_Target3.transform.position) > m_EnemyData.attackRadius)
			{
				InitMove();
				return;
			}
		}
		if (Time.time > attackTrackTime)
		{
			attackTrackTime = Time.time + m_EnemyData.ATTACK_TIME;
			if (m_Target1 != null) m_Target1.TakeDamage(m_EnemyData.damage);
			else if (m_Target2 != null) m_Target2.TakeDamage(m_EnemyData.damage);
			else if (m_Target3 != null) m_Target3.TakeDamage(m_EnemyData.damage);
		}
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

		float damage = m_damaged;
		m_damaged = 0;
		if (damage > 0)
		{
			m_EnemyData.health -= damage;
			if (m_EnemyData.health <= 0)
			{
				Dead();
				return;
			}
		}

		m_SpriteRenderer.sortingOrder = (int)(trans.position.y * -100.0f);
	}

	#region SearchPlayer Function
	float checkTime = 0.0f;
	float CONST_CHECKTIME = 0.5f;
	void SearchPlayer()
	{
		if (Time.time < checkTime) return; 
		if (m_Target1) return;
		if (m_Target1 == null && m_Target2) return;
		checkTime = Time.time + CONST_CHECKTIME;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerTrans.position, m_EnemyData.searchRadius, layerMask);

		for (int i = 0; i < colliders.Length; i++)
		{
			Guardian target1 = colliders[i].GetComponent<Guardian>();
			if (target1 != null)
			{
				m_Target1 = target1;
				return;
			}

			GuardianTower target2 = colliders[i].GetComponent<GuardianTower>();
			if (target2 != null)
			{
				m_Target2 = target2;
				return;
			}
		}
	}
	#endregion

	public void TakeDamage(float damaged)
	{
		m_damaged += damaged;
	}

	private void Dead()
	{
		Destroy(gameObject);
		EnemyManager.instance.Remove(this);
	}

	private void OnDrawGizmos()
	{
		Gizmos2.DrawCircle2(centerTrans.position, Color.grey, m_EnemyData.releaseRadius);
		Gizmos2.DrawCircle2(centerTrans.position, Color.yellow, m_EnemyData.searchRadius);
		Gizmos2.DrawCircle2(centerTrans.position, Color.red, m_EnemyData.attackRadius);
	}
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
}