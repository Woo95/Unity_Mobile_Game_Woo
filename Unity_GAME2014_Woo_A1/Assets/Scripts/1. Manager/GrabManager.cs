using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
	#region singletone;
	public static GrabManager instance;
	public void Awake()
	{
		instance = this;
	}
	#endregion

	private Camera myCam;
	public LayerMask others;
	public List<GameObject> unitList = new List<GameObject>();
	public List<GameObject> unitSelected = new List<GameObject>();

	public void Init()
	{
		myCam = Camera.main;
		unitList.Clear();
		unitSelected.Clear();
	}

	#region Guardian Select And UnSelect
	public void ClickSelect(GameObject unitToAdd)
	{
		Debug.Log("Clicked");
		Guardian guardian = unitToAdd.GetComponent<Guardian>();
		if (!unitSelected.Contains(unitToAdd))
		{
			unitSelected.Add(unitToAdd);
			guardian.SetSelect(true);
		}
		else
		{
			unitSelected.Remove(unitToAdd);
			guardian.SetSelect(false);
		}
	}

	public void DeselectAll()
	{
		foreach (var unit in unitSelected)
		{
			unit.GetComponent<SpriteRenderer>().color = Color.white;
		}
		unitSelected.Clear();
	}

	public void Deselect(GameObject unitToDeselect)
	{
		unitSelected.Remove(unitToDeselect);
		unitToDeselect.GetComponent<SpriteRenderer>().color = Color.white;

	}
	#endregion

	public void End()
	{

	}

	public void Play()
	{
		if (Input.GetMouseButton(0))
		{
			Vector2 mousePos = myCam.ScreenToWorldPoint(Input.mousePosition);

			// No guardian clicked, check for background click
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, others);
			if (hit.collider != null)
			{
				DeselectAll();
			}
		}
	}
}
