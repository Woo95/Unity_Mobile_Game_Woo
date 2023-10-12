using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public enum eEnemyState
	{ NONE, MOVE, ATTACK }
	public eEnemyState enemyState;

	public float health = 100.0f;
	public float speed = 2.0f;
	public float damage = 2.0f;

	public float searchRadius = 5.0f;
	public float attackRadius = 0.5f;
	public float attackTime = 2.0f;

	//Transform trans;
	//void Start()
	//{
	//	trans = transform;
	//	InitMove();
	//}

	//public Tower target;
	//public LayerMask layerMask;


	//public void InitMove()
	//{
	//	enemyState = eEnemyState.MOVE;
	//}
	//public void ModifyMove()
	//{
	//	if (target != null)
	//	{
	//		InitChase();
	//		return;
	//	}
	//	else if (target == null) // no target around.
	//	{
	//		if (trans.position == worldPosition[positionIndex].position)
	//		{
	//			positionIndex = (positionIndex + 1) % 4;
	//		}
	//		trans.position = Vector3.MoveTowards(trans.position, worldPosition[positionIndex].position, speed * Time.deltaTime);
	//	}
	//	CheckPlayer();
	//}
	//public void InitChase()
	//{
	//	enemyState = eEnemyState.CHASE;


	//}
	//public void ModifyChase()
	//{

	//}
	//public void InitAttack()
	//{
	//	enemyState = eEnemyState.ATTACK;


	//}
	//public void ModifyAttack()
	//{

	//}

	//void Update()
	//{
	//	switch (enemyState)
	//	{
	//		case eEnemyState.MOVE:
	//			ModifyMove();
	//			break;

	//		case eEnemyState.CHASE:
	//			ModifyChase();
	//			break;

	//		case eEnemyState.ATTACK:
	//			ModifyAttack();
	//			break;
	//	}
	//}

	//float checkTime = 0.0f;
	//float CONST_CHECKTIME = 0.5f;
	//void CheckPlayer()
	//{
	//	if (Time.time < checkTime) return;
	//	checkTime = Time.time + CONST_CHECKTIME;

	//	Collider[] collider = Physics.OverlapSphere(trans.position, searchRadius, layerMask);

	//	for (int i = 0; i < collider.Length; i++)
	//	{
	//		Player scp = collider[i].GetComponent<Player>();
	//		if (scp != null)
	//		{
	//			target = scp;
	//			return;
	//		}
	//	}
	//}

}