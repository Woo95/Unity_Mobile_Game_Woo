using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{
	#region singleton
	public static UIGamePlay instance;
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		else
			instance = this;
	}
	#endregion

	public GameObject m_GamePlayUI;

	public void Init()
	{
		if (!m_GamePlayUI.activeInHierarchy)
			m_GamePlayUI.SetActive(true);
	}
}
