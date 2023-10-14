using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public Guardian[] guardianPrefabs;
	public Canvas m_PurchaseCanvas;

	public int m_BuyGold;
	public int m_GoldExp;
	public int m_Health;

	private void Start()
	{
		m_PurchaseCanvas.gameObject.SetActive(false);
	}

	private void OnMouseDown()
	{
		Debug.Log(">>Guardian UI Call");
		m_PurchaseCanvas.gameObject.SetActive(true);
	}

	public void Invoke_PurchaseG1()
	{
		m_PurchaseCanvas.gameObject.SetActive(false);
	}
	public void Invoke_PurchaseG2()
	{
		m_PurchaseCanvas.gameObject.SetActive(false);
	}
	public void Invoke_PurchaseG3()
	{
		m_PurchaseCanvas.gameObject.SetActive(false);
	}
}
