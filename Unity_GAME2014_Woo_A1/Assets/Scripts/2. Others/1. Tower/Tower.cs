using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
	private Transform m_guardiansParent;
	public List<Guardian> guardianPrefabList;

	SpriteRenderer m_SpriteRenderer;
	public Canvas m_PurchaseCanvas;
	private Transform m_CastleTrans;

	public float m_Health;
	public List<Transform> spawnPointList = new List<Transform>();

	private void Start()
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_PurchaseCanvas.gameObject.SetActive(false);
		m_CastleTrans = transform;

		Transform buildManager = m_CastleTrans.parent.parent;
		m_guardiansParent = buildManager.transform.Find("Guardians");
	}

	private void OnBecameVisible() // function calls only once it appeals on game scene
	{
		m_SpriteRenderer.sortingOrder = (int)(transform.position.y * -100.0f);
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

	public void Invoke_PurchaseUIClose()
	{
		m_PurchaseCanvas.gameObject.SetActive(false);
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
		int currentGold = CentralTower.instance.m_Gold;
		int guardianCost = guardianToBuy.m_GuardianData.cost;

		if (currentGold >= guardianCost)
		{
			int randIndex = Random.Range(0, spawnPointList.Count-1);
			Vector3 spawnPosition = 
				Vector3.Lerp(spawnPointList[randIndex].position, spawnPointList[randIndex + 1].position, 
				Random.Range(0.0f, 1.0f));
			
			Guardian purchasedGuardian = Instantiate(guardianToBuy, spawnPosition, Quaternion.identity, m_guardiansParent);
			purchasedGuardian.SetData();
			UnitManager.instance.Add(purchasedGuardian);

			m_PurchaseCanvas.gameObject.SetActive(false);

			CentralTower.instance.AddGold(-guardianCost);
		}
	}
}
