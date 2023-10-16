using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
	public List<Guardian> guardianPrefabList;

	public Canvas m_PurchaseCanvas;
	private Transform m_CastleTrans;

	public int m_BuyGold;
	public int m_GoldExp;
	public int m_Health;

	private void Start()
	{
		m_PurchaseCanvas.gameObject.SetActive(false);
		m_CastleTrans = transform;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (m_PurchaseCanvas.gameObject.activeInHierarchy)
			m_PurchaseCanvas.gameObject.SetActive(false);
		else
			m_PurchaseCanvas.gameObject.SetActive(true);
	}

	public void Invoke_PurchaseG1()
	{
		Guardian g1 = guardianPrefabList.Find(guardian => guardian.m_GuardianData.guardianType == eGuardianType.G1);

		if (g1 != null)
		{
			PurchaseGuardian(g1);
		}
	}

	public void Invoke_PurchaseG2()
	{
		Guardian g2 = guardianPrefabList.Find(guardian => guardian.m_GuardianData.guardianType == eGuardianType.G2);

		if (g2 != null)
		{
			PurchaseGuardian(g2);
		}
	}

	public void Invoke_PurchaseG3()
	{
		Guardian g3 = guardianPrefabList.Find(guardian => guardian.m_GuardianData.guardianType == eGuardianType.G3);

		if (g3 != null)
		{
			PurchaseGuardian(g3);
		}
	}

	private void PurchaseGuardian(Guardian guardianToBuy)
	{
		int currentGold = 1000; // CentralTower.instance.m_Gold;
		int guardianCost = guardianToBuy.m_GuardianData.cost;

		if (currentGold >= guardianCost)
		{
			CentralTower.instance.m_Gold -= guardianCost;

			float xOffset = Random.Range(-2.0f, 2.0f);
			float yOffset = Random.Range(3.0f, 4.0f);
			Vector3 spawnPosition = m_CastleTrans.position + new Vector3(xOffset, -yOffset, 0);

			Instantiate(guardianToBuy, spawnPosition, Quaternion.identity);


			m_PurchaseCanvas.gameObject.SetActive(false);
		}
	}
}
