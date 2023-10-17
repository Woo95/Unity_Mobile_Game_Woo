using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGuardianType { G1, G2, G3 }
public class Guardian : MonoBehaviour
{
	public GuardianData m_GuardianData;
    SpriteRenderer m_SpriteRenderer;
	public Transform centerTrans;
	Transform trans;

	public enum eGuardianState { NONE, IDLE, CHASE, ATTACK }
	public eGuardianState guardianState;
	public Enemy m_Target;
	public LayerMask layerMask;

	private float m_damaged;

	public void SetData(Transform parent = null)
    {
		transform.SetParent(parent);
        gameObject.SetActive(true);
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
        trans = transform;

		InitIdle();
	}

	#region FSM IDLE
	public void InitIdle()
	{
		guardianState = eGuardianState.IDLE;
		m_Target = null;
	}
	public void ModifyIdle()
	{
		if(m_Target)
		{
			InitChase();
			return;
		}
		SearchEnemy();
	}
	#endregion

	#region FSM CHASE
	public void InitChase()
	{
		guardianState = eGuardianState.CHASE;
	}
	public void ModifyChase()
	{
		float distance;
		if (m_Target)
		{
			distance = Vector3.Distance(centerTrans.position, m_Target.centerTrans.position);
			if (distance < m_GuardianData.attackRadius)
			{
				InitAttack();
				return;
			}
		}
		else
		{
			InitIdle();
			return;
		}

		trans.position = 
			Vector3.MoveTowards(trans.position, m_Target.centerTrans.position, m_GuardianData.speed * Time.deltaTime);
	}
	#endregion

	#region FSM Attack
	float attackTrackTime;
	public void InitAttack()
	{
		guardianState = eGuardianState.ATTACK;
		attackTrackTime = Time.time;
	}
	public void ModifyAttack()
	{
		if (!m_Target)
		{
			InitIdle();
			return;
		}
		else
		{
			float distance = Vector3.Distance(centerTrans.position, m_Target.centerTrans.position);
			if (distance > m_GuardianData.attackRadius)
			{
				InitChase();
				return;
			}
		}
		if (Time.time > attackTrackTime)
		{
			attackTrackTime = Time.time + m_GuardianData.ATTACK_TIME;
			m_Target.TakeDamage(m_GuardianData.damage);
		}
	}
	#endregion

	void Update()
	{
		switch (guardianState)
		{
			case eGuardianState.IDLE:
				ModifyIdle();
				break;

			case eGuardianState.CHASE:
				ModifyChase();
				break;

			case eGuardianState.ATTACK:
				ModifyAttack();
				break;
		}

		float damage = m_damaged;
		m_damaged = 0;
		if (damage > 0)
		{
			m_GuardianData.health -= damage;
			if (m_GuardianData.health <= 0)
			{
				Dead();
				return;
			}
		}

		m_SpriteRenderer.sortingOrder = (int)(trans.position.y * -100.0f);
	}

	#region SearchEnemy Function
	float checkTime = 0.0f;
	float CONST_CHECKTIME = 0.5f;
	void SearchEnemy()
	{
		if (Time.time < checkTime) return;
		if (m_Target) return;
		checkTime = Time.time + CONST_CHECKTIME;

		Vector3 currentPosition = centerTrans.position;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(currentPosition, m_GuardianData.searchRadius, layerMask);

		float distance = float.MaxValue;
		Enemy nearestEnemy = null;

		for (int i = 0; i < colliders.Length; i++)
		{
			float nearbyEnemyDistance = Vector3.Distance(currentPosition, colliders[i].transform.position);
			if (nearbyEnemyDistance < distance)
			{
				distance = nearbyEnemyDistance;
				nearestEnemy = colliders[i].GetComponent<Enemy>();
				if (nearestEnemy != null)
				{
					m_Target = nearestEnemy;
				}
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
		UnitManager.instance.Remove(this);
		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos2.DrawCircle2(centerTrans.position, Color.yellow, m_GuardianData.searchRadius);
		Gizmos2.DrawCircle2(centerTrans.position, Color.red, m_GuardianData.attackRadius);
	}
}

[System.Serializable]
public class GuardianData
{
    public eGuardianType guardianType;

	public float    health;
	public float    speed;
	public float    damage;
	public int      cost;
	public float    searchRadius;
	public float    attackRadius;
	public float    ATTACK_TIME;
}
