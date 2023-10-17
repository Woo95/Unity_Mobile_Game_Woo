using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public enum eGuardianType { G1, G2, G3 }
public class Guardian : MonoBehaviour
{
    public GuardianData m_GuardianData;
    SpriteRenderer m_SpriteRenderer;
	Transform trans;

	public enum eGuardianState { NONE, IDLE, CHASE, ATTACK }
	public eGuardianState guardianState;

	public void SetData()
    {
        gameObject.SetActive(true);
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
        trans = transform;
	}

	#region FSM IDLE
	public void InitIdle()
	{
		guardianState = eGuardianState.IDLE;
	}
	public void ModifyIdle()
	{
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

	private void OnDestroy()
	{
		UnitManager.instance.Remove(this);
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
