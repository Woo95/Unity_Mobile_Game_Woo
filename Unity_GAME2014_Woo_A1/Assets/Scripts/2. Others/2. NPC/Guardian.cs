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
	}
	#endregion

	#region FSM Attack
	public void InitAttack()
	{
		guardianState = eGuardianState.ATTACK;
	}
	public void ModifyAttack()
	{
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

	private void OnDestroy()
	{
		UnitManager.instance.Remove(this);
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
