using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
	public Transform m_GuardianTowerParent;
	public GameObject m_GuardianTowerPrefab;

	public BuildArea m_BuildArea;
	public Image m_BuildIcon;
	public bool isTowerPlacable = false;

    public void Invoke_PlaceGuardianTower()
    {
        isTowerPlacable = !isTowerPlacable;

		m_BuildIcon.color = isTowerPlacable ? Color.green : Color.white;
		m_BuildArea.gameObject.SetActive(isTowerPlacable);
	}

	public void Init()
	{
		m_BuildArea.gameObject.SetActive(false);
	}

	public void PlaceTower()
	{
		if (!isTowerPlacable)
			return;

		// Get the mouse position in screen space
		Vector3 mousePosition = Input.mousePosition;
		// Convert the screen space coordinates to world space
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -Camera.main.transform.position.z));
		worldPosition.y -= 1.5f;

		m_BuildArea.transform.position = worldPosition;

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			if (m_BuildArea.m_Collision == false)
			{
				int currentResource = CentralTower.instance.m_Resource;
				int guardianTowerCost = m_GuardianTowerPrefab.GetComponent<GuardianTower>().m_BuyResource;

				if (currentResource >= guardianTowerCost)
				{
					Instantiate(m_GuardianTowerPrefab, worldPosition, Quaternion.identity, m_GuardianTowerParent);
					ResetTowerPlacement();

					CentralTower.instance.AddResource(-guardianTowerCost);
				}
			}
			else
			{
				ResetTowerPlacement();
			}
		}
	}

	void ResetTowerPlacement()
	{
		isTowerPlacable = false;
		m_BuildIcon.color = Color.white;
		m_BuildArea.gameObject.SetActive(false);
	}
}