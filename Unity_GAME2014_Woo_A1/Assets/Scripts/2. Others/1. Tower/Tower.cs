using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public Guardian[] guardianPrefabs;

	public int m_BuyGold;
	public int m_GoldExp;
	public int m_Health;

	private void OnMouseDown()
	{
		Debug.Log(">>Guardian UI Call");
	}
}
